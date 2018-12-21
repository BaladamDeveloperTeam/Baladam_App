﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class Order_Skill : MonoBehaviour
{

    private string SkillCode;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private string GetJson = "";
    public GameObject Loading;
    private Image TopImage, TopBtnBackGround, SkillBox0Footer, SkillBox1Footer, SkillBox2Footer;
    public Color UnSelectedColor = new Color(245, 245, 245);
    public Color SelectedColor = new Color(103, 58, 183);
    public Color UnSelectedTextColor = new Color(103, 58, 183);
    private GameObject Content;
    private Button BackBtn;
    private RtlText SkillName, SkillDecep, SkillBox0Cost, SkillBox1Cost, SkillBox2Cost, SkillBoxDecep, DeliveryTime;
    private MySkills[] SelectedSkill;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    private void OnEnable()
    {
        SkillCode = GSM.ReadSelectedSkillCode();
        if (GSM.ReadSelectedSkillCode() == "No Item Selected")
            Debug.Log("No Item Selected!!!");
        StartCoroutine(GetSelectedSkill());
        FindObjects();
    }

    private void FindObjects()
    {
        TopImage = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/Top/TopImage").gameObject.GetComponent<Image>();
        BackBtn = GameObject.Find("ShowSkill_p/Scroll View/Viewport/TopBtn/BackBtn").gameObject.GetComponent<Button>();
        TopBtnBackGround = GameObject.Find("ShowSkill_p/Scroll View/Viewport/TopBtn").gameObject.GetComponent<Image>();
        BackBtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        Content = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content");
        SkillName = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/SkillName_p/SkillName_text").gameObject.GetComponent<RtlText>();
        SkillDecep = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/SkillDecep_text").gameObject.GetComponent<RtlText>();
        SkillBox0Cost = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/0/Cost").gameObject.GetComponent<RtlText>();
        SkillBox1Cost = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/1/Cost").gameObject.GetComponent<RtlText>();
        SkillBox2Cost = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/2/Cost").gameObject.GetComponent<RtlText>();
        SkillBox0Footer = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/0/Footer").gameObject.GetComponent<Image>();
        SkillBox1Footer = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/1/Footer").gameObject.GetComponent<Image>();
        SkillBox2Footer = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/ShowTypes/2/Footer").gameObject.GetComponent<Image>();
        SkillBoxDecep = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/Dec").gameObject.GetComponent<RtlText>();
        DeliveryTime = GameObject.Find("ShowSkill_p/Scroll View/Viewport/Content/BuySkillsBox/DeliveryInfo/DeliveryTime").gameObject.GetComponent<RtlText>();
    }

    private void FillObject()
    {
        if (SelectedSkill[0].url.Length >= 1)
            StartCoroutine(GetImageFromURL(SelectedSkill[0].url[0]));
        SkillName.text = SelectedSkill[0].name;
        SkillDecep.text = SelectedSkill[0].decep;
        SkillBox0Cost.text = "ت " + SelectedSkill[0].skills.box[0].cost;
        SkillBox1Cost.text = "ت " + SelectedSkill[0].skills.box[1].cost;
        SkillBox2Cost.text = "ت " + SelectedSkill[0].skills.box[2].cost;
        //DeliveryTime.text = SelectedSkill[0].skills.box[1].time + " روز ";
        SelectBox1();
        FixUnityBug();
    }

    public void SelectBox0()
    {
        SkillBoxDecep.text = "";
        SkillBox0Footer.color = SelectedColor;
        SkillBox1Footer.color = UnSelectedColor;
        SkillBox2Footer.color = UnSelectedColor;
        SkillBox0Cost.color = SelectedColor;
        SkillBox1Cost.color = UnSelectedTextColor;
        SkillBox2Cost.color = UnSelectedTextColor;
        for(int i = 0; i < SelectedSkill[0].skills.box[0].options.Length; i++)
        {
            SkillBoxDecep.text += SelectedSkill[0].skills.box[0].options[i] + "\n";
        }
        DeliveryTime.text = SelectedSkill[0].skills.box[0].time + " روز ";
        FixUnityBug();
    }

    public void SelectBox1()
    {
        SkillBoxDecep.text = "";
        SkillBox0Footer.color = UnSelectedColor;
        SkillBox1Footer.color = SelectedColor;
        SkillBox2Footer.color = UnSelectedColor;
        SkillBox0Cost.color = UnSelectedTextColor;
        SkillBox1Cost.color = SelectedColor;
        SkillBox2Cost.color = UnSelectedTextColor;
        for (int i = 0; i < SelectedSkill[0].skills.box[1].options.Length; i++)
        {
            SkillBoxDecep.text += SelectedSkill[0].skills.box[1].options[i] + "\n";
        }
        DeliveryTime.text = SelectedSkill[0].skills.box[1].time + " روز ";
        FixUnityBug();
    }

    public void SelectBox2()
    {
        SkillBoxDecep.text = "";
        SkillBox0Footer.color = UnSelectedColor;
        SkillBox1Footer.color = UnSelectedColor;
        SkillBox2Footer.color = SelectedColor;
        SkillBox0Cost.color = UnSelectedTextColor;
        SkillBox1Cost.color = UnSelectedTextColor;
        SkillBox2Cost.color = SelectedColor;
        for (int i = 0; i < SelectedSkill[0].skills.box[2].options.Length; i++)
        {
            SkillBoxDecep.text += SelectedSkill[0].skills.box[2].options[i] + "\n";
        }
        DeliveryTime.text = SelectedSkill[0].skills.box[2].time + " روز ";
        FixUnityBug();
    }

    void Start()
    {

    }

    private void Update()
    {
        SetTopColor();
    }

    private void SetTopColor()
    {
        float a = Content.gameObject.GetComponent<RectTransform>().localPosition.y;
        if (a <= 418)
        {
            a = a / 418;
            var tempColor = TopBtnBackGround.color;
            if (a <= 1)
                tempColor.a = a;
            else
                tempColor.a = 1;
            TopBtnBackGround.color = tempColor;
        }
        else
        {
            var tempColor = TopBtnBackGround.color;
            tempColor.a = 1;
            TopBtnBackGround.color = tempColor;
        }
    }

    private WWWForm SendDataForBuy()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 20);
        web.AddField("skillcode", SkillCode);
        web.AddField("shopper", GSM.ReadUserID());
        web.AddField("seller", SelectedSkill[0].pz_id);
        web.AddField("box", "");                                            //Need fix
        web.AddField("ex", "");                                             //Need fix    
        return web;
    }

    private IEnumerator BuySkill()
    {
        WWWForm WebGet = SendDataForBuy();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }

    private WWWForm SendDataForReadSkill()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 21);
        web.AddField("skillcode", SkillCode);
        return web;
    }

    private IEnumerator GetSelectedSkill()
    {
        WWWForm WebGet = SendDataForReadSkill();
        WWW data = new WWW(Url, WebGet);

        Loading.gameObject.SetActive(true);
        yield return data;

        GetJson = data.text;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            SelectedSkill = JsonHelper.FromJson<MySkills>("{\"Items\": [" + GetJson + "]}");
        }
        FillObject();
        Loading.gameObject.SetActive(false);
    }

    public IEnumerator GetImageFromURL(string URL)
    {
        string url = URL;
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
                TopImage.sprite = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private void FixUnityBug()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x - 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y - 0.01f);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x + 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + 0.01f);
    }
}
