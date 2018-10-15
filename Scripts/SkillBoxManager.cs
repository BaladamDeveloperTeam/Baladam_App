using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class SkillBoxManager : MonoBehaviour
{

    private GameObject[] SkillBoxs;
    public RtlText SkillNumber;
    public int SkillPointNumber = 1;

    private void Awake()
    {
        SkillBoxs = GameObject.FindGameObjectsWithTag("SkillBox");
        SkillNumber.text = "بسته شماره ی " + SkillBoxs.Length;
    }

    void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}

    public void AddToSkillPointNumber()
    {
        SkillPointNumber++;
    }

    public void RemoveToSkillPointNumber()
    {
        if(SkillPointNumber > 1)
        SkillPointNumber--;
    }
}
