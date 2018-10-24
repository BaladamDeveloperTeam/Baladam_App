using System.Collections;
using System.Collections.Generic;
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
    private GameObject GSM;
    private GameObject SkillName, SkillCategory, SkillSubCategory, SkillDescription;
    public GameObject[] SkillPoints, Cost, Period;

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

    public void SubmitBtn()
    {
        UserSkill[0].SkillName = SkillName.gameObject.GetComponent<InputField>().text;
        UserSkill[0].SkillCategory = SkillCategory.gameObject.GetComponent<Dropdown>().itemText.ToString();
        UserSkill[0].SkillSubCategory = SkillSubCategory.gameObject.GetComponent<Dropdown>().itemText.ToString();
        UserSkill[0].SkillDescription = SkillDescription.gameObject.GetComponent<InputField>().text;
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
