using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using security;

public class SignIn : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    public string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public RtlText Username, Password;

    public string token_csrf;

    private WWWForm SendData()
    {
        Coding coding = new Coding();
        token_csrf = "LATARY@" + coding.Md5Sum(Username.text) + coding.Sha1Sum(Username.text);
        Debug.Log(token_csrf);

        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", coding.Md5Sum("5"));
        web.AddField("user", Username.text);
        web.AddField("pass", coding.Md5Sum(Password.text));
        web.AddField("token_csrf", token_csrf);
        return web;
    }

    private IEnumerator DoSingIn()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);

    }

    public void DoSingInBtn()
    {
        StartCoroutine(DoSingIn());
    }
}
