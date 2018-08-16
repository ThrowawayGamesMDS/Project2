using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endzone : MonoBehaviour {
    public static float health;
	// Use this for initialization
	void Start () {
        health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(3);
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
