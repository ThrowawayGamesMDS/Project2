using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {
    public float missileSpeed;
    public AudioSource audiosrc;
    public MeshRenderer mr;
    public float radius;
    public float power;
    public GameObject playerObj;
    public GameObject explosion;
    public float playerImpactStrength;
    [SerializeField] private bool isMoving;
	// Use this for initialization
	void Start () {
        isMoving = true;
        playerObj = GameObject.Find("playercentre");
        Invoke("Explode", 4);
	}
	
	// Update is called once per frame
	void Update () {
        if(isMoving == true)
        {
            transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
        }

	}
    void OnCollisionEnter(Collision other)
    {
        Explode();
    }



    void Explode()
    {
        Invoke("DestroySelf", 2);
        Destroy(transform.GetChild(0).gameObject);
        print("Collide");
        isMoving = false;
        missileSpeed = 0;
        audiosrc.enabled = false;
        mr.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = playerObj.transform.position - transform.position;
        RaycastHit rayhit;
        if (Physics.Raycast(transform.position, rayDirection, out rayhit, 100, 1 << 8))
        {
            Debug.Log(rayhit.distance);
            if (rayhit.distance < 6)
            {
                //create explosion
            }
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

            }

        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }


}
