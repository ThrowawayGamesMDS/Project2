using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    /// <summary>
    /// THE DISPLAY THAT THE UI HANDLER IS ADJUSTING! IMPORTANT ASF
    /// </summary>
    public enum Handling
    {
        WEAPON, PLAYER
    }
    public Handling m_eObjective;
    /// <summary>
    /// DISPLAY 1,2,3,4 TEXTS AND UPDATE THEM IF THE MULTITEXT BOOL ISNT ACTIVE..
    /// </summary>
    public Text m_tDisplay1;
    public Text m_tDisplay2;
    public Text m_tDisplay3;
    public Text m_tDisplay4;
    public Text[] m_tDisplay;
    /// <summary>
    ///  ENTER THIS SHIT WHEN THERES MULTIPLE TEXTS LIKE MORE THAN THREE...
    /// </summary>
    public bool m_bMultiText;

    /// <summary>
    /// SOME STUFF FOR WEAPON UI MEM UPDATING TO SAVE INGAME PROCESSING
    /// </summary>
    private string m_sCurrentWeapon;
    private int m_iAmmoCount;
    private int m_iMagazineSize;
	// Use this for initialization
	void Start ()
    {
		if (m_eObjective == Handling.WEAPON)
        {
              m_sCurrentWeapon = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_sWeaponName;
            m_tDisplay1.text = "Weapon: " + m_sCurrentWeapon;
            m_tDisplay2.text = "" + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount + "\\" + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount;
            m_iAmmoCount = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount;
            m_iMagazineSize = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount;
            //m_tDisplay3.text = "Weapon Level: " + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iWeaponLevel;
            //m_tDisplay4.text = "Weapon EXP: " + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_fWeaponEXP;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_bMultiText)
        {

        }
        else
        {
            switch(m_eObjective)
            {
                case Handling.WEAPON:
                    {
                        if (m_sCurrentWeapon != WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_sWeaponName)
                        {
                            m_sCurrentWeapon = WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_sWeaponName;
                            m_tDisplay1.text = "Weapon: " + m_sCurrentWeapon;
                        }
                        if (m_iAmmoCount != WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount
                            || m_iMagazineSize != WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount)
                        {
                            m_tDisplay2.text = "" + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount + "\\" + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount;
                        }
                        if (WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iMagCount == 0)
                        {
                            m_tDisplay2.text = "0 \\" + WeaponHandler.m_gActiveWeapon.GetComponent<WeaponStats>().m_iAmmoCount;
                        }

                        break;
                    }
                default:
                        break;
            }
        }
		
	}
}
