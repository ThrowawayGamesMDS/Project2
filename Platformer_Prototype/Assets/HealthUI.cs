using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public RectTransform tr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tr.sizeDelta = new Vector2((366f/100f)*endzone.health, 22);
	}
}
