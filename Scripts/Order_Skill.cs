using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order_Skill : MonoBehaviour
{

    public string SkillCode;
    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;

    private void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        SkillCode = GSM.ReadSelectedSkillCode();
        if (GSM.ReadSelectedSkillCode() == "No Item Selected")
            Debug.Log("No Item Selected!!!");
    }

    void Start ()
    {
		
	}


    private WWWForm SendDataForEdit()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 20);
        web.AddField("skillcode", SkillCode);
        web.AddField("shopper", GSM.ReadUserID());
        //web.AddField("seller", JsonUtility.ToJson(ParamsList));
        //web.AddField("box", JsonUtility.ToJson(ParamsList));
        //web.AddField("ex", JsonUtility.ToJson(ParamsList));
        return web;
    }

    private IEnumerator DoEditSkill()
    {
        WWWForm WebGet = SendDataForEdit();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }
}
