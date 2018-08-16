using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrencyUI : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = PublicStats.g_fResourceCount.ToString();
	}
}
