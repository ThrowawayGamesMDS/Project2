using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {


    private float damage;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 30 * Time.deltaTime);
        damage = PublicStats.boltDamage;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.SendMessage("EnemyShot", 50);
        }
    }

}
