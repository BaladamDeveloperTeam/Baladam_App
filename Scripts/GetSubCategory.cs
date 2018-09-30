using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using UnityEngine.UI;

public class GetSubCategory : MonoBehaviour
{

    public string masterKey = "12345678";
    public string Url = "http://127.0.0.2:81/GetLiperosals/This_is_PaSSWord_45M127*22";
    private string SubCategoryJson = "";
    public SubCategoryInfo[] SubCatInfo;
    public GameObject ShowPlace;
    public GameObject ItemsPrefab, ItemsPrefabLine;

    public  bool[] Fill = new bool[8];

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("serverKeycode", masterKey);
        return web;
    }

    private IEnumerator GetSubCategorys()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url);
        yield return data;

        Debug.Log(data.error);
        SubCategoryJson = data.text;

        SubCatInfo = JsonHelper.FromJson<SubCategoryInfo>("{\"Items\": " + SubCategoryJson + "}");

        Debug.Log("select :" + Category_Click_Handler.SelectedItem);
        SetSubCategory(Category_Click_Handler.SelectedItem);

    }

    public void GetSubCategoryBut()
    {
        StartCoroutine(GetSubCategorys());
    }

    public void SetSubCategory(int Item)
    {
        int SubCategoryItemNumber = 0;

        for (int i = 0; i < SubCatInfo.Length; i++)
        {
            if (SubCatInfo[i].databaseID == Item.ToString() && Fill[Item - 1] == false)
            {

                GameObject Items = Instantiate(ItemsPrefab) as GameObject;
                Items.transform.SetParent(GameObject.Find("Category0" + Item).transform);
                GameObject ItemsLine = Instantiate(ItemsPrefabLine) as GameObject;
                ItemsLine.transform.SetParent(GameObject.Find("Category0" + Item).transform);
                RtlText[] text = Items.gameObject.GetComponentsInChildren<RtlText>();
                Image image = Items.gameObject.GetComponentInChildren<Image>();
                text[0].text = SubCatInfo[i].SubName;
                text[1].text = SubCatInfo[i].SubNameEN;
            }
            if (SubCatInfo[i].databaseID == Item.ToString())
            {
                SubCategoryItemNumber++;
            }
        }
        Debug.Log(Item);
        Fill[Item - 1] = true;
        ShowPlace.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, CalHeight(SubCategoryItemNumber));
        switch (Item)
        {
            case 1:
                
                break;

        }
    }

    private int CalHeight(int ItemCount)
    {
        int CategoryImageHeight = 400;
        int ItemHeight = 117;
        int LineHeight = 25;
        return CategoryImageHeight + (ItemHeight * ItemCount) + (LineHeight * (ItemCount - 1)); 
    }

    void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}
}