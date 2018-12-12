using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using security;
using System.Linq;

public class GetCat : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string CategoryJson = "";
    public string[] CatInfo;
    public LoadCategory[] LoadCategory;
    public GameObject[] CategoryTitle;
    public GameObject Loading;
    private Global_Script_Manager GSM;

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }


    private WWWForm SendData()
    {
        Coding coding = new Coding();
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 1);
        return web;
    }

    private IEnumerator GetCats()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        Loading.gameObject.SetActive(true);
        yield return data;
        Loading.gameObject.SetActive(false);

        Debug.Log(data.text);
        CategoryJson = data.text;

        //CatInfo = JsonHelper.FromJson<CategoryNames>("{\"Items\": " + CategoryJson + "}");
        ///CatInfo.name = JsonHelper.FromJson<string>("{\"Items\":" + CategoryJson + "}");
        LoadCategory = JsonHelper.FromJson<LoadCategory>("{\"Items\": " + CategoryJson + "}");
        CatInfo = (from a in LoadCategory select a.name).ToArray();
        GSM.SetCategoryName(CatInfo);
        GSM.SetLoadCategory(LoadCategory);
    }

    public void GetCatBut()
    {
        StartCoroutine(GetCats());
    }

    public void set_Title_text()
    {
        CategoryTitle = GameObject.FindGameObjectsWithTag("Category_title_text");
        for (int i = 0; i < CatInfo.Length; i++)
        {
            //CategoryTitle[i].gameObject.GetComponent<RtlText>().text = CatInfo[i].name;
            CategoryTitle[i].gameObject.GetComponent<RtlText>().text = CatInfo[i];
            //Debug.Log(CatInfo.name[i]);
        }

    }
}



