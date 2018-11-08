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
    public GameObject Drawer, BlackPanel, EditProfile_p, AddSkill_p;
    public Animator DrawerAnim, BlackPanelAnim;
    private GameObject GSM;
    private ImageClass selectImage;
    public Image Pro_Image, Banner_Image;
    public ParamList EditParam;
    public Transform[] MenuItems;

    void Awake()
    { 
        GSM = GameObject.Find("Global script Manager");
        MenuItems = Drawer.transform.Cast<Transform>().ToArray();
        selectImage = this.gameObject.GetComponent<ImageClass>();
    }

    void Start()
    {
        IsSeller();
        StartCoroutine(LoadProfileImage());
    }

    void Update ()
    {
		//GSM.gameObject.GetComponent<Global_Script_Manager>().SetValuetext();
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
        string url = GSM.gameObject.GetComponent<Global_Script_Manager>().ReadPro_imageURL();
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
            case 1:     //CloseDrawe
                //BlackPanelAnim.SetTrigger("End");
                DrawerAnim.SetTrigger("End");
                break;
            case 2:     //SelectPro_Image
                Coding coding = new Coding();
                selectImage.OnPressShowPicker("Pro_Image.png");
                Pro_Image.sprite = selectImage.GetSprite();
                UploadFiles UP = new UploadFiles();
                UP.UploadFile(selectImage.GetPath(), "/storage/Profile/" + GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
                EditParam.Params.Add(new Param() { Key = "pro_image", Value = "http://baladam1.me:81/storage/Profile/" + GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName() + "/" + "Pro_Image.png"});
                StartCoroutine(DoEdit_ProImage());
                break;
            case 3:     //OpenEditProfile
                CloseDrawerClick();
                EditProfile_p.gameObject.SetActive(true);
                GSM.gameObject.GetComponent<Global_Script_Manager>().FindObjectsForEdit();
                GSM.gameObject.GetComponent<Global_Script_Manager>().SetValuetextForEdit();
                break;
            case 4:     //OpenMyProfile
                CloseDrawerClick();
                EditProfile_p.gameObject.SetActive(false);
                AddSkill_p.gameObject.SetActive(false);
                break;
            case 5:     //OpenAddSkill
                CloseDrawerClick();
                AddSkill_p.gameObject.SetActive(true);
                EditProfile_p.gameObject.SetActive(false);
                break;
            case 6:     //DoExitBtn
                StartCoroutine(DoExit());
                break;
            case 7:     //SelectBanner_Image
                selectImage.OnPressShowPicker("Banner_Image.png");
                Banner_Image.sprite = selectImage.GetSprite();
                UploadFiles UP1 = new UploadFiles();
                UP1.UploadFile(selectImage.GetPath(), "/storage/Profile/" + GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
                EditParam.Params.Add(new Param() { Key = "banner_image", Value = "http://baladam1.me:81/storage/Profile/" + GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName() + "/" + "Banner_Image.png" });
                StartCoroutine(DoEdit_ProImage());
                break;
        }
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
        if (data.text == "Wrong" || data.text == "" || data.text == null || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log("Upload ProImage To Database.");
        }

    }
}
