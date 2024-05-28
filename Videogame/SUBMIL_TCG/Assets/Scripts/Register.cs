using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public string registerEndpoint = "http://localhost:5000/api/register"; // Update if hosted elsewhere
    public string loginSceneName;

    public void OnRegisterButtonPressed()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;
        StartCoroutine(AttemptRegister(enteredUsername, enteredPassword));
    }

    IEnumerator AttemptRegister(string username, string password)
    {
        UserRegister user = new UserRegister
        {
            username = username,
            password = password
        };

        string json = JsonUtility.ToJson(user);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(registerEndpoint, "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Handle successful registration (e.g., save user data, load login scene)
            SceneChanger.GoTo(loginSceneName);
        }
        else
        {
            Debug.LogError("Registration failed: " + www.error);
        }
    }

    [System.Serializable]
    public class UserRegister
    {
        public string username;
        public string password;
    }
}
