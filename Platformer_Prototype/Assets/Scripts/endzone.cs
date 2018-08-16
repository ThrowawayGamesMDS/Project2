using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endzone : MonoBehaviour {
    public static float health;
	// Use this for initialization
	void Start () {
        health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        if (health == 0)
        {
            
        }
    }
    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "enemy")
    //    {
    //        Destroy(col.gameObject);
    //    }
    //}
}
