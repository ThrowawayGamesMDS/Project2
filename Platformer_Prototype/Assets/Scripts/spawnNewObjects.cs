using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNewObjects : MonoBehaviour {
    public GameObject[] enemys;
    public GameObject des;
    public List<GameObject> enemiesToCome;
    int timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject dummy = Instantiate(enemys[0]);
            dummy.GetComponent<enemyWalk>().des = des;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            wave();
        }
        if (timer == 0)
        {
            

            if (enemiesToCome.Count > 0)
            {
                GameObject dummy = Instantiate(enemys[0]);
                dummy.GetComponent<enemyWalk>().des = des;

                enemiesToCome.RemoveAt(0);
                timer = 180;
            }
           
           
        }
        else
        {
            timer--;
        }
	}

    void wave()
    {
        enemiesToCome.Add(enemys[0]);
        enemiesToCome.Add(enemys[0]);
        enemiesToCome.Add(enemys[0]);
        enemiesToCome.Add(enemys[0]);
        enemiesToCome.Add(enemys[0]);
    }
}
