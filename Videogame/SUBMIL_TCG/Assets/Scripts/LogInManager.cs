// Nicole Davila
// 23/05/2024
// Script that handles the login process and storage
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
    static public bool loggedIn = false;

    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject creditsPanel;

    // Start
    void Start()
    {
        // if logged in, turn off CredentialsPanel and turn on TitlePanel
        if (loggedIn)
        {
            GameObject.Find("CredentialsPanel").SetActive(false);
            titlePanel.SetActive(true);
        }
    }

    public void OnLoginButtonPressed()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;
        StartCoroutine(AttemptLogin(enteredUsername, enteredPassword));
    }

    public void returnToTitle()
    {
        // Turn off CreditsPanel
        creditsPanel.SetActive(false);
        // Turn on TitlePanel
        titlePanel.SetActive(true);
    }

    public void OnCreditsButtonPressed()
    {
        // Turn off TitlePanel
        titlePanel.SetActive(false);
        // Turn on CreditsPanel
        creditsPanel.SetActive(true);
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
            // Turn off CredentialsPanel, Turn on TitlePanel
            Debug.Log("Login successful");
            // Turn off CredentialsPanel
            GameObject.Find("CredentialsPanel").SetActive(false);
            // Turn on TitlePanel
            titlePanel.SetActive(true);
            loggedIn = true;
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