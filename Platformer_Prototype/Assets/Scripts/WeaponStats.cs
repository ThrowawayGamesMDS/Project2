using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public  enum WeaponType
    {
        PISTOL = 0, SHOTGUN = 1, RIFLE = 2
    }
    public AudioSource src;
    public WeaponType m_eWeaponType;
    public float m_fPower;
    public float m_fFireRate;
    public string m_sWeaponType;
    public int m_iMagCount; // current mag size
    public int m_iMagCap; // the magazine max capacity..
    public int m_iAmmoCount; 
    public int m_iMaximumAmmoCount; // for pickups - if weaponstats.m_iammocount < m_imaximumammocount then give player ammo..
    public string m_sWeaponName;
	// Use this for initialization
	void Start ()
    {
    }

	// Update is called once per frame
	void Update ()
    {

    }
    public void playSound()
    {
        src.Play();
    }
}
