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

    public InputField Email, PhoneNumber, Bio, Gigs, FullName, NewPassword, NewPasswordConferm, Password;
    public Dropdown Age, Sex;
    public RtlText BioText, GigsText;
    public GameObject PassMessage;
    public Color Pass, Faild;
    private GameObject GSM;
    private string param, value;

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
    }

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 8);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("param", param);
        web.AddField("value", value);
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
        switch(item)
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
                if(dataPass.text == "Password Restarted")
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

    public void DoEditCaller(int item)
    {
        switch (item)
        {
            case 0:
                param = "name";
                value = FullName.text;
                break;
            case 1:
                param = "bio";
                value = Bio.text;
                break;
            case 2:
                //param = "Gigs";
                //value = Bio.text;
                break;
            case 3:
                param = "email";
                value = Email.text;
                break;
            case 4:
                param = "phone";
                value = PhoneNumber.text;
                break;
            case 5:
                param = "gender";
                value = Sex.itemText.text;
                break;
            case 6:
                param = "age";
                value = Age.itemText.text;
                break;
        }
        StartCoroutine(DoEdit(1));
    }

    public void CheckEmail()
    {
        if(Email.text.Contains('@') &&(Email.text.Contains(".com") || Email.text.Contains(".ir") || Email.text.Contains(".co")))
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
        if(PhoneNumber.text.Length == 11)
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
        if(NewPassword.text.Any(char.IsLower) && NewPassword.text.Any(char.IsUpper) && NewPassword.text.Any(char.IsNumber))
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
    }

    public void CheckBio()
    {
        BioText.text = (150 - Bio.text.Length).ToString();
    }

    public void CheckGigs()
    {
        GigsText.text = (75 - Gigs.text.Length).ToString();
    }
}
