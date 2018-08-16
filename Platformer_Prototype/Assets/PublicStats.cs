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
    public static int gruntDPS;
    public static int tankDPS;
    public static int kamikazDPS;
    public static float gruntAS;
    public static float tankAS;
    public static float FlameBallDamage;
    public static float FlameBallRadius;
    public static float boltDamage;
    

    public float startingMoney;
	// Use this for initialization
	void Start () {
        g_fResourceCount = startingMoney;
        gruntHealth = 100;
        tankHealth = 300;
        kamikaziHealth = 40;
        EnemySpeed = 3;
        kamikaziSpeed = 6;
        gruntDPS = 10;
        tankDPS = 33;
        kamikazDPS = 75;
        gruntAS = 1.2f;
        tankAS = 2.5f;
        FlameBallDamage = 70;
        FlameBallRadius = 4;
        boltDamage = 50;
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
