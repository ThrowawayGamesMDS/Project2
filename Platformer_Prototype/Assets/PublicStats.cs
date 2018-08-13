using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicStats : MonoBehaviour {
    [SerializeField] public static float g_fResourceCount;
    public static int gruntHealth;
    public static int tankHealth;
    public static int kamikaziHealth;
    public static float EnemySpeed;
    public static float kamikaziSpeed;

    public float startingMoney;
	// Use this for initialization
	void Start () {
        g_fResourceCount = startingMoney;
        gruntHealth = 100;
        tankHealth = 300;
        kamikaziHealth = 40;
        EnemySpeed = 3;
        kamikaziSpeed = 6;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
