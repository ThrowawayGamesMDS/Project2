using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldSpaceUI : MonoBehaviour {
    private GameObject PlayerTr;
    public float damping;
    private Canvas canv;
	// Use this for initialization
	void Start () {
        PlayerTr = GameObject.Find("PlayerCentre");
        canv = transform.GetChild(0).gameObject.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        int layermask = LayerMask.GetMask("upgrade");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 4, layermask))
        {
            transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = false;
        }
        
        Vector3 lookpos = PlayerTr.transform.position - transform.position;
        //lookpos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
