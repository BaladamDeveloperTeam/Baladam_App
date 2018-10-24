using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using DeadMosquito.AndroidGoodies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;

public class AddSkillManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string SubCategoryJson = "";
    public string[] SkillPointsPath;
    private SubCategoryInfo[] SubCatInfo;
    public Skill[] UserSkill = new Skill[1];
    public Dropdown SelectCategory, SelectSubCategory;
    private Toggle IsExpress;
    private GameObject GSM;
    private GameObject SkillName, SkillCategory, SkillSubCategory, SkillDescription, Express_p, ExpressCost, ExpressTime;
    public GameObject[] SkillPoints, Cost, Period;
    private Image image;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
        foreach(string CatText in GSM.gameObject.GetComponent<Global_Script_Manager>().CatInfo.name)
        {
            SelectCategory.options.Add(new Dropdown.OptionData() {text=CatText});
        }
        FindObject();
    }

    private void FindObject()
    {
        SkillName = GameObject.Find("InsertSkillName");
        SkillCategory = GameObject.Find("SelectCategory");
        SkillSubCategory = GameObject.Find("SelectSubCategory");
        SkillDescription = GameObject.Find("InsertDescription");
        Express_p = GameObject.Find("Express_p");
        ExpressCost = GameObject.Find("InsertExpressCost");
        ExpressTime = GameObject.Find("InsertExpressTime");
        Express_p.gameObject.SetActive(false);
        IsExpress = GameObject.Find("IsExpress_t").GetComponent<Toggle>();
    }

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 3);
        web.AddField("DatabaseID", SelectCategory.value);
        return web;
    }

    private IEnumerator GetSubCategorys()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
        SubCategoryJson = data.text;

        SubCatInfo = JsonHelper.FromJson<SubCategoryInfo>("{\"Items\": " + SubCategoryJson + "}");

        SelectSubCategory.options.Clear();
        for (int i = 0; i < SubCatInfo.Length; i++)
        {
            SelectSubCategory.options.Add(new Dropdown.OptionData() { text = SubCatInfo[i].SubName });
        }
    }

    public void ReadSubCat()
    {
        StartCoroutine(GetSubCategorys());
    }

    public string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }

    private void SetSkillPointsParametr()
    {
        SkillPoints = GameObject.FindGameObjectsWithTag("SkillPoints");
        SkillPointsPath = new string[SkillPoints.Length];
        int a1 = 0, a2 = 0, a3 = 0;
        for (int i = 0; i < SkillPoints.Length; i++)
        {
            SkillPointsPath[i] = GetGameObjectPath(SkillPoints[i]);
        }
        for (int i = 0; i < SkillPoints.Length; i++)
        {
            if (SkillPointsPath[i].Contains("SkillBox") && !SkillPointsPath[i].Contains("SkillBox(1)") && !SkillPointsPath[i].Contains("SkillBox(2)"))
            {
                //UserSkill[0].SkillPoints[0].SkillPoints[a1] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a1++;
            }
            if (SkillPointsPath[i].Contains("SkillBox(1)"))
            {
                //UserSkill[0].SkillPoints[1].SkillPoints[a2] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a2++;
            }
            if (SkillPointsPath[i].Contains("SkillBox(2)"))
            {
                //UserSkill[0].SkillPoints[2].SkillPoints[a3] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a3++;
            }
        }
        UserSkill[0].SkillPoints[0].SkillPoints = new string[a1];
        UserSkill[0].SkillPoints[1].SkillPoints = new string[a2];
        UserSkill[0].SkillPoints[2].SkillPoints = new string[a3];
        a1 = 0; a2 = 0; a3 = 0;
        for (int i = 0; i < SkillPoints.Length; i++)
        {
            if (SkillPointsPath[i].Contains("SkillBox") && !SkillPointsPath[i].Contains("SkillBox(1)") && !SkillPointsPath[i].Contains("SkillBox(2)"))
            {
                UserSkill[0].SkillPoints[0].SkillPoints[a1] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a1++;
            }
            if (SkillPointsPath[i].Contains("SkillBox(1)"))
            {
                UserSkill[0].SkillPoints[1].SkillPoints[a2] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a2++;
            }
            if (SkillPointsPath[i].Contains("SkillBox(2)"))
            {
                UserSkill[0].SkillPoints[2].SkillPoints[a3] = SkillPoints[i].gameObject.GetComponent<InputField>().text;
                a3++;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            int.TryParse(Cost[i].gameObject.GetComponent<InputField>().text, out UserSkill[0].SkillPoints[i].SkillCost);
            int.TryParse(Period[i].gameObject.GetComponent<InputField>().text, out UserSkill[0].SkillPoints[i].SkillPeriod);
        }
    }

    public void OnPickGalleryImage()
    {
        // Whether to generate thumbnails
        var shouldGenerateThumbnails = true;

        // if image is larger it will be downscaled to the max size proportionally
        var imageResultSize = ImageResultSize.Max2048;
        AGGallery.PickImageFromGallery(
            selectedImage =>
            {
                var imageTexture2D = selectedImage.LoadTexture2D();

                string msg = string.Format("{0} was loaded from gallery with size {1}x{2}",
                    selectedImage.OriginalPath, imageTexture2D.width, imageTexture2D.height);
                AGUIMisc.ShowToast(msg);
                Debug.Log(msg);
                image.sprite = SpriteFromTex2D(imageTexture2D);

                    // Clean up
                    Resources.UnloadUnusedAssets();
            },
            errorMessage => AGUIMisc.ShowToast("Cancelled picking image from gallery: " + errorMessage),
            imageResultSize, shouldGenerateThumbnails);
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void IsExpressCheack()
    {
        if(IsExpress.isOn == true)
            Express_p.gameObject.SetActive(true);
        else
            Express_p.gameObject.SetActive(false);
    }

    public void SubmitBtn()
    {
        UserSkill[0].SkillName = SkillName.gameObject.GetComponent<InputField>().text;
        UserSkill[0].SkillCategory = SkillCategory.gameObject.GetComponent<Dropdown>().itemText.ToString();
        UserSkill[0].SkillSubCategory = SkillSubCategory.gameObject.GetComponent<Dropdown>().itemText.ToString();
        UserSkill[0].SkillDescription = SkillDescription.gameObject.GetComponent<InputField>().text;
        if (IsExpress.isOn)
        {
            UserSkill[0].IsExpress = 1;
            int.TryParse(ExpressCost.gameObject.GetComponent<RtlText>().text, out UserSkill[0].ExpressCost);
            int.TryParse(ExpressTime.gameObject.GetComponent<RtlText>().text, out UserSkill[0].ExpressTime);
        }
        else
            UserSkill[0].IsExpress = 0;
        SetSkillPointsParametr();
        StartCoroutine(SetSkill());
    }

    private WWWForm SendDataSkill()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 9);
        web.AddField("skillname", UserSkill[0].SkillName);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("gigs", UserSkill[0].SkillDescription);
        web.AddField("skill", JsonHelper.ToJson(UserSkill));
        web.AddField("skillcode", "");
        return web;
    }

    private IEnumerator SetSkill()
    {
        WWWForm WebGet = SendDataSkill();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
        SubCategoryJson = data.text;
        Debug.Log(JsonHelper.ToJson(UserSkill));
    }
}
