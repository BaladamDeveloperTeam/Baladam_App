using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class SkillBoxManager : MonoBehaviour
{

    private GameObject[] SkillBoxs;
    private int SkillPointNumber = 1;
    public RtlText SkillNumber;
    public GameObject SkillPoints, SkillPointPrefab;
    public int NameNumber;

    private void Awake()
    {
        SkillBoxs = GameObject.FindGameObjectsWithTag("SkillBox");
        //SkillNumber.text = "بسته شماره ی " + SkillBoxs.Length;
    }

    private void CalHeight(int ItemCount)
    {
        int ItemHeight = 100;
        SkillPoints.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(SkillPoints.gameObject.GetComponent<RectTransform>().sizeDelta.x, ItemHeight * ItemCount);
        
    }

    public void AddToSkillPointNumber()
    {
        SkillPointNumber++;
        CalHeight(SkillPointNumber);
        FixUnityBug();
        AddSkillPoint();
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + 100);
    }

    public void RemoveToSkillPointNumber()
    {
        if(SkillPointNumber > 1)
        {
            SkillPointNumber--;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y - 100);
        }
        CalHeight(SkillPointNumber);
        FixUnityBug();
        RemoveSkillPoint();
    }

    private void AddSkillPoint()
    {
        GameObject Items = Instantiate(SkillPointPrefab) as GameObject;
        Items.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        Items.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        if (NameNumber == 0)
            Items.transform.SetParent(GameObject.Find("SkillBox/SkillPoints").transform);
        else if (NameNumber == 1)
            Items.transform.SetParent(GameObject.Find("SkillBox(1)/SkillPoints").transform);
        else if (NameNumber == 2)
            Items.transform.SetParent(GameObject.Find("SkillBox(2)/SkillPoints").transform);
    }

    private void RemoveSkillPoint()
    {
        if(NameNumber == 0)
            Destroy(GameObject.Find("SkillBox/SkillPoints/InsertSkillPoint_p(Clone)"));
        else if (NameNumber == 1)
            Destroy(GameObject.Find("SkillBox(1)/SkillPoints/InsertSkillPoint_p(Clone)"));
        else if (NameNumber == 2)
            Destroy(GameObject.Find("SkillBox(2)/SkillPoints/InsertSkillPoint_p(Clone)"));
    }

    private void FixUnityBug()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x - 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y - 0.01f);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x + 0.01f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + 0.01f);
    }
}
