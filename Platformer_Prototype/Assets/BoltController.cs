using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 30 * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.SendMessage("EnemyShot", 50);
        }
    }

}
