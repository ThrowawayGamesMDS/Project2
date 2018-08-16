using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class advanceroundUI : MonoBehaviour {
    public CanvasGroup cg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(RoundSystem.isInPlay)
        {
            cg.alpha = 0;
        }
        else
        {
            cg.alpha = 1;
        }
	}
}
