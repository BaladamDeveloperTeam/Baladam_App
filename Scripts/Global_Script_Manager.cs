﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Global_Script_Manager : MonoBehaviour
{
    private static string path = null;
    private GameObject UserFullName, UserC_prj, UserF_prj, UserRate, Userlvl, UserBioText, UserGigsText, UserFullNameEdit, UserBioEdit, UserGigsEdit, UserEmailEdit,
        UserPhoneEdit, UserAgeEdit, UserSexEdit, UserShabaEdit, UserMelliEdit, UserEduationEdit, UserCityEdit, UserAddressEdit;
    private UserInfo[] userinfo;
    [HideInInspector]
    public string[] CatInfo;
    private string SelectedSkillCode = "No Item Selected";
    private string SelectedSellerID;
    private string SelectedSubCategoryId = "No Item Selected";

    public LoadCategory[] LoadCategory;
    private static List<Log> AppLog = new List<Log>();
    private static string[] json = new string[20];

    void Awake()
    {
        path = Application.persistentDataPath + "BaladamAppLog.json";
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

    public void SetCategoryName(string[] CatInfo)
    {
        this.CatInfo = CatInfo;
    }

    public void SetSkillCode(string SkillCode)
    {
        this.SelectedSkillCode = SkillCode;
    }

    public void SetSubCategoryId(string SubCategoryId)
    {
        this.SelectedSubCategoryId = SubCategoryId;
    }

    public void SetSellerID(string SellerID)
    {
        this.SelectedSellerID = SellerID;
    }

    public void SetLoadCategory(LoadCategory[] loadCategories)
    {
        this.LoadCategory = loadCategories;
    }

    private void FindObjects()
    {
        UserFullName = GameObject.Find("UserFullName");
        UserRate = GameObject.Find("UserRate");
        UserC_prj = GameObject.Find("UserC_prj");
        UserF_prj = GameObject.Find("UserF_prj");
        Userlvl = GameObject.Find("Userlvl");
        UserBioText = GameObject.Find("UserBioText");
        UserGigsText = GameObject.Find("UserGigs");
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
        if (CheckForEmpty(userinfo[0].major_field) == false)
            UserGigsText.gameObject.GetComponent<RtlText>().text = "زمینه های مهارت شما ...";
        else
            UserGigsText.gameObject.GetComponent<RtlText>().text = userinfo[0].major_field;

    }

    public void FindObjectsForEdit()
    {
        UserFullNameEdit = GameObject.Find("EditFullName");
        UserBioEdit = GameObject.Find("EditBio");
        UserGigsEdit = GameObject.Find("EditGigs");
        UserEmailEdit = GameObject.Find("EditEmail");
        UserPhoneEdit = GameObject.Find("EditPhoneNumber");
        UserAgeEdit = GameObject.Find("EditAge");
        UserSexEdit = GameObject.Find("EditSex");
        UserShabaEdit = GameObject.Find("EditShaba");
        UserMelliEdit = GameObject.Find("EditMelli");
        UserEduationEdit = GameObject.Find("EditEducationD");
        UserCityEdit = GameObject.Find("EditCity");
        UserAddressEdit = GameObject.Find("EditAddress");
    }

    public void SetValuetextForEdit()
    {
        if (CheckForEmpty(userinfo[0].name) != false)
            UserFullNameEdit.gameObject.GetComponent<InputField>().text = userinfo[0].name;
            
        if (CheckForEmpty(userinfo[0].bio) != false)
            UserBioEdit.gameObject.GetComponent<InputField>().text = userinfo[0].bio;

        if(CheckForEmpty(userinfo[0].major_field) != false)
            UserGigsEdit.gameObject.GetComponent<InputField>().text = userinfo[0].major_field;

        if (CheckForEmpty(userinfo[0].email) != false)
            UserEmailEdit.gameObject.GetComponent<InputField>().text = userinfo[0].email;

        if (CheckForEmpty(userinfo[0].phone) != false)
            UserPhoneEdit.gameObject.GetComponent<InputField>().text = userinfo[0].phone;

        if (CheckForEmpty(userinfo[0].shaba) != false)
            UserShabaEdit.gameObject.GetComponent<InputField>().text = userinfo[0].shaba;

        if (CheckForEmpty(userinfo[0].melli) != false)
            UserMelliEdit.gameObject.GetComponent<InputField>().text = userinfo[0].melli;

        if (CheckForEmpty(userinfo[0].madrak_tahsili) != false)
            UserEduationEdit.gameObject.GetComponent<InputField>().text = userinfo[0].madrak_tahsili;

        if (CheckForEmpty(userinfo[0].city) != false)
            UserCityEdit.gameObject.GetComponent<InputField>().text = userinfo[0].city;

        if (CheckForEmpty(userinfo[0].address) != false)
            UserAddressEdit.gameObject.GetComponent<InputField>().text = userinfo[0].address;

        if (CheckForEmpty(userinfo[0].gender) != false && userinfo[0].gender == "مرد")
            UserSexEdit.gameObject.GetComponent<Dropdown>().value = 0;
        else
            UserSexEdit.gameObject.GetComponent<Dropdown>().value = 1;

    }

    public string ReadUserName()
    {
        return userinfo[0].username;
    }

    public string ReadPro_imageURL()
    {
        return userinfo[0].pro_image;
    }

    public string ReadUserID()
    {
        return userinfo[0]._id;
    }

    public string ReadIsSeller()
    {
        return userinfo[0].is_seller.ToString();
    }

    public string ReadSelectedSkillCode()
    {
        return SelectedSkillCode;
    }

    public string ReadSelectedSubcategoryId()
    {
        return SelectedSubCategoryId;
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

    public static void SetLog(int _id, string _Detail)
    {
        int ID = int.Parse(1.ToString() + _id.ToString());
        AppLog.Add(new Log { id = ID, Detail = _Detail, Time = System.DateTime.Now.ToString() });
        if (AppLog.Count() >= 15)
        {
            Log[] logs = AppLog.ToArray();
            for (int i = 0; i < 15; i++)
            {
                json[i] = JsonUtility.ToJson(logs[i]);
                using (FileStream fs = new FileStream(path, FileMode.Open))
                //if (File.Exists(path)) 
                {
                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine(json[i]);
                    }
                }
                //else
                //{
                //    using (FileStream fse = new FileStream(path, FileMode.Create))
                //    {
                //        using (StreamWriter w = File.AppendText(path))
                //        {
                //            w.WriteLine(json[i]);
                //        }
                //    }
                //}
            }
            AppLog.Clear();
        }
    }
}
