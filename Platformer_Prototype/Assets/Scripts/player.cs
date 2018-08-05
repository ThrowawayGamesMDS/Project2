using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    float speed = 10;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            var move = new Vector3(0, 0, 1);
            transform.position += move * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            var move = new Vector3(0, 0, -1);
            transform.position += move * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            var move = new Vector3(-1, 0, 0);
            transform.position += move * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var move = new Vector3(1, 0, 0);
            transform.position += move * speed * Time.deltaTime;

        }
    }
}
