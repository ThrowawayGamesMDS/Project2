using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSpawner : MonoBehaviour {
    public GameObject obj;
    public float timer;
    public bool isManual;
    private bool hasInvoked;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isManual)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Instantiate(obj, transform.position, transform.rotation);
            }
        }
        else
        {
            if(!isManual)
            {
                if(!hasInvoked)
                {
                    InvokeRepeating("Spawn", 0, timer);
                    hasInvoked = true;
                }
            }
        }
	}

    void Spawn()
    {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
