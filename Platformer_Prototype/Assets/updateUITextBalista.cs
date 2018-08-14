using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateUITextBalista : MonoBehaviour {
    public Text title;
    public Text price;
    // Use this for initialization
    void Update()
    {
        switch (transform.parent.transform.parent.GetChild(0).GetComponent<BallistaTurret>().myTurretLvl)
        {
            case (BallistaTurret.eTurretLevel.First):
                {
                    title.text = "1";
                    price.text = "50 Gems";
                    break;
                }
            case (BallistaTurret.eTurretLevel.Second):
                {
                    title.text = "2";
                    price.text = "100 Gems";
                    break;
                }
            case (BallistaTurret.eTurretLevel.Third):
                {
                    title.text = "3";
                    price.text = "150 Gems";
                    break;
                }
            case (BallistaTurret.eTurretLevel.Fourth):
                {
                    title.text = "4";
                    price.text = "Max Level";
                    break;
                }

        }
    }
}
