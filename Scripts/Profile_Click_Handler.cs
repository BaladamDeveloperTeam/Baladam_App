using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using security;
using UnityEngine.UI;
using System.Linq;

public class Profile_Click_Handler : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    public GameObject Drawer, BlackPanel, EditProfile_p, Skills_p, AddSkill_p, Messages_p;
    public Animator DrawerAnim, BlackPanelAnim;
    private Global_Script_Manager GSM;
    private ImageClass selectImage;
    private GameObject Unimgpicker;
    public Image Pro_Image, Banner_Image;
    public ParamList EditParam;
    public Transform[] MenuItems;
    public ParamList ParamsList;
    private bool IsDraweOpen = false;

    void Awake()
    { 
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
        MenuItems = Drawer.transform.Cast<Transform>().ToArray();
        selectImage = this.gameObject.GetComponent<ImageClass>();
        Unimgpicker = GameObject.Find("Profile_p/ClickHander/Unimgpicker");
        Unimgpicker.gameObject.SetActive(false);
    }

    void Start()
    {
        IsSeller();
        StartCoroutine(LoadProfileImage());
        ParamsList.Params.Add(new Param() { Key = "Pushe_Id", Value = Pushe.GetPusheId() });
        StartCoroutine(SavePusheID());
    }

    void Update ()
    {
        //GSM.gameObject.GetComponent<Global_Script_Manager>().SetValuetext();
        if (Input.GetKeyDown(KeyCode.Escape) && IsDraweOpen == true)
        {
            click(1);
            Global_Script_Manager.SetLog(4, "Back_Btn_Device CloseDrawe");
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && IsDraweOpen == false)
        {
            Global_Script_Manager.BNC.Home_nClick();
            Global_Script_Manager.SetLog(4, "Back_Btn_Device From Profile_p to Home_p");
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Messages_p.gameObject.activeSelf == true)
        {
            click(10);
            Global_Script_Manager.SetLog(4, "Back_Btn_Device From Messages_p to Profile_p");
        }
    }

    private void IsSeller()
    {
        Debug.Log("Is Seller = " + SignIn.IsSeller);
        if (SignIn.IsSeller == "0")
        {
            Transform skill = (from a in MenuItems where a.gameObject.name == "Menu_MySkills" select a).FirstOrDefault();
            skill.gameObject.SetActive(false);
            Transform Edit = (from a in MenuItems where a.gameObject.name == "Menu_EditProfile(Seller)" select a).FirstOrDefault();
            Edit.gameObject.SetActive(false);
        }
        else
        {
            Transform skill = (from a in MenuItems where a.gameObject.name == "Menu_BeSeller" select a).FirstOrDefault();
            skill.gameObject.SetActive(false);
            Transform Edit = (from a in MenuItems where a.gameObject.name == "Menu_EditProfile" select a).FirstOrDefault();
            Edit.gameObject.SetActive(false);
        }
    }

    public IEnumerator LoadProfileImage()
    {
        string url = GSM.ReadPro_imageURL();
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            yield return www;
            if(string.IsNullOrEmpty(www.error))
                Pro_Image.sprite = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    

    void click(int Item)
    {
        Coding coding = new Coding();
        switch(Item)
        {
            case 0:   //OpenDrawe
                if (Drawer.gameObject.activeInHierarchy == true)
                    DrawerAnim.SetTrigger("Start");
                if (Drawer.gameObject.activeInHierarchy == false)
                Drawer.gameObject.SetActive(true);
                Debug.Log("Open");
                //BlackPanelAnim.SetTrigger("Start");
                //BlackPanel.gameObject.SetActive(true);
                IsDraweOpen = true;
                Global_Script_Manager.SetLog(4, "OpenDrawe");
                break;
            case 1:     //CloseDrawe
                //BlackPanelAnim.SetTrigger("End");
                DrawerAnim.SetTrigger("End");
                IsDraweOpen = false;
                Global_Script_Manager.SetLog(4, "CloseDrawe");
                break;
            case 2:     //SelectPro_Image
                Unimgpicker.gameObject.SetActive(true);
                selectImage.OnPressShowPicker("Pro_Image.png");
                Pro_Image.sprite = selectImage.GetSprite();
                UploadFiles UP = new UploadFiles();
                UP.UploadFile(selectImage.GetPath(), "/storage/Profile/" + GSM.ReadUserName());
                EditParam.Params.Add(new Param() { Key = "pro_image", Value = "http://baladam1.me:81/storage/Profile/" + GSM.ReadUserName() + "/" + "Pro_Image.png"});
                StartCoroutine(DoEdit_ProImage());
                Unimgpicker.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "SelectPro_Image");
                break;
            case 3:     //OpenEditProfile
                CloseDrawerClick();
                EditProfile_p.gameObject.SetActive(true);
                Messages_p.gameObject.SetActive(false);
                AddSkill_p.gameObject.SetActive(false);
                Skills_p.gameObject.SetActive(false);
                GSM.FindObjectsForEdit();
                GSM.SetValuetextForEdit();
                Global_Script_Manager.SetLog(4, "OpenEditProfile");
                break;
            case 4:     //OpenMyProfile
                CloseDrawerClick();
                EditProfile_p.gameObject.SetActive(false);
                AddSkill_p.gameObject.SetActive(false);
                Skills_p.gameObject.SetActive(false);
                Messages_p.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "OpenMyProfile");
                break;
            case 5:     //OpenSkills
                CloseDrawerClick();
                //Skills_p.gameObject.SetActive(false);
                Skills_p.gameObject.SetActive(true);
                EditProfile_p.gameObject.SetActive(false);
                Messages_p.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "OpenSkills");
                break;
            case 6:     //DoExitBtn
                StartCoroutine(DoExit());
                Global_Script_Manager.SetLog(12, coding.Md5Sum(SystemInfo.deviceUniqueIdentifier));
                break;
            case 7:     //SelectBanner_Image
                Unimgpicker.gameObject.SetActive(true);
                selectImage.OnPressShowPicker("Banner_Image.png");
                Banner_Image.sprite = selectImage.GetSprite();
                UploadFiles UP1 = new UploadFiles();
                UP1.UploadFile(selectImage.GetPath(), "/storage/Profile/" + GSM.ReadUserName());
                EditParam.Params.Add(new Param() { Key = "banner_image", Value = "http://baladam1.me:81/storage/Profile/" + GSM.ReadUserName() + "/" + "Banner_Image.png" });
                StartCoroutine(DoEdit_ProImage());
                Unimgpicker.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "SelectBanner_Image");
                break;
            case 8:     //OpenMessage_p
                CloseDrawerClick();
                Messages_p.gameObject.SetActive(true);
                EditProfile_p.gameObject.SetActive(false);
                Skills_p.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "OpenMessage_p");
                break;
            case 9:     //OpenAddSkills
                AddSkill_p.gameObject.SetActive(true);
                Global_Script_Manager.SetLog(4, "OpenAddSkills");
                break;
            case 10:
                Messages_p.gameObject.SetActive(false);
                EditProfile_p.gameObject.SetActive(false);
                Skills_p.gameObject.SetActive(false);
                Global_Script_Manager.SetLog(4, "CloseMessage_p");
                break;
        }
    }

    public void OpenDrawerClick()
    {
        click(0);
    }

    public void CloseDrawerClick()
    {
        click(1);
    }

    public void SelectPro_ImageBtn()
    {
        click(2);
    }

    public void OpenEditProfile()
    {
        click(3);
    }

    public void OpenMyProfile()
    {
        click(4);   
    }

    public void OpenAddSkill()
    {
        click(5);
    }

    public void DoExitBtn()
    {
        click(6);
    }

    public void SelectBanner_Image()
    {
        click(7);
    }

    public void OpenMessage_p()
    {
        click(8);
    }

    public void OpenSkills_p()
    {
        click(9);
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
        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }  
        else
        {
            Debug.Log("Exit");
        }

    }

    private WWWForm SendDataEdit_ProImage()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 15);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("param", JsonUtility.ToJson(EditParam));
        return web;
    }

    private IEnumerator DoEdit_ProImage()
    {
        WWWForm WebGet = SendDataEdit_ProImage();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log("Upload ProImage To Database.");
        }

    }

    private WWWForm SendDataForPusheID()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 15);
        web.AddField("user", GSM.ReadUserName());
        web.AddField("param", JsonUtility.ToJson(ParamsList));
        return web;
    }

    private IEnumerator SavePusheID()
    {
        WWWForm WebGet = SendDataForPusheID();
        WWW data = new WWW(Url, WebGet);
        yield return data;

        Debug.Log(data.text);
    }
}
