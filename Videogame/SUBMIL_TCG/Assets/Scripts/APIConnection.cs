using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIConnection : MonoBehaviour
{
    private string baseURL = "http://localhost:5000/api";

    public void Login(string username, string password)
    {
        UserLogin user = new UserLogin
        {
            username = username,
            password = password
        };

        StartCoroutine(PostRequest($"{baseURL}/login", user));
    }

    private IEnumerator PostRequest(string uri, UserLogin user)
    {
        string json = JsonUtility.ToJson(user);
        UnityWebRequest webRequest = new UnityWebRequest(uri, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.DataProcessingError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login failed: " + webRequest.error);
        }
        else
        {
            Debug.Log("Login complete: " + webRequest.downloadHandler.text);
        }
    }

    [System.Serializable]
    public class UserLogin
    {
        public string username;
        public string password;
    }
}
