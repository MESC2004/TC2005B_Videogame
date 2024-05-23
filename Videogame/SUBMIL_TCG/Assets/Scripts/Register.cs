/*
 * Handles registration logic
 * Nicole Dñavila
 * 22-05-2024
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button registerButton;
    public Button goToLoginButton;

    private ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(writeStuffToFile);
        //goToLoginButton.onClick.AddListener(goToLoginScene);

        string filePath = Application.dataPath + "/credentials.txt";
        if (File.Exists(filePath))
        {
            credentials = new ArrayList(File.ReadAllLines(filePath));
        }
        else
        {
            File.WriteAllText(filePath, "");
            credentials = new ArrayList();
        }
    }

    void goToLoginScene()
    {
        SceneChanger.GoTo("Title");
    }

    void writeStuffToFile()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Split(':')[0] == usernameInput.text)
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text);
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (string[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
        }
    }
}
