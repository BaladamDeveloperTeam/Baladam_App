using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UnityEngine.UI;
using security;
using System.Net.NetworkInformation;

public class SignIn : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string ReceivedJson, ReceivedGetJson, Path;
    private INIParser File = new INIParser();
    public RtlText Username, ErrorText;
    public InputField Password;
    public Toggle RememberMe;
    public GameObject Loading, thisPanel, Profile_p, Register_p;
    private GameObject BNC;
    private Global_Script_Manager GSM;
    public Session LoginSession;
    private Session[] OldSession;
    public static string IsSeller;

    private string token_csrf;

    void Awake()
    {
        Path = Application.persistentDataPath + "BaladamAppSettings.ini";
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        BNC = GameObject.Find("BottomNav");
    }

    private WWWForm SendData()
    {
        Coding coding = new Coding();
        token_csrf = "LATARY@" + coding.Md5Sum(Username.text.ToLower()) + coding.Sha1Sum(Username.text.ToLower());
        Debug.Log(token_csrf);
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        //do what you want with the IP here... add it to a list, just get the first and break out. Whatever.
                        Debug.Log(ip.Address.ToString());
                    }
                }
            }
        }
        LoginSession.name = coding.Md5Sum(SystemInfo.deviceUniqueIdentifier);
        LoginSession.log.address = IPManager.GetIP(ADDRESSFAM.IPv4);
        LoginSession.log.Device.DeviceModel = SystemInfo.deviceModel;
        LoginSession.log.Device.DeviceUsername = SystemInfo.deviceName;
        LoginSession.log.Device.DeviceType = SystemInfo.deviceType.ToString();
        LoginSession.log.Device.IMEI = SystemInfo.deviceUniqueIdentifier;

        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 5);
        web.AddField("user", Username.text);
        web.AddField("pass", coding.Md5Sum(Password.text));
        web.AddField("token_csrf", token_csrf);
        web.AddField("session", JsonUtility.ToJson(LoginSession));
        return web;
    }

    private WWWForm SendData(string username, string password)
    {
        Coding coding = new Coding();
        token_csrf = "LATARY@" + coding.Md5Sum(username.ToLower()) + coding.Sha1Sum(password.ToLower());
        Debug.Log(token_csrf);
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        //do what you want with the IP here... add it to a list, just get the first and break out. Whatever.
                        Debug.Log(ip.Address.ToString());
                    }
                }
            }
        }
        LoginSession.name = coding.Md5Sum(SystemInfo.deviceUniqueIdentifier);
        LoginSession.log.address = IPManager.GetIP(ADDRESSFAM.IPv4);
        LoginSession.log.Device.DeviceModel = SystemInfo.deviceModel;
        LoginSession.log.Device.DeviceUsername = SystemInfo.deviceName;
        LoginSession.log.Device.DeviceType = SystemInfo.deviceType.ToString();
        LoginSession.log.Device.IMEI = SystemInfo.deviceUniqueIdentifier;

        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 5);
        web.AddField("user", username);
        web.AddField("pass", coding.Md5Sum(password));
        web.AddField("token_csrf", token_csrf);
        web.AddField("session", JsonUtility.ToJson(LoginSession));
        return web;
    }

    private IEnumerator DoSingIn(int mode, string username, string password)
    {
        if (mode == 1)
        {
            WWWForm WebGet = SendData(username, password);
            WWW data = new WWW(Url, WebGet);
            Loading.gameObject.SetActive(true);
            yield return data;
            Loading.gameObject.SetActive(false);

            ReceivedJson = data.text;
            Debug.Log(data.text);
            if (data.text == "Wrong" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
                ErrorText.gameObject.SetActive(true);
            else
            {
                BNC.gameObject.GetComponent<Botton_Nav_Click>().Profile_nClick();
                GSM.SetUserInfo(JsonHelper.FromJson<UserInfo>("{\"Items\": [ " + ReceivedJson + " ] }"));
                IsSeller = GSM.ReadIsSeller();
                Coding coding = new Coding();
                Global_Script_Manager.SetLog(11, coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
                if (RememberMe.isOn == true)
                {
                    File.Open(Path);
                    File.WriteValue("UserSignIn", "IsSignIn", 1);
                    File.WriteValue("UserSignIn", "SignTime", System.DateTime.Now);
                    File.WriteValue("UserSignIn", "Username", username);
                    File.WriteValue("UserSignIn", "Code", coding.Md5Sum(SystemInfo.deviceUniqueIdentifier) + coding.Sha1Sum(username));
                    File.Close();
                }

            }
        }
        else
        {
            WWWForm WebGet = SendData();
            WWW data = new WWW(Url, WebGet);
            Loading.gameObject.SetActive(true);
            yield return data;
            Loading.gameObject.SetActive(false);

            ReceivedJson = data.text;
            Debug.Log(data.text);
            if (data.text == "Wrong" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
                ErrorText.gameObject.SetActive(true);
            else
            {
                BNC.gameObject.GetComponent<Botton_Nav_Click>().Profile_nClick();
                GSM.SetUserInfo(JsonHelper.FromJson<UserInfo>("{\"Items\": [ " + ReceivedJson + " ] }"));
                IsSeller = GSM.ReadIsSeller();
                Coding coding = new Coding();
                Global_Script_Manager.SetLog(11, coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
                if (RememberMe.isOn == true)
                {
                    File.Open(Path);
                    File.WriteValue("UserSignIn", "IsSignIn", 1);
                    File.WriteValue("UserSignIn", "SignTime", System.DateTime.Now);
                    File.WriteValue("UserSignIn", "Username", Username.text);
                    File.WriteValue("UserSignIn", "Code", coding.Md5Sum(SystemInfo.deviceUniqueIdentifier) + coding.Sha1Sum(Username.text));
                    File.Close();
                }

            }
        }

    }

    //private WWWForm GetSession()
    //{ 
    //    WWWForm web = new WWWForm();
    //    web.AddField("Master", masterKey);
    //    web.AddField("Chooser", 9);
    //    web.AddField("user", Username.text);
    //    return web;
    //}

    //private IEnumerator DoGet()
    //{
    //    WWWForm Get = GetSession();
    //    WWW data = new WWW(Url, Get);

    //    yield return data;

    //    ReceivedGetJson = data.text;
    //    GSM.gameObject.GetComponent<Global_Script_Manager>().SetOldSession(JsonHelper.FromJson<Session>("{\"Items\": [ " + ReceivedGetJson + " ] }"));
    //}

    public void DoSingInBtn()
    {
        StartCoroutine(DoSingIn(0, "", ""));
    }

    public void DoSingInOther(string username, string password)
    {
        StartCoroutine(DoSingIn(1, username, password));
    }

    public void GoToRegisterBtn()
    {
        thisPanel.gameObject.SetActive(false);
        Register_p.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
