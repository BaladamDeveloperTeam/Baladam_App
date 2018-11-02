using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Home_Click_Handler : MonoBehaviour
{

    public Text PlaceHolder;

	void Start ()
    {
        Screen.fullScreen = false;
	}

	void Update ()
    {
        
	}

    void FixedUpdata()
    {
        
    }

    public void Upload()
    {
        //UploadFiles up = new UploadFiles();
        //up.UploadFile(@"C:/Users/Mohammad/AppData/LocalLow/Baladam/بلدم/0689768c0bc582f99f845db3e272a159b", "test");
        //DownloadFiles DF = new DownloadFiles();
        //string path = Path.Combine(Application.persistentDataPath, "FTP Files");
        //path = Path.Combine(path, "data.png");
        //DF.downloadWithFTP("ftp://138.201.32.126/BaladamSkillImage/test/0689768c0bc582f99f845db3e272a159b.png", path);
        //DF.ListOfDirectory("ftp://138.201.32.126/BaladamSkillImage/");
        
        //DF.NewLibTestAsync();
        SendSMS sms = new SendSMS();
        Debug.Log(sms.GetCredit());
    }

}
