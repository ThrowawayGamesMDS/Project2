using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    public float m_fEnemyHealth;

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
