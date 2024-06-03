// Fernando Fuentes
// 15/05/2024
// Script that handles the scene changing through the project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public static void GoTo(string sceneName)
    {
        if(sceneName == "Title"){
            GameObject objectToDestroy = GameObject.Find("DeckManager");
            Destroy(objectToDestroy);
        }
        // Load the scene with the given name
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
