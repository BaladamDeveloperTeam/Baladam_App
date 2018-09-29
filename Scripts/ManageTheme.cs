using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTheme : MonoBehaviour
{

    public bool EnableDarkTheme = false;

    [Header("Light Theme")]
    public Color ClickedColorLight;
    public Color unClickedColorLight;
    public Color BackGroundLight;
    public Color HederLight;
    public Color BottomNavLight = Color.white;

    [Header("Dark Theme")]
    public Color ClickedColorDark;
    public Color unClickedColorDark;
    public Color BackGroundDark;
    public Color HederDark;
    public Color BottomNavDark;

    [Header("Other Objects")]
    public GameObject Mycamera;
    public GameObject[] Pages;
    public GameObject BottomNav;
    public GameObject Header;
    public Text[] Texts;

    void Start ()
    {
        //Pages = GameObject.FindGameObjectsWithTag("Pages");
        Texts = (Text[])FindObjectsOfType(typeof(Text));

		if(EnableDarkTheme == false)
        {
            Mycamera.gameObject.GetComponent<Camera>().backgroundColor = BackGroundLight;
            for(int i = 0; i < Pages.Length; i++)
            {
                Pages[i].gameObject.GetComponent<Image>().color = BackGroundLight;
            }
            BottomNav.gameObject.GetComponent<Image>().color = BottomNavLight;
            Header.gameObject.GetComponent<Image>().color = HederLight;
            for (int i = 0; i < Texts.Length; i++)
            {
               // Texts[i].color = Color.black;
            }
        }
        else
        {
            Mycamera.gameObject.GetComponent<Camera>().backgroundColor = BackGroundDark;
            for (int i = 0; i < Pages.Length; i++)
            {
                Pages[i].gameObject.GetComponent<Image>().color = BackGroundDark;
            }
            BottomNav.gameObject.GetComponent<Image>().color = BottomNavDark;
            Header.gameObject.GetComponent<Image>().color = HederDark;
            for(int i = 0; i < Texts.Length; i++)
            {
                Texts[i].color = Color.white;
            }
        }
	}


	void Update ()
    {
		
	}
}
