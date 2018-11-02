using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using FluentFTP;

public class DownloadFiles : MonoBehaviour
{

    private readonly string FTPHost = "ftp://138.201.32.126";
    private readonly string FTPUserName = "mohadair";
    private readonly string FTPPassword = "@Amir22102210";

    public byte[] downloadWithFTP(string ftpUrl, string savePath = "")
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpUrl));
        //request.Proxy = null;

        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = true;

        //If username or password is NOT null then use Credential
        if (!string.IsNullOrEmpty(FTPUserName) && !string.IsNullOrEmpty(FTPPassword))
        {
            request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
        }

        request.Method = WebRequestMethods.Ftp.DownloadFile;

        //If savePath is NOT null, we want to save the file to path
        //If path is null, we just want to return the file as array
        if (!string.IsNullOrEmpty(savePath))
        {
            downloadAndSave(request.GetResponse(), savePath);
            return null;
        }
        else
        {
            return downloadAsbyteArray(request.GetResponse());
        }
    }

    byte[] downloadAsbyteArray(WebResponse request)
    {
        using (Stream input = request.GetResponseStream())
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while (input.CanRead && (read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

    void downloadAndSave(WebResponse request, string savePath)
    {
        Stream reader = request.GetResponseStream();

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(savePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }

        FileStream fileStream = new FileStream(savePath, FileMode.Create);


        int bytesRead = 0;
        byte[] buffer = new byte[2048];

        while (true)
        {
            bytesRead = reader.Read(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                break;

            fileStream.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();
    }

    public void ListOfDirectory(string ftpUrl)
    {

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
        request.Method = WebRequestMethods.Ftp.ListDirectory;

        request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);

        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        Debug.Log(reader.ReadToEnd());

        Debug.Log($"Directory List Complete, status {response.StatusDescription}");

        reader.Close();
        response.Close();
    }
    
    public async void NewLibTestAsync()
    {
        FtpClient client = new FtpClient("138.201.32.126");
        client.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
        await client.ConnectAsync();
        Progress<double> progress = new Progress<double>(x =>
        {
            Debug.Log(x);
        });
        await client.DownloadFileAsync(@"D:\MyTest1.png", "/BaladamSkillImage/mohsen/31bc8e894cda9e30cd9f4997832a06033", true, FluentFTP.FtpVerify.Retry, progress);
        client.Disconnect();
    }
}
