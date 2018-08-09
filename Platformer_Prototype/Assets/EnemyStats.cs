using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    public float m_fEnemyHealth;
    public enum eAIMode { push, attack, other };
    public eAIMode myAIMode;
    public List<GameObject> Targets;
    public List<int> priority;
    public GameObject currentTarget;

    public float attackrange;
    public int attackdamage;
    void Start()
    {
        myAIMode = eAIMode.push;
    }
    void Update()
    {
        if (m_fEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyShot(float damage)
    {
        m_fEnemyHealth -= damage;
    }
 
}
