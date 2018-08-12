using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour {
    public static float g_fRound;
    public static int g_iGruntsThisRound;
    public static int g_iTanksThisRound;
    public static int g_iKamikaziThisRound;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        g_fRound = 1;
        g_iGruntsThisRound = 10;
        g_iTanksThisRound = 0;
        g_iKamikaziThisRound = 0;
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AdvanceRound();
        }
    }

    public void AdvanceRound()
    {
        g_fRound += 1;
        g_iGruntsThisRound += (g_iGruntsThisRound / 3);
        g_iTanksThisRound += ((g_iTanksThisRound / 2) + 1);
        g_iKamikaziThisRound += ((g_iKamikaziThisRound / 3) + 1);
    }
}
