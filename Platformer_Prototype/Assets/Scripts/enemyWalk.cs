using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyWalk : MonoBehaviour {
    public GameObject des;
    public float speed;
    public float dis;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (des != null)
        {
            // if (Vector3.Distance(transform.position, des.transform.position) < 1.0f)
            //{
            //    Destroy(gameObject);

            //}
            agent.SetDestination(des.transform.position);

            Vector3 raycastDir = des.transform.position - transform.position;
            raycastDir.y = raycastDir.y + 1.05f;

            RaycastHit hit;
            Ray workingray = new Ray(transform.position, raycastDir);

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
	}
}
