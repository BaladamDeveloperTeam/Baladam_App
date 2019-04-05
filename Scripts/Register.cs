using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;
using System.Linq;
using security;
using Models;
using System.Net.NetworkInformation;
using RestSharp;

public class Register : MonoBehaviour
{
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    //private string API_Url = "http://192.168.88.246:8080/api/v1";
    //private string API_Url = "http://192.168.10.128:8080/api/v1";
    private string API_Url = "http://localhost:8080/api/v1";
    private string ReceivedJson, ReceivedGetJson, Path;
    private INIParser File = new INIParser();

    public RtlText ErrorText, TimerText;
    public InputField Password, Password2, Username, Phone, Code;
    public Toggle AceptRules;
    public GameObject Loading, thisPanel, Login_p, Verifi_p;
    public Button RegesterBtn;
    private GameObject BNC;
    private Global_Script_Manager GSM;
    private SendSMS sendsms = new SendSMS();
    private string VerifiCode;
    private float Second = 60, Min = 1;
    private bool StartTimer = false;

    private string token_csrf;
    public Session LoginSession;
    public Models.User register;

    void Awake()
    {
        RegesterBtn.enabled = false;
        RegesterBtn.interactable = false;
        BNC = GameObject.Find("BottomNav");
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        Path = Application.persistentDataPath + "BaladamAppSettings.ini";
        Debug.Log(API_Url);
    }

    private void Update()
    {
        if (StartTimer == true)
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
        if (AceptRules.isOn == true)
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
        if (Username.text.Length < 5)
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
        web.AddField("phone", Phone.text);
        if(Pushe.GetPusheId() != "No")
            web.AddField("pushe", Pushe.GetPusheId());
        web.AddField("pushe", "test");
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
            else if (data.text == "Register")
            {
                //SignIn signIn = new SignIn();
                //signIn.DoSingInOther(Username.text, Password2.text);
                StartCoroutine(DoSingIn());
            }
        }
        else
            ErrorText.gameObject.SetActive(true);

    }

    private WWWForm SendDataLogin()
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

    private IEnumerator DoSingIn()
    {

        WWWForm WebGet = SendDataLogin();
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
            Coding coding = new Coding();
            Global_Script_Manager.SetLog(11, coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
            //if (RememberMe.isOn == true)
            //{
            //    Coding coding = new Coding();
            //    File.Open(Path);
            //    File.WriteValue("UserSignIn", "IsSignIn", 1);
            //    File.WriteValue("UserSignIn", "SignTime", System.DateTime.Now);
            //    File.WriteValue("UserSignIn", "Username", Username.text);
            //    File.WriteValue("UserSignIn", "Code", coding.Md5Sum(SystemInfo.deviceUniqueIdentifier) + coding.Sha1Sum(Username.text));
            //    File.Close();
            //}

        }
    }

    public void NewRegister()
    {
        Coding coding = new Coding();
        token_csrf = "LATARY@" + coding.Md5Sum(Username.text.ToLower()) + coding.Sha1Sum(Username.text.ToLower());
        var client = new RestClient(API_Url + "/user");
        var request = new RestRequest(Method.POST);
        //request.AddHeader("Postman-Token", "ba5fee2b-f869-40d3-a016-95334ca5d4ec");
        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("X", "true");
        request.AddHeader("X_TOKEN", "DJao310D%jdi!5");
        register.username = Username.text;
        register.pwd = Password.text;
        register.token = token_csrf;
        register.contact.phone = Phone.text;
        register.contact.mail = "jamal@yahoo.com";
        register.profile.firstName = "mohammad";
        register.profile.lastName = "jamali";
        register.profile.melliCode = "2282511931";
        register.profile.creditCard = "6273811101934609";
        //request.AddParameter("undefined", "{\n    \"username\": \"mohada10\",\n    \"pwd\": \"22102210aA\",\n    \"token\": \"123i9(UDJ@!D:DASD\",\n    \"contact\":{\n    \t\"phone\":\"09197279883\",\n    \t\"mail\":\"jamalianm1@yahoo.com\"\n    },\n    \"seller\": false\n}", ParameterType.RequestBody);
        request.AddParameter("User", JsonUtility.ToJson(register), ParameterType.RequestBody);
        Debug.Log(JsonUtility.ToJson(register));
        IRestResponse response = client.Execute(request);
        Debug.Log(response.Content);
    }

    public void DoRegisterBtn()
    {
        Coding coding = new Coding();
        if (VerifiCode == Code.text)
        {
            //StartCoroutine(DoRegister());
            NewRegister();
            Global_Script_Manager.SetLog(19, coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
        }
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
            Verifi_p.gameObject.SetActive(true);
            //if (sendsms.GetCredit() > 0)
            {
                //sendsms.sendSMSRegisterVerification(Phone.text, global::SendSMS.SendKind.Normal);
                //StartCoroutine(sendsms.sendSMSRegisterVerification(System.Convert.ToInt64(Phone.text)));
                //VerifiCode = sendsms.ReadVerfi();
                VerifiCode = "11111";
                Verifi_p.gameObject.SetActive(true);
                StartTimer = true;
            }
        }
        else
        {
            ErrorText.gameObject.SetActive(true);
        }
    }
}
