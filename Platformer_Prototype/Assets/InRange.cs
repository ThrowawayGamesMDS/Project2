using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour {
    public int threat;
    public MeshCollider col;
	// Use this for initialization
	void Start () {
        col.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            
            print("inRange");
            if (other.GetComponent<EnemyStats>().myAIMode != EnemyStats.eAIMode.attack)
            {
                
                other.GetComponent<EnemyStats>().myAIMode = EnemyStats.eAIMode.attack;
            }
          
            print(other.GetComponent<EnemyStats>().myAIMode);
            
            other.GetComponent<EnemyStats>().Targets.Insert(other.GetComponent<EnemyStats>().Targets.Count, gameObject);

            other.GetComponent<EnemyStats>().priority.Insert(other.GetComponent<EnemyStats>().priority.Count, threat);

            other.GetComponent<enemyWalk>().setupattacker();
            other.GetComponent<enemyWalk>().enemy.SetInteger("speed", 0);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
          
            //other.GetComponent<EnemyStats>().Targets.Remove(gameObject);

        }
    }
    public void destorycol()
    {
        Destroy(col);
    }
}
