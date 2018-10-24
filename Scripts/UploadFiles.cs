using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

public class UploadFiles
{
    private readonly string FTPHost = "";
    private readonly string FTPUserName = "";
    private readonly string FTPPassword = "";

    public void UploadFile(string FilePath)
    {
        try
        {
            Debug.Log("Path: " + FilePath);

            WebClient client = new WebClient();
            Uri uri = new Uri(FTPHost + "/" + new FileInfo(FilePath).Name);

            client.UploadProgressChanged += new UploadProgressChangedEventHandler(OnFileUploadProgressChanged);
            client.UploadFileCompleted += new UploadFileCompletedEventHandler(OnFileUploadCompleted);
            client.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            client.UploadFileAsync(uri, "STOR", FilePath);
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
}
