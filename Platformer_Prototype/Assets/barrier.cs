using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {
    public float deffendsHP;
    Collider m_ObjectCollider;
    float delay = 200;
    // Use this for initialization
    void Start () {
        m_ObjectCollider.GetComponent<Collider>();

    }
	
	// Update is called once per frame
	void Update () {
        if (deffendsHP <= 0)
        {
            Destroy(m_ObjectCollider);
            if (delay == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                delay--;
            }
          
        }
    }
    public void barrierShot(float damage)
    {
       
            deffendsHP -= damage;
        
       
        //print(f_TurretHealth);
    }
}
