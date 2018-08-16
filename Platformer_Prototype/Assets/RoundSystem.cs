using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour {
    public static int g_fRound;
    public static int g_iGruntsThisRound;
    public static int g_iTanksThisRound;
    public static int g_iKamikaziThisRound;
    public static int g_iRoundUnits;
    public static int g_iAliveUnits;
    public GameObject Spawner;
    int timer=30*60;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        g_fRound = 1;
        g_iGruntsThisRound = 10;
        g_iTanksThisRound = 0;
        g_iKamikaziThisRound = 0;
        g_iRoundUnits = 10;
        g_iAliveUnits = 10;
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)||(timer == 0))
        {
            timer = 4000;
            //print(g_iAliveUnits);
            //if (g_iAliveUnits <= 0)
            //{
            AdvanceRound();
                Spawner.GetComponent<ManualSpawner>().hasInvoked = false;
            //}
        }
        else
        {
            timer--;
        }
        

    }

    public void AdvanceRound()
    {
        g_fRound += 1;

        PublicStats.EnemySpeed += PublicStats.EnemySpeed / 20;
        
        PublicStats.gruntHealth += PublicStats.gruntHealth / 10;
        PublicStats.gruntAS += PublicStats.gruntAS / 20;
        PublicStats.gruntDPS += PublicStats.gruntDPS / 20;

        PublicStats.tankHealth += PublicStats.tankHealth / 10;
        PublicStats.tankAS += PublicStats.tankAS / 20;
        PublicStats.tankDPS += PublicStats.tankDPS / 20;

        PublicStats.kamikaziHealth += PublicStats.kamikaziHealth / 10;
        PublicStats.kamikazDPS += PublicStats.kamikazDPS / 20;

        g_iGruntsThisRound += Mathf.FloorToInt((g_iGruntsThisRound / 3));
        g_iTanksThisRound += Mathf.FloorToInt((g_iTanksThisRound / 2) + 1);
        g_iKamikaziThisRound += Mathf.FloorToInt((g_iKamikaziThisRound / 3) + 1);
        g_iRoundUnits = g_iGruntsThisRound + g_iTanksThisRound + g_iKamikaziThisRound;
        g_iAliveUnits = g_iRoundUnits;
    }
}
