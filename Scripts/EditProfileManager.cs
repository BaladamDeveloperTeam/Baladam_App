using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;
using System.Linq;
using security;

public class EditProfileManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public InputField Email, PhoneNumber, Bio, Gigs, FullName, Shaba, Melli, EducationD, City, Address, NewPassword, NewPasswordConferm, Password;
    public Dropdown Age, Sex;
    public RtlText BioText, GigsText;
    public GameObject PassMessage;
    public Color Pass, Faild;
    private GameObject GSM;
    private string param, value;
    public ParamList ParamsList;

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
    }

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 15);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("param", JsonUtility.ToJson(ParamsList));
        return web;
    }

    private WWWForm SendDataNewPassword()
    {
        Coding coding = new Coding();
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 10);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("pass", coding.Md5Sum(Password.text));
        web.AddField("new", coding.Md5Sum(NewPasswordConferm.text));
        return web;
    }

    private IEnumerator DoEdit(int item)
    {
        switch (item)
        {
            case 1:
                WWWForm WebGet = SendData();
                WWW data = new WWW(Url, WebGet);
                yield return data;

                Debug.Log(data.text);
                break;
            case 2:
                WWWForm WebGetPass = SendDataNewPassword();
                WWW dataPass = new WWW(Url, WebGetPass);
                yield return dataPass;

                Debug.Log(dataPass.text);
                if (dataPass.text == "Password Restarted")
                {
                    PassMessage.gameObject.SetActive(true);
                    PassMessage.gameObject.GetComponent<RtlText>().color = Pass;
                }
                else
                {
                    PassMessage.gameObject.SetActive(true);
                    PassMessage.gameObject.GetComponent<RtlText>().text = "تغییر کلمه پسور با مشکل مواجه شده است.";
                    PassMessage.gameObject.GetComponent<RtlText>().color = Faild;
                }
                break;
        }
    }

    public void DoEditCaller(int itema)
    {
        try
        {
            switch (itema)
            {
                case 0:
                    if (ParamsList.Params.Exists(o => o.Key == "name"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "name"));
                    ParamsList.Params.Add(new Param() { Key = "name", Value = FullName.text });
                    break;
                case 1:
                    if (ParamsList.Params.Exists(o => o.Key == "bio"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "bio"));
                    ParamsList.Params.Add(new Param() { Key = "bio", Value = Bio.text });
                    break;
                case 2:
                    if (ParamsList.Params.Exists(o => o.Key == "major_field"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "major_field"));
                    ParamsList.Params.Add(new Param() { Key = "major_field", Value = Gigs.text });
                    break;
                case 3:
                    if (ParamsList.Params.Exists(o => o.Key == "email"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "email"));
                    ParamsList.Params.Add(new Param() { Key = "email", Value = Email.text });
                    break;
                case 4:
                    if (ParamsList.Params.Exists(o => o.Key == "phone"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "phone"));
                    ParamsList.Params.Add(new Param() { Key = "phone", Value = PhoneNumber.text });
                    break;
                case 5:
                    if (ParamsList.Params.Exists(o => o.Key == "gender"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "gender"));
                    if (Sex.options[Sex.value].text == "ﺩﺮﻣ")
                        ParamsList.Params.Add(new Param() { Key = "gender", Value = "مرد" });
                    else if (Sex.options[Sex.value].text == "ﻥﺯ")
                        ParamsList.Params.Add(new Param() { Key = "gender", Value = "زن" });
                    break;
                case 6:
                    if (ParamsList.Params.Exists(o => o.Key == "age"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "age"));
                    ParamsList.Params.Add(new Param() { Key = "age", Value = Age.options[Age.value].text });
                    break;
                case 7:
                    if (ParamsList.Params.Exists(o => o.Key == "shaba"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "shaba"));
                    ParamsList.Params.Add(new Param() { Key = "shaba", Value = Shaba.text });
                    break;
                case 8:
                    if (ParamsList.Params.Exists(o => o.Key == "melli"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "melli"));
                    ParamsList.Params.Add(new Param() { Key = "melli", Value = Melli.text });
                    break;
                case 9:
                    if (ParamsList.Params.Exists(o => o.Key == "madrak_tahsili"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "madrak_tahsili"));
                    ParamsList.Params.Add(new Param() { Key = "madrak_tahsili", Value = EducationD.text });
                    break;
                case 10:
                    if (ParamsList.Params.Exists(o => o.Key == "city"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "city"));
                    ParamsList.Params.Add(new Param() { Key = "city", Value = City.text });
                    break;
                case 11:
                    if (ParamsList.Params.Exists(o => o.Key == "address"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "address"));
                    ParamsList.Params.Add(new Param() { Key = "address", Value = Address.text });
                    break;
            }
        }
        catch
        {
            Debug.Log("Error");
        }
        //StartCoroutine(DoEdit(1));
    }

    public void CheckEmail()
    {
        if (Email.text.Contains('@') && (Email.text.Contains(".com") || Email.text.Contains(".ir") || Email.text.Contains(".co")))
        {
            Email.image.color = Pass;
        }
        else
        {
            Email.image.color = Faild;
        }
    }

    public void CheckPhoneNumber()
    {
        if (PhoneNumber.text.Length == 11)
        {
            PhoneNumber.image.color = Pass;
        }
        else
        {
            PhoneNumber.image.color = Faild;
        }
    }

    public void CheckPassword()
    {
        if (NewPassword.text.Any(char.IsLower) && NewPassword.text.Any(char.IsUpper) && NewPassword.text.Any(char.IsNumber))
        {
            NewPassword.image.color = Pass;
        }
        else
        {
            NewPassword.image.color = Faild;
        }
    }

    public bool CheckPasswordConferm()
    {
        if (NewPasswordConferm.text.Any(char.IsLower) && NewPasswordConferm.text.Any(char.IsUpper) &&
            NewPasswordConferm.text.Any(char.IsNumber) && NewPassword.text == NewPasswordConferm.text)
        {
            NewPasswordConferm.image.color = Pass;
            return true;
        }
        else
        {
            NewPasswordConferm.image.color = Faild;
            return false;
        }
    }



    public void ChangePassword()
    {
        if (CheckPasswordConferm() == true)
        {
            StartCoroutine(DoEdit(2));
        }
        //Debug.Log(JsonUtility.ToJson(ParamsList));
    }

    public void CheckBio()
    {
        BioText.text = (150 - Bio.text.Length).ToString();
    }

    public void CheckGigs()
    {
        GigsText.text = (75 - Gigs.text.Length).ToString();
    }

    public void FillParam()
    {
        Debug.Log(JsonUtility.ToJson(ParamsList));
        StartCoroutine(DoEdit(1));
    }
}
