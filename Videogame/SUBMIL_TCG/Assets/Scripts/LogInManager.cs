// Nicole Davila
// 23/05/2024
// Script that handles the login process and storage
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public string nextSceneName; // The name of the scene to load after successful login

    private ArrayList credentials;

    void Start()
    {
        string filePath = Application.dataPath + "/credentials.txt";
        if (File.Exists(filePath))
        {
            credentials = new ArrayList(File.ReadAllLines(filePath));
        }
        else
        {
            Debug.LogError("Credentials file not found.");
            credentials = new ArrayList();
        }
    }

    public void OnLoginButtonPressed()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;

        if (IsLoginValid(enteredUsername, enteredPassword))
        {
            SceneChanger.GoTo(nextSceneName);
        }
        else
        {
            Debug.Log("Invalid login credentials.");
        }
    }

    private bool IsLoginValid(string username, string password)
    {
        foreach (var i in credentials)
        {
            var parts = i.ToString().Split(':');
            if (parts[0] == username && parts[1] == password)
            {
                return true;
            }
        }
        return false;
    }
}
