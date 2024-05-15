using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    public static void GoTo(string sceneName)
    {
        //load the scene with the given name
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
