using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;

public class Login : MonoBehaviour
{

    public string serverMainName = "localhost";
    public string serverUserName = "root";
    public string serverPassword = "";
    public string serverDatabase = "Fivver";
    public string serverKeycode = "defaultkey1230";

    ///////////////////////////////////////////////

    public string url_setValue = "http://localhost/fivver/App/InsertValue.php";

    ///////////////////////////////////////////////

    public InputField set_Username, set_Password;
    public RtlText Status;

    private WWWForm StablishedServer()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("serverMainName", serverMainName);
        webForm.AddField("serverUserName", serverUserName);
        webForm.AddField("serverPassword", serverPassword);
        webForm.AddField("serverDatabase", serverDatabase);
        webForm.AddField("serverKeycode", serverKeycode);
        return webForm;
    }


    private IEnumerator Registration()
    {
        WWWForm setForm = StablishedServer();
        setForm.AddField("set_username", set_Username.text);
        setForm.AddField("set_Password", set_Password.text);

        WWW database = new WWW(url_setValue, setForm);
        yield return database;

        Status.text = database.text;
        Debug.Log(database.text);
    }

    public void User_register()
    {
        StartCoroutine(Registration());
    }
}
