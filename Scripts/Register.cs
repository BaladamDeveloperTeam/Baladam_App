﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;
using System.Linq;
using security;

public class Register : MonoBehaviour
{
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public RtlText ErrorText;
    public InputField Password, Password2, Username;
    public Toggle AceptRules;
    public GameObject Loading;
    public Button RegesterBtn;

    void Awake()
    {
        RegesterBtn.enabled = false;
        RegesterBtn.interactable = false;
    }

    public void ActiveBtn()
    {
        if(AceptRules.isOn == true)
        {
            RegesterBtn.enabled = true;
            RegesterBtn.interactable = true;
        }
        else
        {
            RegesterBtn.enabled = false;
            RegesterBtn.interactable = false;
        }
    }

    public bool CheckPassword()
    {
        if (Password.text.Length < 8)
        {
            ErrorText.text = "کلمه عبور باید حداقل دارای 8 حرف باشد.";
            return false;
        }
        else
        {
            if (Password.text == Password2.text)
            {
                if (Password.text.Any(char.IsLower) && Password2.text.Any(char.IsLower) &&
                    Password.text.Any(char.IsUpper) && Password2.text.Any(char.IsUpper))
                {
                    return true;
                }
                else
                    ErrorText.text = "کلمه عبور باید شامل عدد و حرف کوچک و بزرگ باشد.";
            }
            else
                ErrorText.text = "کلمه های عبور وارد شده یکسان نمی باشد.";
            return false;
        }
    }

    private WWWForm SendData()
    {
        Coding coding = new Coding();
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 6);
        web.AddField("username", Username.text);
        web.AddField("password", coding.Md5Sum(Password.text));
        return web;
    }

    private IEnumerator DoRegister()
    {
        if (CheckPassword() == true)
        {
            WWWForm WebGet = SendData();
            WWW data = new WWW(Url, WebGet);
            Loading.gameObject.SetActive(true);
            yield return data;
            Loading.gameObject.SetActive(false);

            Debug.Log(data.text);
            if (data.text == "user" || data.text == "" || data.text == null ||
                data.text.Contains("<!DOCTYPE html>") || data.text == "duplucate")
                ErrorText.gameObject.SetActive(true);
        }
        else
            ErrorText.gameObject.SetActive(true);

    }

    public void DoRegisterBtn()
    {
        StartCoroutine(DoRegister());
    }
}