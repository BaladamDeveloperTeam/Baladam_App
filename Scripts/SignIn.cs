using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UnityEngine.UI;
using security;

public class SignIn : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    public string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string ReceivedJson, Path;
    INIParser File = new INIParser();
    public UserInfo[] userinfo;
    public RtlText Username, ErrorText;
    public InputField Password;
    public GameObject Loading;

    private string token_csrf;

    void Awake()
    {
        Path = Application.persistentDataPath + "BaladamAppSettings.ini";
    }

    private WWWForm SendData()
    {
        Coding coding = new Coding();
        token_csrf = "LATARY@" + coding.Md5Sum(Username.text.ToLower()) + coding.Sha1Sum(Username.text.ToLower());
        Debug.Log(token_csrf);

        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 5);
        web.AddField("user", Username.text);
        web.AddField("pass", coding.Md5Sum(Password.text));
        web.AddField("token_csrf", token_csrf);
        return web;
    }

    private IEnumerator DoSingIn()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        Loading.gameObject.SetActive(true);
        yield return data;
        Loading.gameObject.SetActive(false);

        ReceivedJson = data.text;
        Debug.Log(data.text);
        if (data.text == "Wrong" || data.text == "" || data.text == null)
            ErrorText.gameObject.SetActive(true);
        else
        {
            userinfo = JsonHelper.FromJson<UserInfo>("{\"Items\": " + ReceivedJson + "}");
            File.Open(Path);
            File.WriteValue("UserSignIn", "IsSignIn", 1);
            File.WriteValue("UserSignIn", "SignTime", Time.time);
            File.WriteValue("UserSignIn", "Username", Username.text);
            File.Close();
        }

    }

    public void DoSingInBtn()
    {
        StartCoroutine(DoSingIn());
    }
}
