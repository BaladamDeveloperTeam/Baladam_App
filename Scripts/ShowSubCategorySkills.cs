using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class ShowSubCategorySkills : MonoBehaviour
{

    private string SubcategoryId;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private string GetJson = "";
    public GameObject Loading, ShowSkill_Long;
    private GameObject Content;
    private Image TopImage, TopBtnBackGround;
    private Button BackBtn;
    public SelectedSkills[] SubcategorySkill;
    private GameObject[] AllItems;
    private Transform[] transform0, transforms1;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    private void OnEnable()
    {
        SubcategoryId = GSM.ReadSelectedSubcategoryId();
        if (SubcategoryId == "No Item Selected")
            Debug.Log("No Item Selected!!!");
        StartCoroutine(GetSubcategorySkills());
        FindObjects();
    }

    private void FindObjects()
    {
        //TopImage = GameObject.Find("ShowSubCategory_p/Scroll View/Viewport/Content/Top/TopImage").gameObject.GetComponent<Image>();
        BackBtn = GameObject.Find("ShowSubCategory_p/Scroll View/Viewport/TopBtn/BackBtn").gameObject.GetComponent<Button>();
        TopBtnBackGround = GameObject.Find("ShowSubCategory_p/Scroll View/Viewport/TopBtn").gameObject.GetComponent<Image>();
        BackBtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            Global_Script_Manager.SetLog(4, "Back_Btn From ShowSubCategory_p To List_p");
        });
        Content = GameObject.Find("ShowSubCategory_p/Scroll View/Viewport/Content");
    }

    private void FillObject()
    {

    }

    private void AddPrefab()
    {
        AllItems = new GameObject[SubcategorySkill.Length];
        for(int i = 0; i < SubcategorySkill.Length; i++)
        {
            GameObject Items = Instantiate(ShowSkill_Long) as GameObject;
            Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            Items.transform.SetParent(GameObject.Find("ShowSubCategory_p/Scroll View/Viewport/Content/Skills").transform);
            AllItems[i] = Items;
            transform0 = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
            transforms1 = transform0[0].gameObject.transform.Cast<Transform>().ToArray();
            transforms1[0].gameObject.GetComponent<RtlText>().text = SubcategorySkill[i].name;
            transforms1[2].gameObject.GetComponent<RtlText>().text = SubcategorySkill[i].rate.ToString();
            if (SubcategorySkill[i].url.Length >= 1)
                StartCoroutine(GetImageFromURL(SubcategorySkill[i].url[0], transforms1[3].gameObject.GetComponent<Image>()));
            if (!string.IsNullOrEmpty(SubcategorySkill[i].seller.pro_image))
                StartCoroutine(GetImageFromURL(SubcategorySkill[i].seller.pro_image, transforms1[4].gameObject.GetComponent<Image>()));
            transforms1[5].gameObject.GetComponent<RtlText>().text = "ت " + SubcategorySkill[i].skills.box[0].cost;
        }
        for (int i = 0; i < AllItems.Length; i++)
        {
            AllItems[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        FixUnityBug();
    }

    private WWWForm SendDataForReadSkills()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 23);
        web.AddField("subid", SubcategoryId);
        web.AddField("load", 2);
        return web;
    }

    private IEnumerator GetSubcategorySkills()
    {
        WWWForm WebGet = SendDataForReadSkills();
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
            SubcategorySkill = JsonHelper.FromJson<SelectedSkills>("{\"Items\": " + GetJson + "}");
        }
        FillObject();
        AddPrefab();
        Loading.gameObject.SetActive(false);
    }

    public IEnumerator GetImageFromURL(string URL, Image Target)
    {
        string url = URL;
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
                Target.sprite = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    void Start()
    {

    }

    private void Update()
    {
        SetTopColor();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Global_Script_Manager.SetLog(4, "Back_Btn_Device From ShowSubCategory_p To List_p");
        }
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

    private void FixUnityBug()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x - 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y - 0.01f);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x + 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + 0.01f);
    }
}
