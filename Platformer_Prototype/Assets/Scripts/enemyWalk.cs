using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyWalk : MonoBehaviour {
    public GameObject des;
   // public GameObject argoTo;
    public float speed;
    public float dis;
    NavMeshAgent agent;
   
    public bool argro = false;
	// Use this for initialization
	void Start () {
       
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        //gameObject.GetComponent<EnemyStats>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 raycastDir;
        RaycastHit hit; 
        Ray workingray;
        switch (gameObject.GetComponent<EnemyStats>().myAIMode)
        {
            case EnemyStats.eAIMode.push:
                if (des != null)
                {
                    // if (Vector3.Distance(transform.position, des.transform.position) < 1.0f)
                    //{
                    //    Destroy(gameObject);

                    //}
                    agent.SetDestination(des.transform.position);

                    raycastDir = des.transform.position - transform.position;
                    //raycastDir.y = raycastDir.y + 1.05f;

                    workingray = new Ray(transform.position, raycastDir);

                    if (Physics.Raycast(workingray, out hit))
                    {
                        if (hit.distance <= dis)
                        {
                            if (hit.transform.tag == "Goal")
                            {
                                hit.transform.GetComponent<endzone>().health--;
                                Destroy(gameObject);
                            }
                            //print("check point");
                        }
                    }
                    raycastDir = raycastDir.normalized;
                    Debug.DrawRay(transform.position, raycastDir * dis, Color.blue);
                }
                break;

            case EnemyStats.eAIMode.attack:
                agent.stoppingDistance = 2;
                agent.SetDestination(gameObject.GetComponent<EnemyStats>().currentTarget.transform.position);

                 raycastDir = gameObject.GetComponent<EnemyStats>().currentTarget.transform.position - transform.position;
                 
                 workingray = new Ray(transform.position, raycastDir);
                if (Physics.Raycast(workingray, out hit))
                {
                    if (hit.distance <= gameObject.GetComponent<EnemyStats>().attackrange)
                    {
					if ((hit.transform.tag == "Turret")||(hit.transform.tag == "Barriers"))
                        {
                            agent.speed = 0;
                            // beguin attacking
                        }

                    }
                }
                //
                break;
            default:
                break;
        }
       
        
        //if (argro == true)
        //{
           

        //    Vector3 raycastDir = argoTo.transform.position - transform.position;
        //    //raycastDir.y = raycastDir.y + 1.05f;

        //    RaycastHit hit;
        //    Ray workingray = new Ray(transform.position, raycastDir);
        //    if (Physics.Raycast(workingray, out hit))
        //    {
        //        if (hit.distance >= 3)
        //        {
        //            agent.SetDestination(argoTo.transform.position);

        //        }
        //        else
        //        {
        //            //attack;
        //        }

        //    }

        //}
        //else
        //{
            

        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        argro = true;
        //        agent.stoppingDistance = 2;
        //    }
        //}
    }
    public void setupattacker()
    {
        int best = 0;
        int slot;
        for (int i = 0; i < gameObject.GetComponent<EnemyStats>().Targets.Count; i++)
        {
            if (gameObject.GetComponent<EnemyStats>().priority[i] == best)
            {
                Vector3 raycastDir = gameObject.GetComponent<EnemyStats>().currentTarget.transform.position - transform.position;

                float closes;
                RaycastHit hit;
                Ray workingray = new Ray(transform.position, raycastDir);
                Physics.Raycast(workingray, out hit);
                closes = hit.distance;

                raycastDir = gameObject.GetComponent<EnemyStats>().Targets[i].transform.position - transform.position;

                workingray = new Ray(transform.position, raycastDir);
                Physics.Raycast(workingray, out hit);

                if (closes > hit.distance)
                {
                    gameObject.GetComponent<EnemyStats>().currentTarget = gameObject.GetComponent<EnemyStats>().Targets[i];
                    best = gameObject.GetComponent<EnemyStats>().priority[i];
                    slot = i;
                }

            }
                if (gameObject.GetComponent<EnemyStats>().priority[i] > best)
            {
                gameObject.GetComponent<EnemyStats>().currentTarget = gameObject.GetComponent<EnemyStats>().Targets[i];
                best = gameObject.GetComponent<EnemyStats>().priority[i];
                slot = i;
            }
           
        }
     
    }

}
