using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNewObjects : MonoBehaviour {
    public GameObject enemys;
    public GameObject des;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject dummy = Instantiate(enemys);
            dummy.GetComponent<enemyWalk>().des = des;
        }
	}
}
