using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class waveUi : MonoBehaviour
{
    public bool wave;
    // Update is called once per frame
    void Update()
    {
        if(wave)
        {
            gameObject.GetComponent<Text>().text = RoundSystem.g_fRound.ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text = RoundSystem.g_iAliveUnits.ToString();
        }
        
    }
}

