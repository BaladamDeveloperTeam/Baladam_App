using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UPersian.Components;

public class MessageManager : MonoBehaviour
{

    private readonly string masterKey = "$2y$10$ooZRpgP3iGc6qYju9/03W.34alpAopQ7frXimfKEloqRdvXibbNem";
    private readonly string Url = "http://baladam1.me:81/api/GetLiperosal/This_is_PaSSWord_45M127*22";
    private GameObject GSM;
    public Session[] ActiveSession;
    public ReadSession[] ActiveSessionWeb;
    private int ActiveSessionCount;
    public GameObject ItemsPrefab;
    private GameObject[] AllItems;
    private Transform[] transform, transforms1;
    private dynamic[] Mydynamics;
    private string[] name;
    private Animator[] Anims;

    [System.Serializable]
    public class btn
    {
        public int id;
        public string name;
        public Button Button;
    }

    public List<btn> Btns = new List<btn>();

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
    }

    void Start()
    {
        StartCoroutine(GetActiveSession());
    }

    private WWWForm SendData()
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 9);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        return web;
    }

    private IEnumerator GetActiveSession()
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
            ActiveSession = JsonHelper.FromJson<Session>("{\"Items\": " + data.text + "}");
            ActiveSessionWeb = JsonHelper.FromJson<ReadSession>("{\"Items\": " + data.text + "}");
            for (int i = 0; i < ActiveSession.Length; i++)
            {
                if (ActiveSession[i].mode == "App")
                {
                    ActiveSessionCount++;
                }
            }
            for (int i = 0; i < ActiveSessionWeb.Length; i++)
            {
                if (ActiveSessionWeb[i].mode == "Web")
                {
                    ActiveSessionCount++;
                }
            }
            Mydynamics = new dynamic[ActiveSessionCount];
            for (int i = 0; i < ActiveSession.Length; i++)
            {
                if (ActiveSession[i].mode == "App")
                {
                    Mydynamics[i] = ActiveSession[i];
                }
            }
            for (int i = 0; i < ActiveSessionWeb.Length; i++)
            {
                if (ActiveSessionWeb[i].mode == "Web")
                {
                    Mydynamics[i] = ActiveSessionWeb[i];
                }
            }
        }
        AddPrefab();
    }

    private void AddPrefab()
    {
        AllItems = new GameObject[ActiveSessionCount];
        name = new string[ActiveSessionCount];
        Anims = new Animator[ActiveSessionCount];
        for (int i = 0; i < ActiveSessionCount; i++)
        {
            GameObject Items = Instantiate(ItemsPrefab) as GameObject;
            Items.transform.SetParent(GameObject.Find("Messages_p/Scroll View/Viewport/Content").transform);
            AllItems[i] = Items;
            if (Mydynamics[i].mode == "App")
            {
                transform = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
                transforms1 = transform[0].gameObject.transform.Cast<Transform>().ToArray();
                name[i] = Mydynamics[i].name;
                Btns.Add(new btn { id = i, name = Mydynamics[i].name, Button = transforms1[0].gameObject.GetComponent<Button>() });
                //transforms1[0].gameObject.GetComponent<Button>().onClick.AddListener(delegate { DeleteSession("test"); });
                transforms1[1].gameObject.GetComponent<RtlText>().text = "نوع دستگاه : " + Mydynamics[i].mode;
                transforms1[2].gameObject.GetComponent<RtlText>().text = "IP دستگاه : " + Mydynamics[i].log.address;
                transforms1[3].gameObject.GetComponent<RtlText>().text = "مدل دستگاه : " + Mydynamics[i].log.Device.DeviceModel;
                transforms1[4].gameObject.GetComponent<RtlText>().text = "نام دستگاه : " + Mydynamics[i].log.Device.DeviceUsername;
                transforms1[5].gameObject.GetComponent<RtlText>().text = "زمان ورود : " + Mydynamics[i].log.at.date;
                transforms1[6].gameObject.GetComponent<RtlText>().text = "شهر : " + Mydynamics[i].log.at.timezone;
            }
            if (Mydynamics[i].mode == "Web")
            {
                transform = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
                transforms1 = transform[0].gameObject.transform.Cast<Transform>().ToArray();
                name[i] = Mydynamics[i].name;
                Btns.Add(new btn { id = i, name = Mydynamics[i].name, Button = transforms1[0].gameObject.GetComponent<Button>() });
                //transforms1[0].gameObject.GetComponent<Button>().onClick.AddListener(() => { DeleteSession(name[i]); });
                transforms1[1].gameObject.GetComponent<RtlText>().text = "نوع دستگاه : " + Mydynamics[i].mode;
                transforms1[2].gameObject.GetComponent<RtlText>().text = "IP دستگاه : " + Mydynamics[i].log.address;
                transforms1[3].gameObject.GetComponent<RtlText>().text = "مرورگر دستگاه : " + Mydynamics[i].log.Device.Browser;
                transforms1[4].gameObject.GetComponent<RtlText>().text = "پلتفرم : " + Mydynamics[i].log.Device.Platform;
                transforms1[5].gameObject.GetComponent<RtlText>().text = "زمان ورود : " + Mydynamics[i].log.at.date;
                transforms1[6].gameObject.GetComponent<RtlText>().text = "شهر : " + Mydynamics[i].log.at.timezone;
            }

        }
        for (int i = 0; i < ActiveSessionCount; i++)
        {
            Button[] bu = (from a in Btns where a.id == i select a.Button).ToArray();
            string[] na = (from a in Btns where a.id == i select a.name).ToArray();
            int[] id = (from a in Btns where a.id == i select a.id).ToArray();
            bu[0].onClick.AddListener(() => { DeleteSession(na[0], id[0]); });
            Anims[i] = bu[0].gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Animator>();
        }
    }

    public void DeleteSession(string name, int item)
    {
        Debug.Log(name);
        StartCoroutine(DoDeleteSession(name, item));
        
    }

    private WWWForm SendDataForDelete(string name)
    {
        WWWForm web = new WWWForm();
        web.AddField("Master", masterKey);
        web.AddField("Chooser", 16);
        web.AddField("user", GSM.gameObject.GetComponent<Global_Script_Manager>().ReadUserName());
        web.AddField("session", name);
        return web;
    }

    private IEnumerator DoDeleteSession(string name, int item)
    {
        WWWForm WebGet = SendDataForDelete(name);
        WWW data = new WWW(Url, WebGet);
        Anims[item].SetTrigger("Delete");
        yield return data;

        Debug.Log(data.text);

        if (data.text == "Wrong" || string.IsNullOrEmpty(data.text) || data.text.Contains("<!DOCTYPE html>"))
        {
            Debug.Log("Error");
            Anims[item].SetTrigger("Error");
        }
        else
        {
            Debug.Log("Deleted");
            Destroy(AllItems[item]);
        }
    }
}