/*
 * Creates the connection between Unity and the API, which fetches Deck information from a MySQL script.
 * Nicole Dávila
 * 03-06-2024
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIConnectionDeck : MonoBehaviour
{
    public string apiCardData;
    [SerializeField] string url;
    [SerializeField] string getEndpoint;

    CombatController controller;
    CardLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        loader = GetComponent<CardLoader>();
    }

    public void GetData(Action onCompleted)
{
    StartCoroutine(RequestGet(url + getEndpoint, onCompleted));
}

IEnumerator RequestGet(string url, Action onCompleted)
{
    using (UnityWebRequest www = UnityWebRequest.Get(url))
    {
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Request failed: " + www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            Debug.Log("The response was: " + result);
            apiCardData = result; // Assign the result to the public field
            onCompleted?.Invoke(); // Invoke the callback only after assignment
        }
    }
}

}
