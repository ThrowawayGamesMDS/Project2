using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateUIText : MonoBehaviour {
    public Text title;
    public Text price;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(transform.parent.transform.parent.GetChild(0).GetComponent<RocketTurret>().myTurretLvl)
        {
            case (RocketTurret.eTurretLevel.First):
                {
                    title.text = "1";
                    price.text = "50 Gems";
                    break;
                }
            case (RocketTurret.eTurretLevel.Second):
                {
                    title.text = "2";
                    price.text = "100 Gems";
                    break;
                }
            case (RocketTurret.eTurretLevel.Third):
                {
                    title.text = "3";
                    price.text = "150 Gems";
                    break;
                }
            case (RocketTurret.eTurretLevel.Fourth):
                {
                    title.text = "4";
                    price.text = "Max Level";
                    break;
                }

        }
	}
}
