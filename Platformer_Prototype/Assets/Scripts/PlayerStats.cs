using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static float f_playerHealth;
    [SerializeField] private float f_playerRegen;
    public float f_MaxHealth;
    public CanvasGroup cg;
    public static float f_regenTimer;


    // Use this for initialization
    void Start() {
        f_playerHealth = f_MaxHealth;
        f_regenTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        f_regenTimer += 1 * Time.deltaTime;
        cg.alpha = 1 - f_playerHealth / f_MaxHealth;
        if (f_playerHealth < 0)
        {
            f_playerHealth = 0;
        }
        if (f_playerHealth < f_MaxHealth)
        {
            if (f_regenTimer > 3)
            {
                f_playerHealth += (f_playerRegen * Time.deltaTime);
            }
        }




        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5))
            {
                Transform objectHit = hit.transform;
                switch (objectHit.tag)
                {
                    case "Weapon":
                        {
                            print("pickup weapon");
                            Destroy(objectHit.gameObject);
                            break;
                        }
                    /*case "Turret":
                        {
                            print("turret shot");
                            hit.transform.SendMessage("TurretShot", 20);
                            break;
                        }*/

                }
            }
        }
     }




    public void PlayerShot(float damage)
    {
        f_playerHealth -= damage;
        f_regenTimer = 0;
    }


}
