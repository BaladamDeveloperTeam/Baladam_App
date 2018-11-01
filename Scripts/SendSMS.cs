using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmsIrRestful;
using security;

public class SendSMS : MonoBehaviour
{

    private string VerficationCode;

    public void sendSMSRegisterVerification(string PhoneNumber)
    {
        Coding coding = new Coding();
        Random rnd = new Random();
        int number = Random.Range(11111, 99999);
        VerficationCode = coding.Md5Sum(number.ToString());

        var token = new Token().GetToken("174e6ec07ea335dd486ec0", "mohada01");

        var restVerificationCode = new RestVerificationCode()
        {
            Code = number.ToString(),
            MobileNumber = PhoneNumber
        };

        var restVerificationCodeRespone = new VerificationCode().Send(token, restVerificationCode);

        if (restVerificationCodeRespone.IsSuccessful)
        {
            Debug.Log("Send");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void sendSMSRegisterVerification(long PhoneNumber)
    {
        Coding coding = new Coding();
        Random rnd = new Random();
        int number = Random.Range(11111, 99999);
        VerficationCode = coding.Md5Sum(number.ToString());

        var token = new Token().GetToken("174e6ec07ea335dd486ec0", "mohada01");

        var ultraFastSend = new UltraFastSend()
        {
            Mobile = PhoneNumber,
            TemplateId = 5112,
            ParameterArray = new List<UltraFastParameters>()
            {
                new UltraFastParameters()
                {
                    Parameter = "VerificationCode" , ParameterValue = number.ToString()
                }
            }.ToArray()

        };

        UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

        if (ultraFastSendRespone.IsSuccessful)
        {
            Debug.Log("Send");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public string ReadVerfi()
    {
        return VerficationCode;
    }

    void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}
}
