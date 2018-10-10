using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;
using System.Linq;

public class EditProfileManager : MonoBehaviour
{
    public InputField Email, PhoneNumber, Bio, Gigs;
    public RtlText BioText, GigsText;
    public Color Pass, Faild;
    private GameObject GSM;

    void Awake()
    {
        GSM = GameObject.Find("Global script Manager");
    }

    public void CheckEmail()
    {
        if(Email.text.Contains('@') &&(Email.text.Contains(".com") || Email.text.Contains(".ir") || Email.text.Contains(".co")))
        {
            Email.image.color = Pass;
        }
        else
        {
            Email.image.color = Faild;
        }
    }

    public void CheckPhoneNumber()
    {
        if(PhoneNumber.text.Length == 11)
        {
            PhoneNumber.image.color = Pass;
        }
        else
        {
            PhoneNumber.image.color = Faild;
        }
    }

    public void CheckBio()
    {
        BioText.text = (150 - Bio.text.Length).ToString();
    }

    public void CheckGigs()
    {
        GigsText.text = (75 - Gigs.text.Length).ToString();
    }
}
