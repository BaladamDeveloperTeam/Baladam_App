using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;
using UnityEngine.UI;
using security;

public class GetSubCategory : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private string SubCategoryJson = "";
    private SubCategoryInfo[] SubCatInfo;
    public GameObject ShowPlace;
    public GameObject ItemsPrefab, ItemsPrefabLine, Loading;
    public string CategoryID;
    private GameObject[] AllItems, AllItemsLine;

    private bool[] Fill = new bool[8];

    private WWWForm SendData()
    {
        Coding coding = new Coding();
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 3);
        web.AddField("DatabaseID", CategoryID);
        return web;
    }

    private IEnumerator GetSubCategorys()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        if (Fill[Category_Click_Handler.SelectedItem-1] == false)
        {
            Loading.gameObject.SetActive(true);
        }
            yield return data;
        if (Fill[Category_Click_Handler.SelectedItem-1] == false)
        {
            Loading.gameObject.SetActive(false);
        }

        Debug.Log(data.text);
        SubCategoryJson = data.text;

        SubCatInfo = JsonHelper.FromJson<SubCategoryInfo>("{\"Items\": " + SubCategoryJson + "}");

        Debug.Log("select :" + Category_Click_Handler.SelectedItem);
        SetSubCategory(Category_Click_Handler.SelectedItem);

    }

    public void GetSubCategoryBut(string databaseID)
    {
        CategoryID = databaseID;
        StartCoroutine(GetSubCategorys());
    }

    public void SetSubCategory(int Item)
    {
        int SubCategoryItemNumber = 0;
        AllItems = new GameObject[SubCatInfo.Length];
        AllItemsLine = new GameObject[SubCatInfo.Length];

        for (int i = 0; i < SubCatInfo.Length; i++)
        {
            //if (SubCatInfo[i].databaseID == Item.ToString() && Fill[Item - 1] == false)
            {

                GameObject Items = Instantiate(ItemsPrefab) as GameObject;
                Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                //Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Items.transform.SetParent(GameObject.Find("Category0" + Item).transform);
                AllItems[i] = Items;
                GameObject ItemsLine = Instantiate(ItemsPrefabLine) as GameObject;
                ItemsLine.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                ItemsLine.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                //ItemsLine.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                ItemsLine.transform.SetParent(GameObject.Find("Category0" + Item).transform);
                AllItemsLine[i] = ItemsLine;
                RtlText[] text = Items.gameObject.GetComponentsInChildren<RtlText>();
                Image image = Items.gameObject.GetComponentInChildren<Image>();
                text[0].text = SubCatInfo[i].SubName;
                text[1].text = SubCatInfo[i].SubNameEN;
            }
            //if (SubCatInfo[i].databaseID == Item.ToString())
            {
                SubCategoryItemNumber++;
            }
        }
        for(int i = 0; i < AllItems.Length; i++)
        {
            Debug.Log(AllItems[i].gameObject.GetComponent<RectTransform>().localScale);
            AllItems[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            AllItemsLine[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
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