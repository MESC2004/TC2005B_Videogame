using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public string loginEndpoint = "http://localhost:5000/api/login"; // Update if hosted elsewhere
    public string nextSceneName;

    public void OnLoginButtonPressed()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;
        StartCoroutine(AttemptLogin(enteredUsername, enteredPassword));
    }

    IEnumerator AttemptLogin(string username, string password)
    {
        UserLogin user = new UserLogin
        {
            username = username,
            password = password
        };

        string json = JsonUtility.ToJson(user);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(loginEndpoint, "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Handle successful login (e.g., save user data, load next scene)
            SceneChanger.GoTo(nextSceneName);
        }
        else
        {
            Debug.LogError("Login failed: " + www.error);
        }
    }

    [System.Serializable]
    public class UserLogin
    {
        public string username;
        public string password;
    }
}
