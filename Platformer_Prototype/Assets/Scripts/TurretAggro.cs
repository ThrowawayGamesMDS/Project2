﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAggro : MonoBehaviour
{
    public List<GameObject> Targets;
    public enum eAIMode { Idle, Alert, Aggro };
    public enum eTurretLevel { First, Second, Third, Fourth };
    public eAIMode myAIMode;
    public eTurretLevel myTurretLvl;
    public float damping;
    public Transform endofturret;
    public float fireRate;
    public float turretAccuracy;
    public float turretDamage;
    private float turretCooldown;
    public AudioSource gunShotSound;
    public float playerForce;
    public float f_TurretHealth;
    public GameObject flames;
    public GameObject upgradeParticle;
    public float m_fOverallDamage;
    
    public Vector3 lookpos;
    int delay = 150;
    bool alive = true;
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
        if(Targets.Count > 0)
        {
            if(Targets[0] == null)
            {
                Targets.RemoveAt(0);
            }
        }


        if(Targets.Count <= 0)
        {
            myAIMode = eAIMode.Idle;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(Targets.Count);
        }
        switch (myAIMode)
        {
            case eAIMode.Idle:
                {
                    if (transform.eulerAngles.x != 0)
                    {
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    transform.Rotate(0, 10 * Time.deltaTime, 0);
                    break;
                }
            case eAIMode.Aggro:
                {
                    if (alive == true)
                    { 
                        if (Targets[0] != null)
                        {
                            lookpos = Targets[0].transform.position - transform.position;
                            //lookpos.z = 0;
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
                                        }
                                        else
                                        {
                                            Debug.DrawRay(endofturret.position, rayDirection, Color.white);
                                        }
                                        gunShotSound.Play();
                                        turretCooldown = Time.time + fireRate;
                                    }
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
        
        if (hit.transform.tag == "Enemy")
        {
            hit.transform.SendMessage("EnemyShot", turretDamage);
            m_fOverallDamage += turretDamage;
        }
    }

    void TurretDeath()
    {
        //give player xp
        //spawn any particle effects

        //Destroy(m_ObjectCollider);
        gameObject.GetComponentInChildren<InRange>().destorycol();
        alive = false;
        if (delay == 0)
        {
            Instantiate(flames, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
        else
        {
            delay--;
        }
       
    }

    void TurretShot(float damage)
    {
        f_TurretHealth -= damage;
        print(f_TurretHealth);
    }

    public void UpgradeTo(eTurretLevel lvl)
    {
        if (alive == true)
        {


            if (lvl == eTurretLevel.Second)
            {
                Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
                turretDamage *= 1.2f;
                fireRate *= 0.8f;
                myTurretLvl = eTurretLevel.Second;
            }
            if (lvl == eTurretLevel.Third)
            {
                Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
                turretDamage *= 1.2f;
                fireRate *= 0.8f;
                myTurretLvl = eTurretLevel.Third;
            }
            if (lvl == eTurretLevel.Fourth)
            {
                Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
                turretDamage *= 1.2f;
                fireRate *= 0.8f;
                myTurretLvl = eTurretLevel.Fourth;
            }
        }
    }
}
