using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using security;

public class GetCat : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string CategoryJson = "";
    public CategoryNames CatInfo;
    public GameObject[] CategoryTitle;
    public GameObject Loading;


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
        CatInfo.name = JsonHelper.FromJson<string>("{\"Items\": " + CategoryJson + "}");
        //Debug.Log(name[0]);
        //Debug.Log(CatInfo[1].name);

        set_Title_text();

        

    }

    public void GetCatBut()
    {
        StartCoroutine(GetCats());
    }

    private void set_Title_text()
    {
        CategoryTitle = GameObject.FindGameObjectsWithTag("Category_title_text");
        for (int i = 0; i < CatInfo.name.Length; i++)
        {
            //CategoryTitle[i].gameObject.GetComponent<RtlText>().text = CatInfo[i].name;
            CategoryTitle[i].gameObject.GetComponent<RtlText>().text = CatInfo.name[i];
            //Debug.Log(CatInfo.name[i]);
        }

    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}



