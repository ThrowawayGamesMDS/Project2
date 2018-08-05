using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSelf : MonoBehaviour {
    public float timer;
	// Use this for initialization
	void Start () {
        Invoke("destroyself", timer);
	}
	
	// Update is called once per frame
	void destroyself () {
        Destroy(gameObject);
	}
}
