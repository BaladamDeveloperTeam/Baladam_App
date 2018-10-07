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
    private readonly string Url = "http://127.0.0.2:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";

    public RtlText ErrorText;
    public InputField Searchtext;
    public CategoryInfo[] Categoryinfo;
    private string SearchResult = "";

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

            Categoryinfo = JsonHelper.FromJson<CategoryInfo>("{\"Items\": " + SearchResult + "}");
        }

        if (data.text == "bad post" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
           ErrorText.gameObject.SetActive(true);

    }

    public void SearchTextUpdate()
    {
        if(Searchtext.text.Length >= 2)
            StartCoroutine(DoSearch());
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
