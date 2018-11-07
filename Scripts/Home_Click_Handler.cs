using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Home_Click_Handler : MonoBehaviour
{

    public Text PlaceHolder;
    public ParamList c;
    private GetCat GetCat;

	void Start ()
    {
        Screen.fullScreen = false;
        GetCat = this.gameObject.GetComponent<GetCat>();
        GetCat.GetCatBut();
	}

	void Update ()
    {
        
	}

    void FixedUpdata()
    {
        
    }

    public void Upload()
    {
        UploadFiles up = new UploadFiles();
        up.UploadFile(@"E:\TODO.txt", "/test");
        //DownloadFiles DF = new DownloadFiles();
        //string path = Path.Combine(Application.persistentDataPath, "FTP Files");
        //path = Path.Combine(path, "data.png");
        //DF.downloadWithFTP("ftp://138.201.32.126/BaladamSkillImage/test/0689768c0bc582f99f845db3e272a159b.png", path);
        //DF.ListOfDirectory("ftp://138.201.32.126/BaladamSkillImage/");
        
        //DF.NewLibTestAsync();

        c.Params.Add(new Param() { Key = "name", Value = "asdfasfd"});
        c.Params.Add(new Param() { Key = "346345", Value = "1234"});
        c.Params.Add(new Param() { Key = "nasdgfgsme", Value = "wetrewt"});
        c.Params.Add(new Param() { Key = "ghdfhdfghfd", Value = "543453hgjkgugu"});
        var a =  JsonUtility.ToJson(c);
        
    }

}
