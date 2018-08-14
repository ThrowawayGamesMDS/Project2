using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour {
    public float deffendsHP;
    Collider m_ObjectCollider;
    float delay = 200;
    // Use this for initialization
    void Start () {
        m_ObjectCollider.GetComponent<Collider>();

    }
	
	// Update is called once per frame
	void Update () {
        if (deffendsHP <= 0)
        {
            Destroy(m_ObjectCollider);
            if (delay == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                delay--;
            }
          
        }
    }
    public void barrierShot(float damage, GameObject player)
    {
        if (deffendsHP <= 0)
        {
            print("b");

            // for (int i = 0; i < player.GetComponent<EnemyStats>().priority.Count; i++)
            // {
            player.GetComponent<EnemyStats>().Targets.Clear();// (gameObject);
            player.GetComponent<EnemyStats>().priority.Clear();// Remove(gameObject.GetComponentInChildren<InRange>().threat);

            print("c");
            player.GetComponent<enemyWalk>().setupattacker();
            player.GetComponent<enemyWalk>().agent.speed = 2;
            print("d");
            //if (player.GetComponent<EnemyStats>().Targets[i] == gameObject)
            //{
            //    print("a");

            //    player.GetComponent<EnemyStats>().Targets.RemoveAt(i);
            //    player.GetComponent<EnemyStats>().priority.RemoveAt(i);
            //    player.GetComponent<enemyWalk>().setupattacker();
            //    player.GetComponent<enemyWalk>().agent.speed = 2;
            //    break;
            //}
            //}

        }
        else
        {
            deffendsHP -= damage;
        }
       
        //print(f_TurretHealth);
    }
}
