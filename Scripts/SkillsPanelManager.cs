using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class SkillsPanelManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private Global_Script_Manager GSM;
    private string GetJson = "";
    private GameObject[] AllItems;
    private GameObject ShowPlace;
    private Sprite Temp;
    public GameObject MySkillPrefab, AddNewSkillPrefab, AddSkill_p, EditSkills_p;
    public MySkills[] UserSkills;
    public GameObject Loading;
    public Transform[] MySkilltransform_p, MySkilltransform;
    [HideInInspector]
    public string SelectedSkillCode;
    [HideInInspector]
    public int SelectedID;

    [System.Serializable]
    public class btn
    {
        public int id;
        public string _id;
        public string name;
        public string SkillCode;
        public Button Button;
    }

    public List<btn> Btns = new List<btn>();


    void Awake()
    {
        //Loading = GameObject.Find("Skills/WaitForReadSkill");
        ShowPlace = GameObject.Find("Skills/ShowAllSkills/Scroll View/Viewport/Content");
        GSM = GameObject.Find("Global script Manager").gameObject.GetComponent<Global_Script_Manager>();
    }

    void Start()
    {
        StartCoroutine(GetAllUserSkills());
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
            UserSkills = JsonHelper.FromJson<MySkills>("{\"Items\": " + GetJson + "}");
        }
        AddPrefab();
        ShowPlace.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, CalHeight(UserSkills.Length));
    }

    private void AddPrefab()
    {
        AllItems = new GameObject[UserSkills.Length];
        for(int i = 0; i < UserSkills.Length; i++)
        {
            GameObject Items = Instantiate(MySkillPrefab) as GameObject;
            Items.transform.SetParent(GameObject.Find("Skills/ShowAllSkills/Scroll View/Viewport/Content").transform);
            AllItems[i] = Items;
            MySkilltransform_p = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
            MySkilltransform = MySkilltransform_p[0].gameObject.transform.Cast<Transform>().ToArray();
            if (UserSkills[i].url.Length > 0)
            {
                StartCoroutine(GetImageFromURL(UserSkills[i].url[0]));
                MySkilltransform[0].gameObject.GetComponent<Image>().sprite = Temp;
            }
            MySkilltransform[1].gameObject.GetComponent<RtlText>().text = UserSkills[i].skills.box[0].cost + " ت";
            MySkilltransform[2].gameObject.GetComponent<RtlText>().text = UserSkills[i].name;
            MySkilltransform[4].gameObject.GetComponent<RtlText>().text = UserSkills[i].rate.ToString();
            Btns.Add(new btn { id = i, _id = UserSkills[i]._id, name = UserSkills[i].name, SkillCode = UserSkills[i].skillCode, Button = AllItems[i].gameObject.GetComponent<Button>() });
        }
        for (int i = 0; i < UserSkills.Length; i++)
        {
            Button[] bu = (from a in Btns where a.id == i select a.Button).ToArray();
            string[] _id = (from a in Btns where a.id == i select a._id).ToArray();
            string[] SkillCode = (from a in Btns where a.id == i select a.SkillCode).ToArray();
            int[] id = (from a in Btns where a.id == i select a.id).ToArray();
            bu[0].onClick.AddListener(() => { ShowSkill(_id[0], SkillCode[0], id[0]); });
        }
        GameObject AddNew = Instantiate(AddNewSkillPrefab) as GameObject;
        AddNew.transform.SetParent(GameObject.Find("Skills/ShowAllSkills/Scroll View/Viewport/Content").transform);
        AddNew.gameObject.GetComponent<Button>().onClick.AddListener(delegate {
            AddSkill_p.gameObject.SetActive(true);
        });
    }

    private int CalHeight(int ItemCount)
    {
        int ItemHeight = 500;
        if (ItemCount % 2 == 0)
            return ((((ItemCount) / 2) + 1) * ItemHeight);
        else
            return (((ItemCount + 1) / 2) * ItemHeight);
    }

    public IEnumerator GetImageFromURL(string URL)
    {
        string url = URL;
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            //Temp = SpriteFromTex2D(www.texture);
            yield return www;
            //if (string.IsNullOrEmpty(www.error))
                Temp = SpriteFromTex2D(www.texture);
            www.Dispose();
            www = null;
        }
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void ShowSkill(string _id, string SkillCode, int id)
    {
        SelectedSkillCode = SkillCode;
        SelectedID = id;
        EditSkills_p.gameObject.SetActive(true);
        Debug.Log(_id);
    }
}
