using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicStats : MonoBehaviour {
    [SerializeField] public static float g_fResourceCount;
    public float startingMoney;
	// Use this for initialization
	void Start () {
        g_fResourceCount = startingMoney;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
