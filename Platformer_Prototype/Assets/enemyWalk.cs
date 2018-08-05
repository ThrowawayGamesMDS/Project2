using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyWalk : MonoBehaviour {
    public GameObject des;
    public float speed;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(des.transform.position);
	}
}
