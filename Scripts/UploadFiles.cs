using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UploadFiles
{
    private readonly string FTPHost = "ftp://138.201.32.126";
    private readonly string FTPUserName = "mohadair";
    private readonly string FTPPassword = "@Amir22102210";
    private readonly string SaveTo = "/BaladamSkillImage/";

    public void UploadFile(string FilePath, string UserName)
    {
        try
        {
            Debug.Log("Path: " + FilePath);

            try
            {
                WebRequest request = WebRequest.Create(FTPHost + "/BaladamSkillImage" + "/" + UserName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Debug.Log(resp.StatusCode);
                }

                WebClient client = new WebClient();
                Uri uri = new Uri(FTPHost + SaveTo + "/" + UserName + "/" + new FileInfo(FilePath).Name);
                //Uri uri = new Uri(FTPHost + "/" + new FileInfo(FilePath).Name);

                client.UploadProgressChanged += new UploadProgressChangedEventHandler(OnFileUploadProgressChanged);
                client.UploadFileCompleted += new UploadFileCompletedEventHandler(OnFileUploadCompleted);
                client.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                client.UploadFileAsync(uri, "STOR", FilePath);
            }
            catch
            {
                WebClient client = new WebClient();
                Uri uri = new Uri(FTPHost + SaveTo + "/" + UserName + "/" + new FileInfo(FilePath).Name);
                //Uri uri = new Uri(FTPHost + "/" + new FileInfo(FilePath).Name);

                client.UploadProgressChanged += new UploadProgressChangedEventHandler(OnFileUploadProgressChanged);
                client.UploadFileCompleted += new UploadFileCompletedEventHandler(OnFileUploadCompleted);
                client.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                client.UploadFileAsync(uri, "STOR", FilePath);
            }
        }
        catch
        {
            Debug.Log("Upload Error!!!");
        }
        
    }

    void OnFileUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
    {
        Debug.Log("Uploading Progreess: " + e.ProgressPercentage);
    }

    void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
    {
        Debug.Log("File Uploaded");
    }

    public async Task Upload(string ftpAddress, string filePath, string Username, string Password)
    {
        try
        {
            FtpWebRequest re = (FtpWebRequest)FtpWebRequest.Create(ftpAddress + "/UsersInfo/" + "User_" + "Baladam");
            re.Method = WebRequestMethods.Ftp.UploadFile;
            re.Credentials = new NetworkCredential(Username, Password);
            re.UsePassive = true;
            re.UseBinary = true;
            re.KeepAlive = false;

            FileStream stream = File.OpenRead(filePath);
            byte[] buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            stream.Close();

            Stream reqstream = re.GetRequestStream();
            await reqstream.WriteAsync(buffer, 0, buffer.Length);
            reqstream.Close();
            Debug.Log("Uploaded");
        }
        catch
        {
            Debug.Log("Upload Error!!!");
        }
    }
}
