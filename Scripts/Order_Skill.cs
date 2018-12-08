using System.Collections;
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
    private Image TopImage, TopBtnBackGround;
    private GameObject Content;
    private Button BackBtn;
    public RtlText SkillName; 
    public MySkills[] SelectedSkill;

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
    }

    private void FillObject()
    {
        if (SelectedSkill[0].url.Length >= 1)
            StartCoroutine(GetImageFromURL(SelectedSkill[0].url[0]));
        SkillName.text = SelectedSkill[0].name;
    }

    void Start ()
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
}
