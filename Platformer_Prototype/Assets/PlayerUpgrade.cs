using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layermask =~ LayerMask.GetMask("upgrade");
            if (Physics.Raycast(ray, out hit, 6, layermask))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);
                Transform objectHit = hit.transform;
                switch (objectHit.tag)
                {
                    case "Turret":
                        {
                            if(hit.transform.gameObject.GetComponent<TurretAggro>() != null)
                            {
                                switch (hit.transform.gameObject.GetComponent<TurretAggro>().myTurretLvl)
                                {
                                    case TurretAggro.eTurretLevel.First:
                                        {
                                            if (PublicStats.g_fResourceCount >= 50)
                                            {
                                                print("upgrade turret to level 2");
                                                PublicStats.g_fResourceCount -= 50;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<TurretAggro>().UpgradeTo(TurretAggro.eTurretLevel.Second);
                                            }
                                            break;
                                        }
                                    case TurretAggro.eTurretLevel.Second:
                                        {
                                            if (PublicStats.g_fResourceCount >= 100)
                                            {
                                                print("upgrade turret to level 3");
                                                PublicStats.g_fResourceCount -= 100;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<TurretAggro>().UpgradeTo(TurretAggro.eTurretLevel.Third);
                                            }
                                            break;
                                        }
                                    case TurretAggro.eTurretLevel.Third:
                                        {
                                            if (PublicStats.g_fResourceCount >= 150)
                                            {
                                                print("upgrade turret to level 4");
                                                PublicStats.g_fResourceCount -= 150;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<TurretAggro>().UpgradeTo(TurretAggro.eTurretLevel.Fourth);
                                            }
                                            break;
                                        }
                                    case TurretAggro.eTurretLevel.Fourth:
                                        {
                                            print("cannot upgrade any further");
                                            print("balance: " + PublicStats.g_fResourceCount);
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }


                            if (hit.transform.gameObject.GetComponent<RocketTurret>() != null)
                            {
                                switch (hit.transform.gameObject.GetComponent<RocketTurret>().myTurretLvl)
                                {
                                    case RocketTurret.eTurretLevel.First:
                                        {
                                            if (PublicStats.g_fResourceCount >= 50)
                                            {
                                                print("upgrade turret to level 2");
                                                PublicStats.g_fResourceCount -= 50;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<RocketTurret>().UpgradeTo(RocketTurret.eTurretLevel.Second);
                                            }
                                            break;
                                        }
                                    case RocketTurret.eTurretLevel.Second:
                                        {
                                            if (PublicStats.g_fResourceCount >= 100)
                                            {
                                                print("upgrade turret to level 3");
                                                PublicStats.g_fResourceCount -= 100;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<RocketTurret>().UpgradeTo(RocketTurret.eTurretLevel.Third);
                                            }
                                            break;
                                        }
                                    case RocketTurret.eTurretLevel.Third:
                                        {
                                            if (PublicStats.g_fResourceCount >= 150)
                                            {
                                                print("upgrade turret to level 4");
                                                PublicStats.g_fResourceCount -= 150;
                                                print("balance: " + PublicStats.g_fResourceCount);
                                                hit.transform.gameObject.GetComponent<RocketTurret>().UpgradeTo(RocketTurret.eTurretLevel.Fourth);
                                            }
                                            break;
                                        }
                                    case RocketTurret.eTurretLevel.Fourth:
                                        {
                                            print("cannot upgrade any further");
                                            print("balance: " + PublicStats.g_fResourceCount);
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }

                            break;
                        }
                }
            }
        }
    }
}
