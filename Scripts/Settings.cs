using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Toggle Notif;

    private void TurnNotifOff()
    {
        Pushe.NotificationOff();
    }

    private void TurnNotifOn()
    {
        Pushe.NotificationOn();
    }

    public void NotifController()
    {
        if (Notif.isOn == true)
            TurnNotifOn();
        else if (Notif.isOn == false)
            TurnNotifOff();
    }
}
