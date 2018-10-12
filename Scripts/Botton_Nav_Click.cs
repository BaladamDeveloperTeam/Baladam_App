﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botton_Nav_Click : MonoBehaviour
{

    public Color ClickedColor = Color.blue, unClickedColor = Color.gray;
    public Image HomeImg, ListImg, LearnImg, SearchImg, ProfileImg, LoginImg;
    public GameObject Home_p, List_p, Learn_p, Search_p, Profile_p, Login_p, Profile_n, Login_n;
    private int State = 0;
    private GameObject ManageT;
    private string Path;
    private INIParser File = new INIParser();

    void Awake()
    {
        Path = Application.persistentDataPath + "BaladamAppSettings.ini";
    }

    void Start()
    {
        ManageT = GameObject.Find("Panels");
        if(ManageT.gameObject.GetComponent<ManageTheme>().EnableDarkTheme == false)
        {
            ClickedColor = ManageT.gameObject.GetComponent<ManageTheme>().ClickedColorLight;
            unClickedColor = ManageT.gameObject.GetComponent<ManageTheme>().unClickedColorLight;
        }
        else
        {
            ClickedColor = ManageT.gameObject.GetComponent<ManageTheme>().ClickedColorDark;
            unClickedColor = ManageT.gameObject.GetComponent<ManageTheme>().unClickedColorDark;
        }
        HomeImg.color = ClickedColor;
        //LocalNotification.ClearNotifications();
        File.Open(Path);
        if(File.ReadValue("UserSignIn", "IsSignIn", 0) == 0)
        {
            Login_n.gameObject.SetActive(true);
            Profile_n.gameObject.SetActive(false);
        }
        else
        {
            Login_n.gameObject.SetActive(false);
            Profile_n.gameObject.SetActive(true);
        }
        File.Close();
    }

    public void Home_nClick()
    {
        State = 0;
        ChangeColor(State);
        Show_p(State);
    }

    public void List_nClick()
    {
        State = 1;
        ChangeColor(State);
        Show_p(State);
    }

    public void Learn_nClick()
    {
        State = 2;
        ChangeColor(State);
        Show_p(State);
        //LocalNotification.SendNotification(1, 5000, "بلدم", "یک درخواست برای شما دریافت شد", new Color32(0xff, 0x44, 0x44, 255), true, true, true, "app_icon");
    }

    public void Profile_nClick()
    {
        State = 3;
        ChangeColor(State);
        Show_p(State);
        Login_n.gameObject.SetActive(false);
        Profile_n.gameObject.SetActive(true);
    }

    public void Search_nClick()
    {
        State = 4;
        ChangeColor(State);
        Show_p(State);
        //LocalNotification.SendNotification(1, 5000, "بلدم", "یک درخواست برای شما دریافت شد", new Color32(0xff, 0x44, 0x44, 255));
    }

    public void Login_nClick()
    {
        State = 5;
        ChangeColor(State);
        Show_p(State);
    }

    void ChangeColor(int Item)
    {
        switch(Item)
        {
            case 0:
                HomeImg.color = ClickedColor;
                ListImg.color = unClickedColor;
                LearnImg.color = unClickedColor;
                SearchImg.color = unClickedColor;
                ProfileImg.color = unClickedColor;
                LoginImg.color = unClickedColor;
                break;
            case 1:
                HomeImg.color = unClickedColor;
                ListImg.color = ClickedColor;
                LearnImg.color = unClickedColor;
                SearchImg.color = unClickedColor;
                ProfileImg.color = unClickedColor;
                LoginImg.color = unClickedColor;
                break;
            case 2:
                HomeImg.color = unClickedColor;
                ListImg.color = unClickedColor;
                LearnImg.color = ClickedColor;
                SearchImg.color = unClickedColor;
                ProfileImg.color = unClickedColor;
                LoginImg.color = unClickedColor;
                break;
            case 3:
                HomeImg.color = unClickedColor;
                ListImg.color = unClickedColor;
                LearnImg.color = unClickedColor;
                SearchImg.color = unClickedColor;
                ProfileImg.color = ClickedColor;
                LoginImg.color = unClickedColor;
                break;
            case 4:
                HomeImg.color = unClickedColor;
                ListImg.color = unClickedColor;
                LearnImg.color = unClickedColor;
                SearchImg.color = ClickedColor;
                ProfileImg.color = unClickedColor;
                LoginImg.color = unClickedColor;
                break;
            case 5:
                HomeImg.color = unClickedColor;
                ListImg.color = unClickedColor;
                LearnImg.color = unClickedColor;
                SearchImg.color = unClickedColor;
                ProfileImg.color = unClickedColor;
                LoginImg.color = ClickedColor;
                break;
        }
    }

    void Show_p(int Item)
    {
        switch (Item)
        {
            case 0:
                Home_p.gameObject.SetActive(true);
                List_p.gameObject.SetActive(false);
                Learn_p.gameObject.SetActive(false);
                Search_p.gameObject.SetActive(false);
                Profile_p.gameObject.SetActive(false);
                Login_p.gameObject.SetActive(false);
                break;
            case 1:
                Home_p.gameObject.SetActive(false);
                List_p.gameObject.SetActive(true);
                Learn_p.gameObject.SetActive(false);
                Search_p.gameObject.SetActive(false);
                Profile_p.gameObject.SetActive(false);
                Login_p.gameObject.SetActive(false);
                break;
            case 2:
                Home_p.gameObject.SetActive(false);
                List_p.gameObject.SetActive(false);
                Learn_p.gameObject.SetActive(true);
                Search_p.gameObject.SetActive(false);
                Profile_p.gameObject.SetActive(false);
                Login_p.gameObject.SetActive(false);
                break;
            case 3:
                Home_p.gameObject.SetActive(false);
                List_p.gameObject.SetActive(false);
                Learn_p.gameObject.SetActive(false);
                Search_p.gameObject.SetActive(false);
                Profile_p.gameObject.SetActive(true);
                Login_p.gameObject.SetActive(false);
                break;
            case 4:
                Home_p.gameObject.SetActive(false);
                List_p.gameObject.SetActive(false);
                Learn_p.gameObject.SetActive(false);
                Search_p.gameObject.SetActive(true);
                Profile_p.gameObject.SetActive(false);
                Login_p.gameObject.SetActive(false);
                break;
            case 5:
                Home_p.gameObject.SetActive(false);
                List_p.gameObject.SetActive(false);
                Learn_p.gameObject.SetActive(false);
                Search_p.gameObject.SetActive(false);
                Profile_p.gameObject.SetActive(false);
                Login_p.gameObject.SetActive(true);
                break;
        }
    }
}
