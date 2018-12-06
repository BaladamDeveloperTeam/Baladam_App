﻿using SmsIrRestful;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class Home_Click_Handler : MonoBehaviour
{
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    public GameObject SkillPrefab, ShowSkill_p;
    public MySkills[] UserSkills;
    private GameObject[] AllItems;
    private string GetJson = "";
    public Text PlaceHolder;
    private GetCat GetCat;
    private Sprite Temp;
    public Transform[] MySkilltransform_p, MySkilltransform;
    private Global_Script_Manager GSM;

    public List<SkillButton> SkillButton = new List<SkillButton>();

    void Start ()
    {
        Screen.fullScreen = false;
        GetCat = this.gameObject.GetComponent<GetCat>();
        GetCat.GetCatBut();
        StartCoroutine(GetAllUserSkills());
    }

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    public void Upload()
    {
        UploadFiles up = new UploadFiles();
        up.UploadFile(@"E:\TODO.txt", "/test");
        //DownloadFiles DF = new DownloadFiles();
        //string path = Path.Combine(Application.persistentDataPath, "FTP Files");
        //path = Path.Combine(path, "data.png");
        //DF.downloadWithFTP("ftp://138.201.32.126/BaladamSkillImage/test/0689768c0bc582f99f845db3e272a159b.png", path);
        //DF.ListOfDirectory("ftp://138.201.32.126/BaladamSkillImage/");
        
        //DF.NewLibTestAsync();
    }

    //TODO:Fix This Funny Test 
    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 17);
        web.AddField("user", "mohada");
        return web;
    }
    //TODO:Fix This Funny Test 
    private IEnumerator GetAllUserSkills()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);

        yield return data;

        GetJson = data.text;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        { 
            UserSkills = JsonHelper.FromJson<MySkills>("{\"Items\": " + GetJson + "}");
        }
        AddPrefab();
        //ShowPlace.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, CalHeight(UserSkills.Length));
    }

    public void AddPrefab()
    {
        AllItems = new GameObject[UserSkills.Length];
        for (int i = 0; i < UserSkills.Length; i++)
        {
            GameObject Items = Instantiate(SkillPrefab) as GameObject;
            Items.transform.SetParent(GameObject.Find("Content/Mod2/Scroll View (1)/Viewport/Content").transform);
            AllItems[i] = Items;
            MySkilltransform_p = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
            MySkilltransform = MySkilltransform_p[0].gameObject.transform.Cast<Transform>().ToArray();
            if (UserSkills[i].url.Length > 0)
            {
                StartCoroutine(GetImageFromURL(UserSkills[i].url[0]));
                MySkilltransform[0].gameObject.GetComponent<Image>().sprite = Temp;
            }
            MySkilltransform[1].gameObject.GetComponent<RtlText>().text = UserSkills[i].skills.box[0].cost + " ت";
            MySkilltransform[2].gameObject.GetComponent<RtlText>().text = UserSkills[i].name;
            MySkilltransform[4].gameObject.GetComponent<RtlText>().text = UserSkills[i].rate.ToString();
            SkillButton.Add(new SkillButton { id = i, _id = UserSkills[i].pz_id, name = UserSkills[i].name, SkillCode = UserSkills[i].skillCode, Button = AllItems[i].gameObject.GetComponent<Button>() });
        }
        for (int i = 0; i < UserSkills.Length; i++)
        {
            Button[] bu = (from a in SkillButton where a.id == i select a.Button).ToArray();
            string[] _id = (from a in SkillButton where a.id == i select a._id).ToArray();
            string[] SkillCode = (from a in SkillButton where a.id == i select a.SkillCode).ToArray();
            int[] id = (from a in SkillButton where a.id == i select a.id).ToArray();
            bu[0].onClick.AddListener(() => { ShowSkill(_id[0], SkillCode[0], id[0]); });
        }
    }

    public IEnumerator GetImageFromURL(string URL)
    {
        string url = URL;
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            //Temp = SpriteFromTex2D(www.texture);
            yield return www;
            //if (string.IsNullOrEmpty(www.error))
            Temp = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void ShowSkill(string SellerID, string SkillCode, int id)
    {
        GSM.SetSkillCode(SkillCode);
        GSM.SetSellerID(SellerID);
        ShowSkill_p.gameObject.SetActive(true);
        Debug.Log(SkillCode);
    }

}
