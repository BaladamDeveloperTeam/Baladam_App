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
    List<int> AppPlace = new List<int>();
    List<int> WebPlace = new List<int>();
    public GameObject ItemsPrefab;
    public GameObject[] AllItems;
    private Transform[] transform, transforms1;

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
    }

    void Start ()
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
            for(int i = 0; i < ActiveSession.Length; i++)
            {
                if (ActiveSession[i].mode == "App")
                {
                    ActiveSessionCount++;
                    AppPlace.Add(i);
                }
            }
            for (int i = 0; i < ActiveSessionWeb.Length; i++)
            {
                if (ActiveSessionWeb[i].mode == "Web")
                {
                    ActiveSessionCount++;
                    WebPlace.Add(i);
                }
            }
        }
        AddPrefab();
    }

    private void AddPrefab()
    {
        AllItems = new GameObject[ActiveSessionCount];
        for(int i = 0; i < ActiveSessionCount; i++)
        {
            GameObject Items = Instantiate(ItemsPrefab) as GameObject;
            Items.transform.SetParent(GameObject.Find("Messages_p/Scroll View/Viewport/Content").transform);
            AllItems[i] = Items;
            
        }
        for(int i = 0; i < AllItems.Length; i++)
        {
            transform = AllItems[i].gameObject.transform.Cast<Transform>().ToArray();
            transforms1 = transform[0].gameObject.transform.Cast<Transform>().ToArray();
            transforms1[0].gameObject.GetComponent<Button>().onClick.AddListener(DeleteSession);
        }
    }

    public void DeleteSession()
    {
        Debug.Log("click");
    }
}