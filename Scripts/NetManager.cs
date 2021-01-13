using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class NetManager
{
    // URLは差し替え
    static string rootURL = "https://hogehoge";

    public static async UniTask<string> Connect(string apiName,Hashtable post)
    {
        string postString = "";

        int paramCount = 0;
        if (post != null)
        {
            paramCount = post.Keys.Count;

            if (paramCount > 0)
            {
                postString += "?";
            }

            int index = 0;
            foreach (string keyName in post.Keys)
            {
                postString += keyName + "=" + post[keyName];
                index++;
                if (index < paramCount)
                {
                    postString += "&";
                }
            }
        }

        string requestURL = rootURL + apiName + postString;
        UnityWebRequest request = UnityWebRequest.Get(requestURL);

        request.certificateHandler = new AcceptAllCertificates();

        await request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            return request.downloadHandler.text;
        }

        return "";

    }
    
}

class AcceptAllCertificates : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
} 
