using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile_Click_Handler : MonoBehaviour
{

    public GameObject Drawer, BlackPanel;
    public Animator DrawerAnim, BlackPanelAnim;

	void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}

    public void OpenDrawerClick()
    {
        click(0);
        //Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void CloseDrawerClick()
    {
        click(1);
        Screen.fullScreen = false;
    }

    void click(int Item)
    {
        switch(Item)
        {
            case 0:   //OpenDrawe
                if (Drawer.gameObject.active == true)
                    DrawerAnim.SetTrigger("Start");
                if (Drawer.gameObject.active == false)
                Drawer.gameObject.SetActive(true);
                Debug.Log("Open");
                //BlackPanelAnim.SetTrigger("Start");
                //BlackPanel.gameObject.SetActive(true);
                break;
            case 1:
                //BlackPanelAnim.SetTrigger("End");
                DrawerAnim.SetTrigger("End");
                break;
        }
    }
}
