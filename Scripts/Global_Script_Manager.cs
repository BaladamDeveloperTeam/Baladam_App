using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using UnityEngine.UI;

public class Global_Script_Manager : MonoBehaviour
{
    private GameObject UserFullName, UserC_prj, UserF_prj, UserRate, Userlvl, UserBioText, UserFullNameEdit, UserBioEdit, UserEmailEdit,
        UserPhoneEdit, UserAgeEdit, UserSexEdit;
    private UserInfo[] userinfo;
    //[HideInInspector]
    //public Session[] OldSession;
    public CategoryNames CatInfo;

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

    public void SetCategoryName(CategoryNames CatInfo)
    {
        this.CatInfo = CatInfo;
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

    public void FindObjectsForEdit()
    {
        UserFullNameEdit = GameObject.Find("EditFullName");
        UserBioEdit = GameObject.Find("EditBio");
        UserEmailEdit = GameObject.Find("EditEmail");
        UserPhoneEdit = GameObject.Find("EditPhoneNumber");
        UserAgeEdit = GameObject.Find("EditAge");
        UserSexEdit = GameObject.Find("EditSex");
    }

    public void SetValuetextForEdit()
    {
        if (CheckForEmpty(userinfo[0].name) != false)
            UserFullNameEdit.gameObject.GetComponent<InputField>().text = userinfo[0].name;
            
        if (CheckForEmpty(userinfo[0].bio) != false)
            UserBioEdit.gameObject.GetComponent<InputField>().text = userinfo[0].bio;

        if (CheckForEmpty(userinfo[0].email) != false)
            UserEmailEdit.gameObject.GetComponent<InputField>().text = userinfo[0].email;

        if (CheckForEmpty(userinfo[0].phone) != false)
            UserPhoneEdit.gameObject.GetComponent<InputField>().text = userinfo[0].phone;
    }

    public string ReadUserName()
    {
        return userinfo[0].username;
    }

    //public void SetOldSession(Session[] session)
    //{
    //    OldSession = session;
    //}

    private bool CheckForEmpty(string text)
    {
        if (text == "" || text == null || text == " ")
            return false;
        else
            return true;
    }
}
