using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using UnityEngine.UI;

public class Global_Script_Manager : MonoBehaviour
{
    private GameObject UserFullName, UserC_prj, UserF_prj, UserRate, Userlvl, UserBioText;
    private UserInfo[] userinfo;

    void Awake()
    {
        
    }


    void Start ()
    {
		
	}

    public void SetUserInfo(UserInfo[] userInfos)
    {
        userinfo = userInfos;
        FindObjects();
        SetValuetext();
    }

    private void FindObjects()
    {
        UserFullName = GameObject.Find("UserFullName");
        UserRate = GameObject.Find("UserRate");
        UserC_prj = GameObject.Find("UserC_prj");
        UserF_prj = GameObject.Find("UserF_prj");
        Userlvl = GameObject.Find("Userlvl");
        UserBioText = GameObject.Find("UserBioText");
    }

    public void SetValuetext()
    {
        if(CheckForEmpty(userinfo[0].name) == false)
            UserFullName.gameObject.GetComponent<RtlText>().text = "بلدم";
        else
            UserFullName.gameObject.GetComponent<RtlText>().text = userinfo[0].name;
        UserRate.gameObject.GetComponent<RtlText>().text = userinfo[0].rate.ToString();
        UserC_prj.gameObject.GetComponent<RtlText>().text = userinfo[0].c_prj.ToString();
        UserF_prj.gameObject.GetComponent<RtlText>().text = userinfo[0].f_prj.ToString();
        Userlvl.gameObject.GetComponent<RtlText>().text = userinfo[0].lvl;
        if (CheckForEmpty(userinfo[0].bio) == false)
            UserBioText.gameObject.GetComponent<RtlText>().text = "درباره شما ...";
        else
            UserBioText.gameObject.GetComponent<RtlText>().text = userinfo[0].bio;

    }

    private bool CheckForEmpty(string text)
    {
        if (text == "" || text == null || text == " ")
            return false;
        else
            return true;
    }
}
