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
    public GameObject MoneyPrefab;
    public float attackrange;
    public int attackdamage;
    void Start()
    {
        myAIMode = eAIMode.push;
        m_fEnemyHealth = PublicStats.gruntHealth;
    }
    void Update()
    {
        if (m_fEnemyHealth <= 0)
        {
            RoundSystem.g_iAliveUnits -= 1;
            Instantiate(MoneyPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void EnemyShot(float damage)
    {
        m_fEnemyHealth -= damage;
    }
 
}




