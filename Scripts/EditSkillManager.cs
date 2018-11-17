using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSkillManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private SubCategoryInfo[] SubCatInfo;
    private string SubCategoryJson = "";
    private GameObject SkillName, SkillCategory, SkillSubCategory, SkillDescription, Express_p
        , ExpressCost, ExpressTime, Loading, AddSkillMessage, ImagePicker_p, ImagePicker_Content, ImagePickerNumber;
    public Dropdown SelectCategory, SelectSubCategory;
    private Toggle IsExpress;
    private SkillsPanelManager SPM;
    public ParamList ParamsList;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        foreach (string CatText in GSM.CatInfo.name)
        {
            SelectCategory.options.Add(new Dropdown.OptionData() { text = CatText });
        }
        FindObject();
        FillGameObject();
    }

    private void FindObject()
    {
        string ObjectPath = "Skills/EditSkill_p/Scroll View/Viewport/Content/";
        SkillName = GameObject.Find(ObjectPath + "InsertSkillName_p/InsertSkillName");
        SkillCategory = GameObject.Find(ObjectPath + "SelectCategory_p/SelectCategory");
        SkillSubCategory = GameObject.Find(ObjectPath + "SelectSubCategory_p/SelectSubCategory");
        SkillDescription = GameObject.Find(ObjectPath + "InsertDescription_p/InsertDescription");
        Express_p = GameObject.Find(ObjectPath + "Express_p");
        ExpressCost = GameObject.Find(ObjectPath + "InsertExpressCost_p/InsertExpressCost");
        ExpressTime = GameObject.Find(ObjectPath + "InsertExpressTime_p/InsertExpressTime");
        Express_p.gameObject.SetActive(false);
        Loading = GameObject.Find("Skills/EditSkill_p/WaitForAddSkill");
        Loading.gameObject.SetActive(false);
        AddSkillMessage = GameObject.Find(ObjectPath + "AddSkillMessage");
        AddSkillMessage.gameObject.SetActive(false);
        //ImagePicker_p = GameObject.Find(ObjectPath + "ImagePicker_p");
        //ImagePicker_Content = GameObject.Find("ImagePicker_p/Images/ScrollView/Viewport/Content");
        //ImagePickerNumber = GameObject.Find("ImagePicker_p/Bottom/ImageCounter");
        //ImagePicker_p.gameObject.SetActive(false);
        SPM = GameObject.Find("Profile_p/Skills/Skills_p Manager").gameObject.GetComponent<SkillsPanelManager>();

        IsExpress = GameObject.Find("IsExpress_t").GetComponent<Toggle>();
    }

    private void FillGameObject()
    {
        SkillName.gameObject.GetComponent<InputField>().text = SPM.UserSkills[SPM.SelectedID].name;
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

    private WWWForm SendDataFomEdit()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 19);
        web.AddField("skillcode", SPM.SelectedSkillCode);
        web.AddField("param", JsonUtility.ToJson(ParamsList));
        return web;
    }

    private IEnumerator DoEditSkill(int item)
    {
        WWWForm WebGet = SendDataFomEdit();
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

    private void OnEnable()
    {
        FillGameObject();
    }
}
