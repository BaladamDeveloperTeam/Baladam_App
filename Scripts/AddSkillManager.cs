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
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string SubCategoryJson = "";
    private string[] SkillPointsPath, ImageName = { null, null, null, null};
    private SubCategoryInfo[] SubCatInfo;
    public Skill[] UserSkill = new Skill[1];
    public Dropdown SelectCategory, SelectSubCategory;
    private Toggle IsExpress;
    private Global_Script_Manager GSM;
    private GameObject SkillName, SkillCategory, SkillSubCategory, SkillDescription, Express_p
        , ExpressCost, ExpressTime, Loading, AddSkillMessage, ImagePicker_p, ImagePicker_Content, ImagePickerNumber;
    public GameObject[] SkillPoints, Cost, Period;
    public Image[] image = new Image[4];
    public Animator ImagePickerAnim;
    public ImageClass Pimg;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        foreach(string CatText in GSM.CatInfo)
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
        Loading = GameObject.Find("WaitForAddSkill");
        Loading.gameObject.SetActive(false);
        AddSkillMessage = GameObject.Find("AddSkillMessage");
        AddSkillMessage.gameObject.SetActive(false);
        ImagePicker_p = GameObject.Find("ImagePicker_p");
        ImagePicker_Content = GameObject.Find("ImagePicker_p/Images/ScrollView/Viewport/Content");
        ImagePickerNumber = GameObject.Find("ImagePicker_p/Bottom/ImageCounter");
        ImagePicker_p.gameObject.SetActive(false);

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

    private void OnPickGalleryImage()
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
                image[0].sprite = SpriteFromTex2D(imageTexture2D);
                if(ImageName[0] == null)
                    ImageName[0] = selectedImage.DisplayName;
                else if(ImageName[1] == null)
                    ImageName[1] = selectedImage.DisplayName;
                else if (ImageName[2] == null)
                    ImageName[2] = selectedImage.DisplayName;
                else if (ImageName[3] == null)
                    ImageName[3] = selectedImage.DisplayName;
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

    public void SelectImageBtn()
    {
        security.Coding coding = new security.Coding();
        int i = 0;
        //ImageClass Pimg = GameObject.Find("Profile_p/Skills/SkillScriptManager").gameObject.GetComponent<ImageClass>();
        if (ImageName[0] == null)
        {
            i = 0;
            Pimg.OnPressShowPicker(i.ToString() + coding.Md5Sum(DateTime.Now + GSM.ReadUserName()));
            ImageName[0] = Pimg.GetPath();
            image[0].sprite = Pimg.GetSprite();
            var tempColor = image[0].color;
            tempColor.a = 1f;
            image[0].color = tempColor;
            ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            ImagePickerNumber.gameObject.GetComponent<RtlText>().text = "1/4";
        }
        else if (ImageName[1] == null)
        {
            i = 1;
            Pimg.OnPressShowPicker(i.ToString() + coding.Md5Sum(DateTime.Now + GSM.ReadUserName()));
            ImageName[1] = Pimg.GetPath();
            image[1].sprite = Pimg.GetSprite();
            var tempColor = image[1].color;
            tempColor.a = 1f;
            image[1].color = tempColor;
            ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(800, ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            ImagePickerNumber.gameObject.GetComponent<RtlText>().text = "2/4";
        }
        else if (ImageName[2] == null)
        {
            i = 2;
            Pimg.OnPressShowPicker(i.ToString() + coding.Md5Sum(DateTime.Now + GSM.ReadUserName()));
            ImageName[2] = Pimg.GetPath();
            image[2].sprite = Pimg.GetSprite();
            var tempColor = image[2].color;
            tempColor.a = 1f;
            image[2].color = tempColor;
            ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1615, ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            ImagePickerNumber.gameObject.GetComponent<RtlText>().text = "3/4";
        }
        else if (ImageName[3] == null)
        {
            i = 3;
            Pimg.OnPressShowPicker(i.ToString() + coding.Md5Sum(DateTime.Now + GSM.ReadUserName()));
            ImageName[3] = Pimg.GetPath();
            image[3].sprite = Pimg.GetSprite();
            var tempColor = image[3].color;
            tempColor.a = 1f;
            image[3].color = tempColor;
            ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2430, ImagePicker_Content.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            ImagePickerNumber.gameObject.GetComponent<RtlText>().text = "4/4";
        }
    }

    public void OpenImagePicker_p()
    {
        if (ImagePicker_p.gameObject.activeInHierarchy == true)
            ImagePicker_p.gameObject.SetActive(false);
        ImagePicker_p.gameObject.SetActive(true);
    }

    public void CloseImagePicker_p()
    {
        ImagePickerAnim.SetTrigger("Close");
    }

    public void IsExpressCheack()
    {
        switch (IsExpress.isOn)
        {
            case true:
                Express_p.gameObject.SetActive(true);
                break;
            default:
                Express_p.gameObject.SetActive(false);
                break;
        }
    }

    public void SubmitBtn()
    {
        security.Coding coding = new security.Coding();
        UserSkill[0].SkillName = SkillName.gameObject.GetComponent<InputField>().text;
        UserSkill[0].SkillCategory = SkillCategory.gameObject.GetComponent<Dropdown>().value.ToString();
        UserSkill[0].SkillSubCategory = SubCatInfo[SelectSubCategory.value].subID;
        UserSkill[0].SkillDescription = SkillDescription.gameObject.GetComponent<InputField>().text;
        UserSkill[0].ImageName = new string[4];
        if (UserSkill[0].ImageName != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if(ImageName[i] != null)
                {
                    UserSkill[0].ImageName[i] = ImageName[i];
                }
            }
        }
        if (IsExpress.isOn)
        {
            UserSkill[0].IsExpress = 1;
            int.TryParse(ExpressCost.gameObject.GetComponent<InputField>().text, out UserSkill[0].ExpressCost);
            int.TryParse(ExpressTime.gameObject.GetComponent<InputField>().text, out UserSkill[0].ExpressTime);
        }
        else
        {
            UserSkill[0].IsExpress = 0;
        }
        for (int i = 0; i < 4; i++)
        {
            UploadFiles UP = new UploadFiles();
            UP.UploadFile(@UserSkill[0].ImageName[i], GSM.ReadUserName());
        }

        SetSkillPointsParametr();
        StartCoroutine(SetSkill());
    }

    private WWWForm SendDataSkill()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 13);
        web.AddField("user", GSM.ReadUserName());
        web.AddField("skill", JsonHelper.ToJson(UserSkill));
        return web;
    }

    private IEnumerator SetSkill()
    {
        WWWForm WebGet = SendDataSkill();
        WWW data = new WWW(Url, WebGet);

        Loading.gameObject.SetActive(true);
        yield return data;
        Loading.gameObject.SetActive(false);

        Debug.Log(data.text);
        SubCategoryJson = data.text;
        Debug.Log(JsonHelper.ToJson(UserSkill));

        if(data.text == "save")
        {
            AddSkillMessage.gameObject.SetActive(true);
        }
    }
}
