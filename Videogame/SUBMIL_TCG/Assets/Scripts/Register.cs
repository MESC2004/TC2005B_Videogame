using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
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
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(registerEndpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Handle successful registration (e.g., show success message, redirect to login)
                Debug.Log("User registered successfully!");
                SceneChanger.GoTo(loginSceneName);
            }
            else
            {
                Debug.LogError("Registration failed: " + www.error);
            }
        }
    }
}
