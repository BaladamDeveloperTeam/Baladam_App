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
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public RtlText ErrorText, TimerText;
    public InputField Password, Password2, Username, Phone, Code;
    public Toggle AceptRules;
    public GameObject Loading, thisPanel, Login_p, Verifi_p;
    public Button RegesterBtn;
    private GameObject BNC;
    private SendSMS sendsms = new SendSMS();
    private string VerifiCode;
    private float Second = 60, Min = 1;
    private bool StartTimer = false;

    void Awake()
    {
        RegesterBtn.enabled = false;
        RegesterBtn.interactable = false;
        BNC = GameObject.Find("BottomNav");
    }

    private void Update()
    {
        if(StartTimer == true)
        {
            Second -= Time.deltaTime;
            if (Second <= 0 && Min >= 1)
            {
                Min--;
                Second = 60;
            }
            TimerText.text = "زمان اعتبار کد : " + Min.ToString() + ":" + ((int)Second).ToString();
            if (Min <= 0 && Second <= 0)
                StartTimer = false;
        }
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
        if(Username.text.Length < 5)
        {
            ErrorText.text = "نام کاربری باید حداقل دارای 5 حرف باشد.";
            return false;
        }
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
        web.AddField("user", Username.text);
        web.AddField("pass", coding.Md5Sum(Password.text));
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
            else if(data.text == "Register")
            {
                SignIn signIn = new SignIn();
                signIn.DoSingInOther(Username.text, Password2.text);
            }
        }
        else
            ErrorText.gameObject.SetActive(true);

    }

    public void DoRegisterBtn()
    {
        Coding coding = new Coding();
        if (VerifiCode == coding.Md5Sum(Code.text))
            StartCoroutine(DoRegister());
        else
            Debug.Log("Worng Code");
    }

    public void GoToLoginBtn()
    {
        thisPanel.gameObject.SetActive(false);
        Login_p.gameObject.SetActive(true);
    }

    public void SendSMS()
    {
        if (CheckPassword() == true)
        {
            //sendsms.sendSMSRegisterVerification(Phone.text);
            StartCoroutine(sendsms.sendSMSRegisterVerification(System.Convert.ToInt64(Phone.text)));
            VerifiCode = sendsms.ReadVerfi();
            Verifi_p.gameObject.SetActive(true);
            StartTimer = true;
        }
        else
        {
            ErrorText.gameObject.SetActive(true);
        }
    }
}
