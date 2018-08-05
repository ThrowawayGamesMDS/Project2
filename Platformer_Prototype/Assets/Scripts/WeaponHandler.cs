using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public int m_iShotgunPelletDispersionRange;
    public int m_iRifleBulletDispersionRange;
    public static int m_iCurrentWeapon;
    private int m_iPlayerHeldShoot;
    public bool m_bPlayerCanShoot;
    public int m_iShotgunAmmoCount;
    public int m_iPistolAmmoCount;
    public int m_iRifleAmmoCount;
    private int[] m_arriGunLevels; // 0 = pistol, 1 = shotgun, 2 = rifle
    private float[] m_arrfGunEXP; // 0 = pistol, 1 = shotgun, 2 = rifle
    public GameObject[] m_goWeapons;
    public GameObject m_goShotHitOBJ;
    public static GameObject m_gActiveWeapon;
    public int m_iAmmoToSpawn;
    public bool m_bAmmoSpawned;

    public GameObject m_goPlayerObject;
    // Use this for initialization
    void Start()
    {
        m_arriGunLevels = new int[3];
        m_arrfGunEXP = new float[3];
        m_iAmmoToSpawn = 1;
        for (int i = 0; i < 3; i++)
        {
            m_arriGunLevels[i] = 1;
            m_arrfGunEXP[i] = 0.0f;
        }
        m_bPlayerCanShoot = true;
        m_bAmmoSpawned = false;
        m_iPlayerHeldShoot = 0;
        m_iCurrentWeapon = 0;
        m_iShotgunPelletDispersionRange = 35;
        m_iRifleBulletDispersionRange = 10;
        m_iShotgunAmmoCount = 0;
        m_iPistolAmmoCount = 0;
        m_iRifleAmmoCount = 0;
        for (int i = 0; i < 3; i++)
        {
            if (i != m_iCurrentWeapon)
            {
                m_goWeapons[i].SetActive(false);
            }
        }
        m_gActiveWeapon = m_goWeapons[m_iCurrentWeapon];
        // m_goWeapons = new GameObject[3];

        if (m_goPlayerObject == null)
        {
            m_goPlayerObject = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void HandleWeaponSwitch()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != m_iCurrentWeapon)
            {
                m_goWeapons[i].SetActive(false);
            }
            else
            {
                m_goWeapons[i].SetActive(true);
            }
        }
        m_gActiveWeapon = m_goWeapons[m_iCurrentWeapon];
    }

    private float DetermineDamage(RaycastHit _h) // int or float depending on what we store npc/player health as???
    {
        var _iResult = 2.0f;
        switch (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
        {
            case WeaponStats.WeaponType.PISTOL:
                {
                    //_iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[0]/ (_h.distance/10));
                    _iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[2]);
                    break;
                }
            case WeaponStats.WeaponType.SHOTGUN:
                {
                    // _iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[1] / (_h.distance / 10));
                    _iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[2]);
                    break;
                }
            case WeaponStats.WeaponType.RIFLE:
                {
                    // _iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[2] / (_h.distance / 10));
                    _iResult = (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fPower * m_arriGunLevels[2]);
                    break;
                }
            default:
                break;
        }

        print("DAMAGE TO DEAL: " + _iResult);
         

        return _iResult;
    }

    private bool PlayerHitSomething(RaycastHit _h)
    {
        if (_h.transform != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckHit(RaycastHit _h, float _fDamageToApply)
    {

        if (_h.transform.tag == "Turret")
        {
            Debug.Log("turret shot");
            _h.transform.SendMessage("TurretShot", _fDamageToApply);
        }
        else if (_h.transform.tag == "Ground")
        {
            GameObject pInstance = Instantiate(m_goShotHitOBJ, _h.point, Quaternion.identity);
            pInstance.transform.up = _h.normal;
        }
        else if (_h.transform.tag == "AMMO_BOX")
        {
            if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType == WeaponStats.WeaponType.SHOTGUN) // multiple box spawn on shotgun shot bug fix lol
            {
                if (!m_bAmmoSpawned) 
                {
                    _h.transform.SendMessage("SpawnTheLoot");
                    m_bAmmoSpawned = true;

                    Invoke("AmmoBoxLootedRefresh", WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fFireRate);
                }
            }
            else
            {
                _h.transform.SendMessage("SpawnTheLoot");
            }
        }
        else
        {
            GameObject pInstance = Instantiate(m_goShotHitOBJ, _h.point, Quaternion.identity);
            pInstance.transform.up = _h.normal;
        }
        
    }

    private void FireRateRefresh()
    {
        m_bPlayerCanShoot = true;
    }

    private void AmmoBoxLootedRefresh()
    {
        m_bAmmoSpawned = false;
    }

    private RaycastHit GenerateRayShot(float _fDispersionRange, float _fShotDistance, bool _bAccuracyVarianceActivated)
    {
        RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0, 0));
        int _iWidth = Screen.width / 2;
        int _iHeight = Screen.height / 2;

        switch (_bAccuracyVarianceActivated)
        {
            case true:
                {
                    switch (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
                    {
                        case WeaponStats.WeaponType.SHOTGUN:
                            {
                                ray = Camera.main.ScreenPointToRay(new Vector3(Random.Range(_iWidth - _fDispersionRange, _iWidth + _fDispersionRange),
                                        Random.Range(_iHeight - _fDispersionRange, _iHeight + _fDispersionRange / 2)));
                                break;
                            }
                        case WeaponStats.WeaponType.RIFLE:
                            {
                                ray = Camera.main.ScreenPointToRay(new Vector3(Random.Range(_iWidth - _fDispersionRange, _iWidth + _fDispersionRange),
                                        Random.Range(_iHeight - _fDispersionRange, _iHeight + _fDispersionRange)));
                                break;
                            }
                    }
                    break;
                }
            case false:
                {
                    ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                    break;
                }
        }

        Debug.DrawRay(ray.origin, ray.direction * _fShotDistance, new Color(1f, 0.922f, 0.016f, 1f));
        if (Physics.Raycast(ray.origin, ray.direction * _fShotDistance, out hit, 250.0f))
        {
            return hit;
        }
        else 
        {
            return hit;
        }
        
    }

    private void PlayerFacingShootingDirection()
    {
        
        Quaternion test;
        test.y =  Camera.main.transform.rotation.y;
        Quaternion _qPlayerDir = m_goPlayerObject.transform.rotation;
        if (m_goPlayerObject.transform.rotation.y != test.y)
        {
            _qPlayerDir.y = test.y;
            m_goPlayerObject.transform.rotation = _qPlayerDir;
            print("Updated player's rotation to face shooting direction");
        }
        else
        {
            print("Player's direction is already facing in that of the camera");
        }
    }

    private void UpdateGunRotationToView()
    {
        Quaternion test;
        test.y = Camera.main.transform.rotation.y;
        Quaternion _qPlayerDir = WeaponHandler.m_gActiveWeapon.transform.rotation;
        if (m_goPlayerObject.transform.rotation.y != test.y)
        {
            _qPlayerDir.y = test.y;
            WeaponHandler.m_gActiveWeapon.transform.rotation = _qPlayerDir;
            print("Updated player's rotation to face shooting direction");
        }
        else
        {
            print("Player's direction is already facing in that of the camera");
        }
    }

    private void GenerateGunShot()
    {
        /***
         * 
         * Check that the players direction is the same as the camera..
         * 
         ***/
        // update char class that player is shooting
       // Character.m_bPlayerShooting = true;
       // PlayerFacingShootingDirection();

        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().playSound();
        RaycastHit hit;
        float _fDamageToApply;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 last_direction = new Vector3(0,0);
        int _iWidth = Screen.width / 2;
        int _iHeight = Screen.height / 2;

            switch (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
            {
                case WeaponStats.WeaponType.PISTOL:
                    hit = GenerateRayShot(0,35,false);
                    if (PlayerHitSomething(hit) == true)
                    {
                        _fDamageToApply = DetermineDamage(hit);
                        CheckHit(hit, _fDamageToApply);
                    }
                break;
            case WeaponStats.WeaponType.SHOTGUN:
                for (int i = 0; i < 9; i++) // random shotgun pellet variance
                {
                    hit = GenerateRayShot(m_iShotgunPelletDispersionRange, 15, true);
                    if (PlayerHitSomething(hit))
                    {
                        _fDamageToApply = DetermineDamage(hit);
                        CheckHit(hit, _fDamageToApply);
                    }
                }
                    break;
            case WeaponStats.WeaponType.RIFLE:
                hit = GenerateRayShot(m_iRifleBulletDispersionRange, 55, true);
                if (PlayerHitSomething(hit))
                {
                    _fDamageToApply = DetermineDamage(hit);
                    CheckHit(hit, _fDamageToApply);
                }
                break;
            }
        Invoke("FireRateRefresh", WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fFireRate);

        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount -= 1;

        m_bPlayerCanShoot = false;
       // Character.m_bPlayerShooting = false;
    }

    private void ReloadPlayersWeapon()
    {
        if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount < WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap) // can reload bcuz curr mag is less than cap size
        {
            if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount > 0) // player has spare ammo for gun
            {
                // dependening on gun individually load each bull? but meh
                if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap == WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount ||
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap > WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount)
                {
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount;
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount = 0;
                }
                else if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount < WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap &&
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount > 0)
                {
                    int _iAmmoToReload;
                    _iAmmoToReload = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap - WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount;
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount += _iAmmoToReload;
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount -= _iAmmoToReload;
                }
                else
                {
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap;
                    WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount -= WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCap;
                }
            }
        }
    }

    private void OtherInputHandle()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadPlayersWeapon();
        }
    }

    private void ShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && m_bPlayerCanShoot) // player shooting
        {
            if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType != WeaponStats.WeaponType.RIFLE)
            {
                GenerateGunShot();
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && m_bPlayerCanShoot)
        {
            if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType == WeaponStats.WeaponType.RIFLE)
            {
                GenerateGunShot();
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            switch (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
            {
                case WeaponStats.WeaponType.PISTOL:
                    break;
                case WeaponStats.WeaponType.RIFLE:
                    m_iRifleBulletDispersionRange = 5;
                    break;
                case WeaponStats.WeaponType.SHOTGUN:
                    m_iShotgunPelletDispersionRange = 17;
                    break;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1)) // player shooting
        {
            switch (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
            {
                case WeaponStats.WeaponType.PISTOL:
                    break;
                case WeaponStats.WeaponType.RIFLE:
                    m_iRifleBulletDispersionRange = 10;
                    break;
                case WeaponStats.WeaponType.SHOTGUN:
                    m_iShotgunPelletDispersionRange = 35;
                    break;
            }

        }
    }

    private void WeaponSwitchInput()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d != 0)
        {
            if (d > 0f)
            {
                if (m_iCurrentWeapon > 0)
                {
                    m_iCurrentWeapon -= 1;
                    print(m_iCurrentWeapon);
                }
                // scroll up
            }
            else if (d < 0f)
            {
                if (m_iCurrentWeapon <= 2)
                {
                    if (m_iCurrentWeapon != 2)
                        m_iCurrentWeapon += 1;

                    print(m_iCurrentWeapon);

                }
            }
            HandleWeaponSwitch();
        }
    }

    private void UpdateGunAmmo(int _iType) // because guns are enabled/disabled we need to update the script bcuz it could be disabled...
    {
       //type 0 = pistol, type 1 = rifle, type 2 = shotty..
       switch(_iType)
        {
            case 0:
                {
                    if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount <= WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount + m_iPistolAmmoCount)
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount;
                    }
                    else
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount += m_iPistolAmmoCount;
                    }
                    m_iPistolAmmoCount = 0;
                    break;
                }
            case 1:
                {
                    if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount <= WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount + m_iRifleAmmoCount)
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount;
                    }
                    else
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount += m_iRifleAmmoCount;
                    }
                    m_iRifleAmmoCount = 0;
                    break;
                }
            case 2:
                {
                    if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount <= WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount + m_iShotgunAmmoCount)
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMaximumAmmoCount;
                    }
                    else
                    {
                        WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount += m_iShotgunAmmoCount;
                    }
                    m_iShotgunAmmoCount = 0;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_iShotgunAmmoCount > 0 || m_iRifleAmmoCount > 0 || m_iPistolAmmoCount > 0)
        {
            switch(WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_eWeaponType)
            {
                case WeaponStats.WeaponType.PISTOL:
                    {

                        UpdateGunAmmo(0);
                        break;
                    }
                case WeaponStats.WeaponType.RIFLE:
                    {
                        UpdateGunAmmo(1);
                        break;
                    }
                case WeaponStats.WeaponType.SHOTGUN:
                    {
                        UpdateGunAmmo(2);
                        break;
                    }
            }
        }

        if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount > 0)
        {
            ShootingInput();
        }

        WeaponSwitchInput();

        OtherInputHandle();

        if (m_iCurrentWeapon > 2 || m_iCurrentWeapon < 0)
        {
            if (m_iCurrentWeapon > 2)
            {
                m_iCurrentWeapon = 2;
            }
            else
            {
                m_iCurrentWeapon = 0;
            }
        }
    }
}
