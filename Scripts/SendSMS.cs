using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmsIrRestful;
using security;

public class SendSMS
{

    private string VerficationCode;
    public enum SendKind { Normal, Fast};

    public void sendSMSRegisterVerification(string PhoneNumber, SendKind sendKind)
    {
        if (sendKind == SendKind.Fast)
        {
            Coding coding = new Coding();
            Random rnd = new Random();
            int number = Random.Range(11111, 99999);
            VerficationCode = number.ToString();

            var token = new Token().GetToken("ddcda1c76e994aff3e3a1c7", "45m127*2210");

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
        else if(sendKind == SendKind.Normal)
        {
            Coding coding = new Coding();
            Random rnd = new Random();
            int number = Random.Range(11111, 99999);
            VerficationCode = number.ToString();

            var token = new Token().GetToken("ddcda1c76e994aff3e3a1c7", "45m127*2210");

            var messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { "بلدم ! \n کاربر گرامی با تشکر از ثبت نام شما کد ثبت نام :" + number.ToString() }.ToArray(),
                MobileNumbers = new List<string> { PhoneNumber }.ToArray(),
                LineNumber = "30004747473203",
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

            if (messageSendResponseObject.IsSuccessful)
            {
                Debug.Log("Send");
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }

    public IEnumerator sendSMSRegisterVerification(long PhoneNumber)
    {
        UltraFastSendRespone ultraFastSendRespone;
        Coding coding = new Coding();
        Random rnd = new Random();
        int number = Random.Range(11111, 99999);
        VerficationCode = number.ToString();

        var token = new Token().GetToken("ddcda1c76e994aff3e3a1c7", "45m127*2210");

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

        yield return ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

        if (ultraFastSendRespone.IsSuccessful)
        {
            Debug.Log("Send");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public int GetCredit()
    {
        var token = new Token().GetToken("ddcda1c76e994aff3e3a1c7", "45m127*2210");

        CreditResponse credit = new Credit().GetCredit(token);
        return (int)credit.Credit;
    }

    public string ReadVerfi()
    {
        return VerficationCode;
    }
}
