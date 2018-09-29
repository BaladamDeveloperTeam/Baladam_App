using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;
using UPersian.Utils;

public class Search_panel_Handler : MonoBehaviour
{

    public RtlText PlaceHolder;

	void Start ()
    {
        //StartCoroutine(Type());
	}
	

	void Update ()
    {
		
	}

    IEnumerator Type()
    {
        PlaceHolder.text = "";
        PlaceHolder.text += "ب";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ر";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ن";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ا";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "م";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ه";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += " ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ی";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﺴ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﯾ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﻮ";
        yield return new WaitForSeconds(0.2f);
        PlaceHolder.text += "ﻧ";




    }
}
