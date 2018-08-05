using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAggro : MonoBehaviour
{

    public enum eAIMode { Idle, Alert, Aggro };
    public eAIMode myAIMode;
    public GameObject playerCentre;
    public float damping;
    public Transform endofturret;
    public float fireRate;
    public float turretAccuracy;
    private float turretCooldown;
    public AudioSource gunShotSound;
    public GameObject ball;
    public GameObject playerObj;
    public float playerForce;
    public float f_TurretHealth;
    public GameObject flames;
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
                    Vector3 lookpos = playerCentre.transform.position - transform.position;
                    //lookpos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(lookpos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

                    if (Time.time > turretCooldown)
                    {
                        Vector3 rayOrigin = endofturret.position;
                        Vector3 rayDirection = playerCentre.transform.position - endofturret.position;
                        RaycastHit hit;
                        if (Physics.Raycast(endofturret.position, rayDirection, out hit, 100))
                        {
                            if (hit.transform.tag == "Player")
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
        if (other.tag == "Player")
        {
            myAIMode = eAIMode.Aggro;
            Debug.Log(myAIMode);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myAIMode = eAIMode.Idle;
            Debug.Log(myAIMode);
        }
    }

    void CheckHit(RaycastHit hit, Vector3 rayDirection)
    {
        GameObject pInstance = Instantiate(ball, hit.point, Quaternion.identity);
        pInstance.transform.up = hit.normal;
        if (hit.transform.tag == "Player")
        {
            //hit.transform.SendMessage("PlayerShot", 20f);
            //push player here

        }
    }

    void TurretDeath()
    {
        //give player xp
        //spawn any particle effects
        Instantiate(flames, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void TurretShot(float damage)
    {
        f_TurretHealth -= damage;
        print(f_TurretHealth);
    }


}
