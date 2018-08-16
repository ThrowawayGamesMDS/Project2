using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceUIHandler : MonoBehaviour
{
    public float m_fDamping;
    private GameObject m_goTarget;
    private Canvas canv;
    // Use this for initialization
    void Start ()
    {
        m_goTarget = GameObject.Find("PlayerCentre");
        canv = transform.GetChild(0).gameObject.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 lookpos = m_goTarget.transform.position - transform.position;
        //lookpos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_fDamping);
        //Vector3 targetDir = m_goTarget.transform.position - transform.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5.0f, 0.0f);
        // gameObject.transform.rotation = Quaternion.LookRotation(newDir);
        //gameObject.transform.TransformDirection(m_goTarget.transform.position);
    }
}
