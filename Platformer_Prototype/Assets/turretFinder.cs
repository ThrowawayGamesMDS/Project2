using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretFinder : MonoBehaviour {
    SphereCollider argoRange;
  
	// Use this for initialization
	void Start () {
        argoRange = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        print("gi");
        if (GetComponent<enemyWalk>().argro == true)
        {
            //find out which is more of a problem
        }
        else
        {
            if (col.transform.tag == "Turret")
            {
                print("hti");
                GetComponent<enemyWalk>().argro = true;
                GetComponent<enemyWalk>().argoTo = col.gameObject;
            }
        }


    }
}
