using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class EditSkillManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private SubCategoryInfo[] SubCatInfo;
    private string SubCategoryJson = "";
    private GameObject SkillName, SkillCategory, SkillSubCategory, SkillDescription, Express_p
        , ExpressCost, ExpressTime, Loading, AddSkillMessage, ImagePicker_p, ImagePicker_Content, ImagePickerNumber;
    public GameObject[] Boxs;
    public Dropdown SelectCategory, SelectSubCategory;
    private Toggle IsExpress;
    private SkillsPanelManager SPM;
    public ParamList ParamsList;
    private Transform[] SkillBoxItem;
    public GameObject SkillPointPrefab;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        foreach (string CatText in GSM.CatInfo.name)
        {
            SelectCategory.options.Add(new Dropdown.OptionData() { text = CatText });
        }
        FindObject();
        //FillGameObject();
    }

    private void FindObject()
    {
        string ObjectPath = "Skills/EditSkill_p/Scroll View/Viewport/Content/";
        SkillName = GameObject.Find(ObjectPath + "InsertSkillName_p/InsertSkillName");
        SkillCategory = GameObject.Find(ObjectPath + "SelectCategory_p/SelectCategory");
        SkillSubCategory = GameObject.Find(ObjectPath + "SelectSubCategory_p/SelectSubCategory");
        SkillDescription = GameObject.Find(ObjectPath + "InsertDescription_p/InsertDescription");
        Express_p = GameObject.Find(ObjectPath + "Express_p");
        ExpressCost = GameObject.Find(ObjectPath + "Express_p/InsertExpressCost_p/InsertExpressCost");
        ExpressTime = GameObject.Find(ObjectPath + "Express_p/InsertExpressTime_p/InsertExpressTime");
        Loading = GameObject.Find("Skills/EditSkill_p/WaitForAddSkill");
        Loading.gameObject.SetActive(false);
        AddSkillMessage = GameObject.Find(ObjectPath + "AddSkillMessage");
        AddSkillMessage.gameObject.SetActive(false);
        //ImagePicker_p = GameObject.Find(ObjectPath + "ImagePicker_p");
        //ImagePicker_Content = GameObject.Find("ImagePicker_p/Images/ScrollView/Viewport/Content");
        //ImagePickerNumber = GameObject.Find("ImagePicker_p/Bottom/ImageCounter");
        //ImagePicker_p.gameObject.SetActive(false);
        SPM = GameObject.Find("Profile_p/Skills/Skills_p Manager").gameObject.GetComponent<SkillsPanelManager>();
        Boxs = GameObject.FindGameObjectsWithTag("SkillBox");

        IsExpress = GameObject.Find("IsExpress_t").GetComponent<Toggle>();
    }

    private void FillGameObject()
    {
        SkillName.gameObject.GetComponent<InputField>().text = SPM.UserSkills[SPM.SelectedID].name;
        SkillCategory.gameObject.GetComponent<Dropdown>().value = Convert.ToInt32(SPM.UserSkills[SPM.SelectedID].title);
        StartCoroutine(GetSubCategorys());
        SkillSubCategory.gameObject.GetComponentsInChildren<RtlText>()[0].text = SPM.UserSkills[SPM.SelectedID].subtitle;
        for(int i = 0; i < SPM.UserSkills[SPM.SelectedID].skills.box[0].options.Length - 1; i++)
        { 
            GameObject Items = Instantiate(SkillPointPrefab) as GameObject;
            Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            Items.transform.SetParent(GameObject.Find("SkillBox/SkillPoints").transform);
            Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        SkillBoxItem = Boxs[0].transform.Cast<Transform>().ToArray();
        Transform SkillPoints_p0 = (from a in SkillBoxItem where a.gameObject.name == "SkillPoints" select a).FirstOrDefault();
        Boxs[0].gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Boxs[0].gameObject.GetComponent<RectTransform>().sizeDelta.x, 480 + ((SPM.UserSkills[SPM.SelectedID].skills.box[0].options.Length - 1) * 100));
        CalHeight(SPM.UserSkills[SPM.SelectedID].skills.box[0].options.Length, SkillPoints_p0.gameObject);
        Boxs[0].gameObject.GetComponent<SkillBoxManager>().SetSkillPointNumber(SPM.UserSkills[SPM.SelectedID].skills.box[0].options.Length);
        for (int i = 0; i < SPM.UserSkills[SPM.SelectedID].skills.box[1].options.Length - 1; i++)
        {
            GameObject Items = Instantiate(SkillPointPrefab) as GameObject;
            Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            Items.transform.SetParent(GameObject.Find("SkillBox(1)/SkillPoints").transform);
            Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        SkillBoxItem = Boxs[1].transform.Cast<Transform>().ToArray();
        Transform SkillPoints_p1 = (from a in SkillBoxItem where a.gameObject.name == "SkillPoints" select a).FirstOrDefault();
        Boxs[1].gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Boxs[1].gameObject.GetComponent<RectTransform>().sizeDelta.x, 480 + ((SPM.UserSkills[SPM.SelectedID].skills.box[1].options.Length - 1) * 100));
        CalHeight(SPM.UserSkills[SPM.SelectedID].skills.box[1].options.Length, SkillPoints_p1.gameObject);
        Boxs[1].gameObject.GetComponent<SkillBoxManager>().SetSkillPointNumber(SPM.UserSkills[SPM.SelectedID].skills.box[1].options.Length);
        for (int i = 0; i < SPM.UserSkills[SPM.SelectedID].skills.box[2].options.Length - 1; i++)
        {
            GameObject Items = Instantiate(SkillPointPrefab) as GameObject;
            Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            Items.transform.SetParent(GameObject.Find("SkillBox(2)/SkillPoints").transform);
            Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        SkillBoxItem = Boxs[2].transform.Cast<Transform>().ToArray();
        Transform SkillPoints_p2 = (from a in SkillBoxItem where a.gameObject.name == "SkillPoints" select a).FirstOrDefault();
        Boxs[2].gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Boxs[2].gameObject.GetComponent<RectTransform>().sizeDelta.x, 480 + ((SPM.UserSkills[SPM.SelectedID].skills.box[2].options.Length - 1) * 100));
        CalHeight(SPM.UserSkills[SPM.SelectedID].skills.box[2].options.Length, SkillPoints_p2.gameObject);
        Boxs[2].gameObject.GetComponent<SkillBoxManager>().SetSkillPointNumber(SPM.UserSkills[SPM.SelectedID].skills.box[2].options.Length);
        for (int i = 0; i < Boxs.Length; i++)
        {
            SkillBoxItem = Boxs[i].transform.Cast<Transform>().ToArray();
            Transform SkillPoints_p = (from a in SkillBoxItem where a.gameObject.name == "SkillPoints" select a).FirstOrDefault();
            InputField[] InsertSkillPoints = SkillPoints_p.gameObject.GetComponentsInChildren<InputField>();
            for(int j = 0; j < InsertSkillPoints.Length; j++)
            {
                InsertSkillPoints[j].text = SPM.UserSkills[SPM.SelectedID].skills.box[i].options[j];
            }
            Transform InsertSkillCost_p = (from a in SkillBoxItem where a.gameObject.name == "InsertSkillCost_p" select a).FirstOrDefault();
            InsertSkillCost_p.gameObject.GetComponentInChildren<InputField>().text = SPM.UserSkills[SPM.SelectedID].skills.box[i].cost;
            Transform InsertSkillPeriod_p = (from a in SkillBoxItem where a.gameObject.name == "InsertSkillPeriod_p" select a).FirstOrDefault();
            InsertSkillPeriod_p.gameObject.GetComponentInChildren<InputField>().text = SPM.UserSkills[SPM.SelectedID].skills.box[i].time;
        }
        SkillDescription.gameObject.GetComponent<InputField>().text = SPM.UserSkills[SPM.SelectedID].decep;
        ExpressCost.gameObject.GetComponent<InputField>().text = SPM.UserSkills[SPM.SelectedID].express.more_cost;
        ExpressTime.gameObject.GetComponent<InputField>().text = SPM.UserSkills[SPM.SelectedID].express.more_time;
        FixUnityBug();
        Express_p.gameObject.SetActive(false);
    }

    public void CalHeight(int ItemCount, GameObject SkillPoints)
    {
        int ItemHeight = 100;
        SkillPoints.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(SkillPoints.gameObject.GetComponent<RectTransform>().sizeDelta.x, ItemHeight * ItemCount);
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

    private WWWForm SendDataForEdit()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 19);
        web.AddField("skillcode", SPM.SelectedSkillCode);
        web.AddField("param", JsonUtility.ToJson(ParamsList));
        return web;
    }

    private IEnumerator DoEditSkill()
    {
        WWWForm WebGet = SendDataForEdit();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }

    public void DoEditCaller(int itema)
    {
        try
        {
            switch (itema)
            {
                case 0:
                    if (ParamsList.Params.Exists(o => o.Key == "name"))
                        ParamsList.Params.Remove(ParamsList.Params.Find(o => o.Key == "name"));
                    ParamsList.Params.Add(new Param() { Key = "name", Value = SkillName.gameObject.GetComponent<InputField>().text });
                    break;
                case 1:

                    break;
            }
        }
        catch
        {
            Debug.Log("Error");
        }
    }

    private WWWForm SendDataForDelete()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 18);
        web.AddField("skillcode", SPM.SelectedSkillCode);
        return web;
    }

    private IEnumerator DoDeleteSkill()
    {
        WWWForm WebGet = SendDataForDelete();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }

    public void Delete()
    {
        StartCoroutine(DoDeleteSkill());
    }

    private void FixUnityBug()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x - 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y - 0.01f);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x + 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + 0.01f);
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

    private void OnEnable()
    {
        FillGameObject();
    }
}
