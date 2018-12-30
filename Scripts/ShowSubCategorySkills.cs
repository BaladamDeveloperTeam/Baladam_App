using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSubCategorySkills : MonoBehaviour
{

    private string SubcategoryId;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private string GetJson = "";
    public GameObject Loading;
    public SelectedSkills[] SubcategorySkill;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    private void OnEnable()
    {
        SubcategoryId = GSM.ReadSelectedSubcategoryId();
        if (SubcategoryId == "No Item Selected")
            Debug.Log("No Item Selected!!!");
        StartCoroutine(GetSubcategorySkills());
        FindObjects();
    }

    private void FindObjects()
    {

    }

    private void FillObject()
    {

    }

    private WWWForm SendDataForReadSkills()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 23);
        web.AddField("subid", SubcategoryId);
        return web;
    }

    private IEnumerator GetSubcategorySkills()
    {
        WWWForm WebGet = SendDataForReadSkills();
        WWW data = new WWW(Url, WebGet);

        Loading.gameObject.SetActive(true);
        yield return data;

        GetJson = data.text;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            SubcategorySkill = JsonHelper.FromJson<SelectedSkills>("{\"Items\": [" + GetJson + "]}");
        }
        FillObject();
        Loading.gameObject.SetActive(false);
    }

    void Start()
    {

    }
}
