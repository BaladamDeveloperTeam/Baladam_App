using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;

public class Search_panel_Handler : MonoBehaviour
{

    public RtlText PlaceHolder;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public RtlText ErrorText;
    public InputField Searchtext;
    public SearchResult[] SearchedResult;
    private string SearchResult = "";
    public GameObject SearchedUserPrefab, SearchedCategoryPrefab, SearchedGigsPrefab;
    private GameObject[] US, CS, GS;

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 7);
        web.AddField("key", Searchtext.text);
        return web;
    }

    private IEnumerator DoSearch()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
        if (data.text != "[]" && data.text != "[ ]" && data.text != null && data.text != "")
        {
            SearchResult = data.text;

            SearchedResult = JsonHelper.FromJson<SearchResult>("{\"Items\": [" + SearchResult + "] }");
        }

        if (data.text == "bad post" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
           ErrorText.gameObject.SetActive(true);

    }

    public void SearchTextUpdate()
    {
        if(Searchtext.text.Length >= 2)
            StartCoroutine(DoSearch());
    }

    private void FixedUpdate()
    {
        ShowCategory();
        ShowUser();
        ShowGigs();
        if (Searchtext.text == "" || Searchtext.text.Length == 0)
        {
            SearchedResult[0].category = new CategoryInfo[0];
            SearchedResult[0].user = new UserSearchInfo[0];
            SearchedResult[0].gig = new gigsInfo[0];

        }
        if (SearchedResult[0].user.Length == 0)
        {
            GameObject[] US_D = GameObject.FindGameObjectsWithTag("SearchedUser");
            for (int i = 0; i < US_D.Length; i++)
            {
                Destroy(US_D[i]);
            }
        }
        if(SearchedResult[0].category.Length == 0)
        {
            GameObject[] CS_D = GameObject.FindGameObjectsWithTag("SearchedCategory");
            for (int i = 0; i < CS_D.Length; i++)
            {
                Destroy(CS_D[i]);
            }
        }
        if (SearchedResult[0].gig.Length == 0)
        {
            GameObject[] GS_D = GameObject.FindGameObjectsWithTag("SearchedGigs");
            for (int i = 0; i < GS_D.Length; i++)
            {
                Destroy(GS_D[i]);
            }
        }
    }

    private void ShowCategory()
    {
        CS = GameObject.FindGameObjectsWithTag("SearchedCategory");
        if(SearchedResult[0].category.Length != 0 && SearchedResult[0].category != null && Searchtext.text.Length >= 2)
        {
            for (int i = 0; i < SearchedResult[0].category.Length; i++)
            {
                GameObject Items = Instantiate(SearchedCategoryPrefab) as GameObject;
                Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                Items.transform.SetParent(GameObject.Find("Search_p/SearchedUser_Show").transform);
                Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            for (int i = 0; i < CS.Length; i++)
            {
                if (CS.Length == SearchedResult[0].category.Length)
                {
                    CS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RtlText>().text = SearchedResult[0].category[i].nameEN;
                    CS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<RtlText>().text = SearchedResult[0].category[i].name;
                }
            }
            //TODO:improve Performance
            GameObject[] CS_D = GameObject.FindGameObjectsWithTag("SearchedCategory");
            for (int i = SearchedResult[0].category.Length; i < CS_D.Length; i++)
            {
                Destroy(CS_D[i]);
            }
        }
        if (Searchtext.text == "" || Searchtext.text == null)
        {
            //TODO:improve Performance
            GameObject[] CS_D = GameObject.FindGameObjectsWithTag("SearchedCategory");
            for (int i = 0; i < CS_D.Length; i++)
            {
                Destroy(CS_D[i]);
            }
        }
    }

    private void ShowUser()
    {
        US = GameObject.FindGameObjectsWithTag("SearchedUser");
        if (SearchedResult[0].user.Length != 0 && SearchedResult[0].user != null && Searchtext.text.Length >= 2)
        {

            for (int i = 0; i < SearchedResult[0].user.Length; i++)
            {
                GameObject Items = Instantiate(SearchedUserPrefab) as GameObject;
                Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                Items.transform.SetParent(GameObject.Find("Search_p/SearchedUser_Show").transform);
                Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            for (int i = 0; i < US.Length; i++)
            {
                if (US.Length == SearchedResult[0].user.Length)
                {
                    US[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RtlText>().text = SearchedResult[0].user[i].username;
                    US[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<RtlText>().text = SearchedResult[0].user[i].name;
                    US[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<RtlText>().text = "سطح : " + SearchedResult[0].user[i].lvl;
                    US[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<RtlText>().text = "پروژه های تکمیل شده : " + SearchedResult[0].user[i].f_prj.ToString();
                    US[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<RtlText>().text = "امتیاز : " + SearchedResult[0].user[i].rate.ToString();
                }
            }
            //TODO:improve Performance
            GameObject[] US_D = GameObject.FindGameObjectsWithTag("SearchedUser");
            for (int i = SearchedResult[0].user.Length; i < US_D.Length; i++)
            {
                Destroy(US_D[i]);
                //Destroy(US[i]);
            }
        }
        if (Searchtext.text == "" || Searchtext.text == null)
        {
            //TODO:improve Performance
            GameObject[] US_D = GameObject.FindGameObjectsWithTag("SearchedUser");
            for (int i = 0; i < US_D.Length; i++)
            {
                Destroy(US_D[i]);
                //Destroy(US[i]);
            }
        }
    }

    private void ShowGigs()
    {
        GS = GameObject.FindGameObjectsWithTag("SearchedGigs");
        if (SearchedResult[0].gig.Length != 0 && SearchedResult[0].gig != null && Searchtext.text.Length >= 2)
        {

            for (int i = 0; i < SearchedResult[0].gig.Length; i++)
            {
                GameObject Items = Instantiate(SearchedGigsPrefab) as GameObject;
                Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                Items.transform.SetParent(GameObject.Find("Search_p/SearchedUser_Show").transform);
                Items.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            for (int i = 0; i < GS.Length; i++)
            {
                GS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<RtlText>().text = null;
                if (GS.Length == SearchedResult[0].gig.Length)
                {
                    GS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RtlText>().text = SearchedResult[0].gig[i].pz_id;
                    GS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<RtlText>().text = SearchedResult[0].gig[i].name;
                    for(int j = 0; j <  SearchedResult[0].gig[0].gigs.Length; j++)
                        GS[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<RtlText>().text += " " + SearchedResult[0].gig[i].gigs[j];
                }
            }
            //TODO:improve Performance
            GameObject[] GS_D = GameObject.FindGameObjectsWithTag("SearchedGigs");
            for (int i = SearchedResult[0].gig.Length; i < GS_D.Length; i++)
            {
                Destroy(GS_D[i]);
            }
        }
        if (Searchtext.text == "" || Searchtext.text == null)
        {
            //TODO:improve Performance
            GameObject[] GS_D = GameObject.FindGameObjectsWithTag("SearchedGigs");
            for (int i = 0; i < GS_D.Length; i++)
            {
                Destroy(GS_D[i]);
            }
        }
    }

    IEnumerator Type()
    {
        PlaceHolder.text = "";
        PlaceHolder.text += "ب";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ر";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ن";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ا";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "م";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ه";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += " ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ی";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﺴ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﯾ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﻮ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﻧ";
    }
}
