using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order_Skill : MonoBehaviour
{

    private string SkillCode;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private string GetJson = "";
    public GameObject Loading;
    public MySkills[] SelectedSkill;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        SkillCode = GSM.ReadSelectedSkillCode();
        if (GSM.ReadSelectedSkillCode() == "No Item Selected")
            Debug.Log("No Item Selected!!!");
        StartCoroutine(GetSelectedSkill());
    }

    void Start ()
    {
		
	}

    private WWWForm SendDataForBuy()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 20);
        web.AddField("skillcode", SkillCode);
        web.AddField("shopper", GSM.ReadUserID());
        web.AddField("seller", SelectedSkill[0].pz_id);
        web.AddField("box", "");                                            //Need fix
        web.AddField("ex", "");                                             //Need fix    
        return web;
    }

    private IEnumerator BuySkill()
    {
        WWWForm WebGet = SendDataForBuy();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }

    private WWWForm SendDataForReadSkill()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 21);
        web.AddField("skillcode", SkillCode);
        return web;
    }

    private IEnumerator GetSelectedSkill()
    {
        WWWForm WebGet = SendDataForReadSkill();
        WWW data = new WWW(Url, WebGet);
        Loading.gameObject.SetActive(true);
        yield return data;
        Loading.gameObject.SetActive(false);
        GetJson = data.text;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            SelectedSkill = JsonHelper.FromJson<MySkills>("{\"Items\": [" + GetJson + "]}");
        }
    }
}
