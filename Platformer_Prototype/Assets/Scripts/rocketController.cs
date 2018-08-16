using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour {
    public float missileSpeed;
    public AudioSource audiosrc;
    public float radius;
    private float damage;
    public GameObject explosion;
    [SerializeField] private bool isMoving;

	void Start () {
        isMoving = true;
        Invoke("Explode", 4);
        radius = PublicStats.FlameBallRadius;
        damage = PublicStats.FlameBallDamage;
	}
	
	void Update () {
        if(isMoving == true)
        {
            transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag != "Turret")
        {
            Explode();
        }
    }
    

    void Explode()
    {
        Invoke("DestroySelf", 2);
        Destroy(transform.GetChild(0).gameObject);
        isMoving = false;
        missileSpeed = 0;
        audiosrc.enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if(hit.tag == "Enemy")
            {
                hit.SendMessage("EnemyShot", damage);
            }
        }
    }

    void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }


}
