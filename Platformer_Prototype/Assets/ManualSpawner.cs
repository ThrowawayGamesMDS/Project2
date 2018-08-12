using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSpawner : MonoBehaviour {
    public GameObject Grunt;
    public GameObject Tank;
    public GameObject Kamikazi;
    public float timer;
    public bool isManual;
    private bool hasInvoked;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.K))
        {
            hasInvoked = false;
        }
        if(isManual)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                print(RoundSystem.g_iGruntsThisRound);
                Instantiate(Grunt, transform.position, transform.rotation);
            }
        }
        else
        {
            if(!isManual)
            {
                if(!hasInvoked)
                {
                    hasInvoked = true;
                    StartCoroutine(SpawnTank());
                }
                
            }
        }
	}

    IEnumerator SpawnGrunt()
    {
        for (int i = 0; i < RoundSystem.g_iGruntsThisRound; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(Grunt, transform.position, transform.rotation);
        }
        StartCoroutine(SpawnKamikazi());
        
    }
    IEnumerator SpawnTank()
    {
        for (int i = 0; i < RoundSystem.g_iTanksThisRound; i++)
        {
            yield return new WaitForSeconds(0.9f);
            Instantiate(Tank, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(SpawnGrunt());
    }
    IEnumerator SpawnKamikazi()
    {
        for (int i = 0; i < RoundSystem.g_iKamikaziThisRound; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(Kamikazi, transform.position, transform.rotation);
        }
    }
}
