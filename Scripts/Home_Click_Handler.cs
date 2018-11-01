using System.Collections;
using System.Collections.Generic;
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
        UploadFiles up = new UploadFiles();
        up.UploadFile(@"C:/Users/Mohammad/AppData/LocalLow/Baladam/بلدم/0689768c0bc582f99f845db3e272a159b", "test");
    }

}
