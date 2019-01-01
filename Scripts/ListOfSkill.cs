using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class ListOfSkill : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private MySkills[] UserSkills;
    private GameObject[] AllItems;
    private string GetJson = "";
    private Global_Script_Manager GSM;
    private Transform[] MySkilltransform_p, MySkilltransform;
    public GameObject SkillPrefab, ShowSkill_p;

    public List<SkillButton> SkillButton = new List<SkillButton>();

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    void Start()
    {
        StartCoroutine(GetAllUserSkills());
    }

    void Update()
    {

    }

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 17);
        web.AddField("user", GSM.ReadUserName());
        return web;
    }

    private IEnumerator GetAllUserSkills()
    {
        WWWForm WebGet = SendData();
        WWW data = new WWW(Url, WebGet);

        yield return data;

        GetJson = data.text;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
        }
        else
        {
            UserSkills = JsonHelper.FromJson<MySkills>("{\"Items\": " + GetJson + "}");
        }
        AddPrefab();
        CalSize(UserSkills.Length);
        //ShowPlace.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, CalHeight(UserSkills.Length));
    }

    public void AddPrefab()
    {
        AllItems = new GameObject[UserSkills.Length];
        for (int i = 0; i < UserSkills.Length; i++)
        {
            GameObject Items = Instantiate(SkillPrefab) as GameObject;
            Items.transform.SetParent(GameObject.Find("Profile_p/Main/Scroll View/Viewport/Content/Scroll View (1)/Viewport/Content").transform);
            AllItems[i] = Items;
            MySkilltransform_p = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
            MySkilltransform = MySkilltransform_p[0].gameObject.transform.Cast<Transform>().ToArray();
            if (UserSkills[i].url.Length > 0)
            {
                StartCoroutine(GetImageFromURL(UserSkills[i].url[0]));
            }
            MySkilltransform[1].gameObject.GetComponent<RtlText>().text = UserSkills[i].skills.box[0].cost + " ت";
            MySkilltransform[2].gameObject.GetComponent<RtlText>().text = UserSkills[i].name;
            MySkilltransform[4].gameObject.GetComponent<RtlText>().text = UserSkills[i].rate.ToString();
            SkillButton.Add(new SkillButton { id = i, _id = UserSkills[i].pz_id, name = UserSkills[i].name, SkillCode = UserSkills[i].skillCode, Button = AllItems[i].gameObject.GetComponent<Button>() });
        }
        for (int i = 0; i < UserSkills.Length; i++)
        {
            Button bu = (from a in SkillButton where a.id == i select a.Button).FirstOrDefault();
            string _id = (from a in SkillButton where a.id == i select a._id).FirstOrDefault();
            string SkillCode = (from a in SkillButton where a.id == i select a.SkillCode).FirstOrDefault();
            int id = (from a in SkillButton where a.id == i select a.id).FirstOrDefault();
            bu.onClick.AddListener(() => { ShowSkill(_id, SkillCode, id); });
        }
    }

    public void ShowSkill(string SellerID, string SkillCode, int id)
    {
        //GSM.SetSkillCode(SkillCode);
        //GSM.SetSellerID(SellerID);
        //ShowSkill_p.gameObject.SetActive(true);
        //Global_Script_Manager.SetLog(3, SkillCode);
        //Debug.Log(SkillCode);
    }

    public IEnumerator GetImageFromURL(string URL)
    {
        string url = URL;
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
                MySkilltransform[0].gameObject.GetComponent<Image>().sprite = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void CalSize(int Items)
    {
        GameObject.Find("Profile_p/Main/Scroll View/Viewport/Content/Scroll View (1)/Viewport/Content").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(((Items * 292) - 800), 377.6f);
    }
}
