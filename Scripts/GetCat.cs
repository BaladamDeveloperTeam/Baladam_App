using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;

public class GetCat : MonoBehaviour
{

    public string masterKey = "2210";
    public string Url = "http://127.0.0.2:81/GetLiperosal/This_is_PaSSWord_45M127*2210";
    public string CategoryJson = "";
    public CategoryInfo[] CatInfo;
    public GameObject[] CategoryTitle;


    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("serverKeycode", masterKey);
        return web;
    }

    private IEnumerator GetCats()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url);
        yield return data;
       
        Debug.Log(data.text);
        CategoryJson = data.text;

        CatInfo = JsonHelper.FromJson<CategoryInfo>("{\"Items\": " + CategoryJson + "}");
        Debug.Log(CatInfo[0].name);
        Debug.Log(CatInfo[1].name);

        set_Title_text();

        //yield return new WaitForSeconds(5);

    }

    public void GetCatBut()
    {
        StartCoroutine(GetCats());
        //Object a = JsonUtility.FromJson<GetCat>(CategoryJson);


        //CategoryInfo CF = new CategoryInfo();
        //CF = CF.CreateFromJSON(CategoryJson);
        //Debug.Log("Dooooo");
        //Debug.Log(CF.name);

        //CategoryInfo myClass = new CategoryInfo();
        //CFI = JsonUtility.FromJson<CategoryInfoList>(CategoryJson);
        //Debug.Log(CFI.CategoryInfo);


        

    }

    private void set_Title_text()
    {
        CategoryTitle = GameObject.FindGameObjectsWithTag("Category_title_text");
        for (int i = 0; i < CatInfo.Length; i++)
        {
            CategoryTitle[i].gameObject.GetComponent<RtlText>().text = CatInfo[i].name;
        }

    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}



