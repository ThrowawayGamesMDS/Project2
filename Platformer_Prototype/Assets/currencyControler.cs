using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currencyControler : MonoBehaviour {
    private bool isAttracted;
    GameObject player;
    private int value;
    
	// Use this for initialization
	void Start () {
        isAttracted = false;
        player = GameObject.Find("PlayerCentre");
        value = (100 * RoundSystem.g_fRound);
	}
	
	// Update is called once per frame
	void Update () {
		if(isAttracted)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = player.transform.position - rayOrigin;
            RaycastHit hit;
            int layermask = LayerMask.GetMask("player");
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, 100, layermask))
            {
                
                if(hit.distance <= 1)
                {
                    hit.transform.SendMessage("Pickup", value);
                    Destroy(gameObject);
                }
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAttracted = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
