using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour {
    public enum eAIMode { Idle, Alert, Aggro };
    public eAIMode myAIMode;
    public enum eTurretLevel { First, Second, Third, Fourth };
    public eTurretLevel myTurretLvl;
    public float damping;
    public Transform endofturret;
    public float fireRate;
    public float turretAccuracy;
    private float turretCooldown;
    public AudioSource gunShotSound;
    public GameObject missile;
    public float f_TurretHealth;
    public GameObject flames;
    public GameObject upgradeParticle;
    public List<GameObject> Targets;

    // Use this for initialization
    void Start()
    {
        print(f_TurretHealth);
        myAIMode = eAIMode.Idle;
        turretCooldown = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Targets.Count > 0)
        {
            //if (Targets[0].GetComponent<EnemyStats>().m_fEnemyHealth <= 0)
            if (Targets[0] == null)
            {
                //Destroy(Targets[0]);
                Targets.RemoveAt(0);

            }
        }
        if (Targets.Count <= 0)
        {
            myAIMode = eAIMode.Idle;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(Targets.Count);
        }

        switch (myAIMode)
        {
            case eAIMode.Idle:
                {
                    /*if (transform.eulerAngles.x != 0)
                    {
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    transform.Rotate(0, 10 * Time.deltaTime, 0);*/
                    break;
                }
            case eAIMode.Aggro:
                {
                    if (Targets[0] != null)
                    {
                        Vector3 lookpos = Targets[0].transform.position - transform.position;
                        //lookpos.y = 0;
                        Quaternion rotation = Quaternion.LookRotation(lookpos);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

                        if (Time.time > turretCooldown)
                        {
                            Vector3 rayOrigin = endofturret.position;
                            Vector3 rayDirection = Targets[0].transform.position - endofturret.position;
                            RaycastHit hit;
                            if (Physics.Raycast(endofturret.position, rayDirection, out hit, 100))
                            {
                                if (hit.transform.tag == "Enemy")
                                {
                                    rayDirection.x += Random.Range(-turretAccuracy, turretAccuracy);
                                    rayDirection.z += Random.Range(-turretAccuracy, turretAccuracy);
                                    rayDirection.y += Random.Range(-turretAccuracy, turretAccuracy);
                                    if (Physics.Raycast(endofturret.position, rayDirection, out hit, 100))
                                    {
                                        Debug.DrawRay(endofturret.position, rayDirection, Color.yellow);
                                        //Debug.Log(hit.transform.name);
                                        CheckHit(hit, rayDirection);

                                        ////////////////////////////////////////////
                                        //draw raycast if player is in view, instantiate rocket that travels in direction of rayDirection,
                                        //when collides creates physics explosion force and makes raycast towards player with range of around 5
                                        //if it hits then addimpact to player in direction of explosion ray and sendmessage-playershot with damage multiplied by 
                                        //distance to explosion normalized
                                        ///////////////////////////////////////////



                                    }
                                    else
                                    {
                                        Debug.DrawRay(endofturret.position, rayDirection, Color.white);
                                        CheckHit(hit, rayDirection);
                                    }
                                    gunShotSound.Play();
                                    turretCooldown = Time.time + fireRate;
                                }
                            }
                        }
                    }
                    break;
                }
        }
        if (f_TurretHealth <= 0)
        {
            TurretDeath();
        }
    }


    void TurretDeath()
    {
        //give player xp
        //spawn any particle effects
        Instantiate(flames, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            myAIMode = eAIMode.Aggro;
            Targets.Insert(Targets.Count, other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Targets.Remove(other.gameObject);
        }
    }

    void CheckHit(RaycastHit hit, Vector3 rayDirection)
    {
        GameObject MissileObj = Instantiate(missile, endofturret.position, Quaternion.Euler(new Vector3(rayDirection.x, rayDirection.y, rayDirection.z)));
        MissileObj.transform.forward = rayDirection;
    }

    void TurretShot(float damage)
    {
        f_TurretHealth -= damage;
        print(f_TurretHealth);
    }

    public void UpgradeTo(eTurretLevel lvl)
    {
        if (lvl == eTurretLevel.Second)
        {
            Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //turretDamage += turretDamage;
            fireRate *= 0.5f;
            myTurretLvl = eTurretLevel.Second;
        }
        if (lvl == eTurretLevel.Third)
        {
            Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //turretDamage += turretDamage;
            fireRate *= 0.5f;
            myTurretLvl = eTurretLevel.Third;
        }
        if (lvl == eTurretLevel.Fourth)
        {
            Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //turretDamage += turretDamage;
            fireRate *= 0.5f;
            myTurretLvl = eTurretLevel.Fourth;
        }
    }

}
