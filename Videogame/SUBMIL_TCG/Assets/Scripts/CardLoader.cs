using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class CardStats
{
    public int HP;
    public int Speed;
    public int Speed_Cost;
    public int Atk;
    public int Def;
    public string Passive;
}

[System.Serializable]
public class Card
{
    public int Card_ID;
    public int Type_ID;
    public string Name;
    public string Description;
    public string Image_Path;
    public CardStats Stats;
}

[System.Serializable]
public class CardResponse
{
    public List<Card> cards;
}

public class CardLoader : MonoBehaviour
{
    public string apiUrl = "http://localhost:5000/api/cards"; // Update this to match your API URL
    public Transform cardParent; // The parent transform where card UI elements will be instantiated
    public GameObject cardPrefab; // The prefab for a card UI element

    // Add a reference to the CombatController script
    public CombatController combatController;

    void Start()
    {
        StartCoroutine(FetchCards());
    }

    IEnumerator FetchCards()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;

                // Pass the json data to CombatController
                combatController.apiCardData = json;

                CardResponse cardResponse = JsonUtility.FromJson<CardResponse>(json);
                DisplayCards(cardResponse.cards);
            }
        }
    }

    void DisplayCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardParent);
            cardGO.transform.Find("CardName").GetComponent<Text>().text = card.Name;
            cardGO.transform.Find("CardDescription").GetComponent<Text>().text = card.Description;

            StartCoroutine(LoadImage(card.Image_Path, cardGO.transform.Find("CardImage").GetComponent<Image>()));

            // Set other card stats as needed
        }
    }

    IEnumerator LoadImage(string url, Image imageComponent)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}
