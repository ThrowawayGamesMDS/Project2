using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateUIText : MonoBehaviour {
    public Text title;
    public Text price;
	
	// Update is called once per frame
	void Update () {
		switch(transform.parent.transform.parent.GetComponent<RocketTurret>().myTurretLvl)
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
