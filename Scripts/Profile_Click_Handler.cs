using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using security;

public class Profile_Click_Handler : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    public GameObject Drawer, BlackPanel, EditProfile_p, AddSkill_p;
    public Animator DrawerAnim, BlackPanelAnim;
    private GameObject GSM;

    void Awake()
    { 
        GSM = GameObject.Find("Global script Manager");
        
    }
	
	void Update ()
    {
		//GSM.gameObject.GetComponent<Global_Script_Manager>().SetValuetext();
	}

    public void OpenDrawerClick()
    {
        click(0);
        Screen.fullScreen = false;
    }

    public void CloseDrawerClick()
    {
        click(1);
        Screen.fullScreen = false;
    }

    void click(int Item)
    {
        switch(Item)
        {
            case 0:   //OpenDrawe
                if (Drawer.gameObject.active == true)
                    DrawerAnim.SetTrigger("Start");
                if (Drawer.gameObject.active == false)
                Drawer.gameObject.SetActive(true);
                Debug.Log("Open");
                //BlackPanelAnim.SetTrigger("Start");
                //BlackPanel.gameObject.SetActive(true);
                break;
            case 1:
                //BlackPanelAnim.SetTrigger("End");
                DrawerAnim.SetTrigger("End");
                break;
        }
    }

    public void OpenEditProfile()
    {
        CloseDrawerClick();
        EditProfile_p.gameObject.SetActive(true);
        GSM.gameObject.GetComponent<Global_Script_Manager>().FindObjectsForEdit();
        GSM.gameObject.GetComponent<Global_Script_Manager>().SetValuetextForEdit();
    }

    public void OpenMyProfile()
    {
        CloseDrawerClick();
        EditProfile_p.gameObject.SetActive(false);
        AddSkill_p.gameObject.SetActive(false);
    }

    public void OpenAddSkill()
    {
        CloseDrawerClick();
        AddSkill_p.gameObject.SetActive(true);
    }
    private WWWForm SendData()
    {
        Coding coding = new Coding();
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 12);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("IMEI", coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
        return web;
    }

    private IEnumerator DoExit()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
        if (data.text == "Wrong" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }  
        else
        {
            Debug.Log("Exit");
        }

    }

    public void DoExitBtn()
    {
        StartCoroutine(DoExit());
    }
}
