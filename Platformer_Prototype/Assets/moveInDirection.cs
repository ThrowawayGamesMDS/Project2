﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveInDirection : MonoBehaviour {
    public Vector3 xyz;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(xyz * Time.deltaTime);
	}
}
