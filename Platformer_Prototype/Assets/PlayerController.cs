using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool m_bPlayerPlacingTurret;
    public GameObject[] m_goPossibleTurrets;
    public GameObject[] m_goTurretPlacementOk;
    public GameObject[] m_goTurretPlacementBad;
    public GameObject m_goPlacementDefault;
    public GameObject m_goCurrentlyPlacing;
    public GameObject[] m_goPlacedTurrets;
    public int m_iTurretsPlaced;
    public int m_iMaxTurretsPlaceable;
    public int m_iCurrentlyPlacing;

    public enum PlayerStates
    {
        PLACING, DEFAULT
    }
    PlayerStates m_ePlayerState;

    void Start()
    {
        m_iTurretsPlaced = 0;
        m_iCurrentlyPlacing = 0;
        m_goCurrentlyPlacing = m_goPossibleTurrets[0];
        m_bPlayerPlacingTurret = false;
        m_ePlayerState = PlayerStates.DEFAULT;
        m_iMaxTurretsPlaceable = 50;
        m_goPlacedTurrets = new GameObject[m_iMaxTurretsPlaceable];
        m_goPlacementDefault = m_goTurretPlacementOk[m_iCurrentlyPlacing];
    }

    RaycastHit GenerateRayCast(float _fDistanceOfRay)
    {
        RaycastHit _rh;
        int _iWidth = Screen.width / 2;
        int _iHeight = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(_iWidth, _iHeight));

        Debug.DrawRay(ray.origin, ray.direction * _fDistanceOfRay, new Color(1f, 0.922f, 0.016f, 1f));

        if (Physics.Raycast(ray.origin, ray.direction * _fDistanceOfRay, out _rh, 250.0f))
        {
            return _rh;
        }
        else
        {
            return _rh;
        }
    }

    private bool PlayerCanPlace(RaycastHit _rhCheck)
    {
        if (_rhCheck.transform.tag == "Ground")
        {
            // do some other checks here...
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckIfPlayerCanPlaceTurret()
    {
        if (m_iTurretsPlaced <= m_iMaxTurretsPlaceable)
        {
            RaycastHit _rhCheck;
            Vector3 m_vec3Pos = new Vector3(0, 0, 0);

            _rhCheck = GenerateRayCast(5.0f);
            if (PlayerCanPlace(_rhCheck))
            {
                m_vec3Pos = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
                if (m_vec3Pos.y < 0 || m_vec3Pos.y > 0)
                {
                    m_vec3Pos.y = 0.5f;
                }

                for (int i = 0; i < m_iTurretsPlaced; i++)
                {
                    //if ((m_vec3Pos.x < m_goPlacedTurrets[i].transform.position.x + 3.0f && m_vec3Pos.x > m_goPlacedTurrets[i].transform.position.x - 3.0f) && (m_vec3Pos.z < m_goPlacedTurrets[i].transform.position.z + 3.0f && m_vec3Pos.z > m_goPlacedTurrets[i].transform.position.z - 3.0f))
                    if (Vector3.Distance(m_vec3Pos, m_goPlacedTurrets[i].transform.position) <= 1.7f)
                    {
                        // a turret already exists in the desired position
                        print("CANT PLACE TURRET!");
                        return;
                    }
                }
                m_goPlacedTurrets[m_iTurretsPlaced] = Instantiate(m_goPossibleTurrets[m_iCurrentlyPlacing], m_vec3Pos, Quaternion.identity) as GameObject;
               //m_goPlacedTurrets[m_iTurretsPlaced] = Instantiate(m_goCurrentlyPlacing, m_vec3Pos, Quaternion.identity) as GameObject;
               m_iTurretsPlaced += 1;
            }
        }

    }

    private void GeneratePlayerGunShot()
    {
        GenerateRayCast(Random.Range(90.0f, 200.0f));
    }

    private void UpdateTurretPlacementDefault(Vector3 _vec3DesiredPos)
    {
        m_goPlacementDefault.transform.position = _vec3DesiredPos;
        for (int i = 0; i < m_iTurretsPlaced; i++)
        {
            //if ((m_vec3Pos.x < m_goPlacedTurrets[i].transform.position.x + 3.0f && m_vec3Pos.x > m_goPlacedTurrets[i].transform.position.x - 3.0f) && (m_vec3Pos.z < m_goPlacedTurrets[i].transform.position.z + 3.0f && m_vec3Pos.z > m_goPlacedTurrets[i].transform.position.z - 3.0f))
            if (Vector3.Distance(_vec3DesiredPos, m_goPlacedTurrets[i].transform.position) <= 1.7f)
            {
                Destroy(m_goPlacementDefault);
                m_goPlacementDefault = Instantiate(m_goTurretPlacementBad[m_iCurrentlyPlacing], _vec3DesiredPos, Quaternion.identity) as GameObject; 
                // a turret already exists in the desired position
                print("CANT PLACE TURRET HERE!");
                return;
            }
            else
            {
                Destroy(m_goPlacementDefault);
                m_goPlacementDefault = Instantiate(m_goTurretPlacementOk[m_iCurrentlyPlacing], _vec3DesiredPos, Quaternion.identity) as GameObject;
            }
        }
    }
    
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            switch (m_ePlayerState)
            {
                case PlayerStates.PLACING:
                    Destroy(m_goPlacementDefault);
                    m_ePlayerState = PlayerStates.DEFAULT;
                    break;
                case PlayerStates.DEFAULT:
                    Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
                    if (pos.y < 0 || pos.y > 0)
                    {
                        pos.y = 0.5f;
                    }
                    m_goPlacementDefault = Instantiate(m_goTurretPlacementOk[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
                    m_ePlayerState = PlayerStates.PLACING;
                    break;
            }

        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            m_iCurrentlyPlacing = 0;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            m_iCurrentlyPlacing = 1;
        }

        if (m_ePlayerState == PlayerStates.PLACING)
        {
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
            if (pos.y < 0 || pos.y > 0)
            {
                pos.y = 0.5f;
            }
            UpdateTurretPlacementDefault(pos);
        }

		if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            print("player trying to left click");
            switch (m_ePlayerState)
            {
                case PlayerStates.DEFAULT:
                    {
                        GeneratePlayerGunShot();
                        break;
                    }
                case PlayerStates.PLACING:
                    {
                        CheckIfPlayerCanPlaceTurret();
                        break;
                    }
            }
        }
	}
}
