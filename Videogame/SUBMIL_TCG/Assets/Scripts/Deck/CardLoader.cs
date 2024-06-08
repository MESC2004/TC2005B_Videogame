// Fernando Fuentes
// 29/05/2024
// Script that handles the stat assignation as well as image assignation to the cards
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardLoader : MonoBehaviour
{
    public GameObject CardButtonPrefab;
    public Transform content;
    public Cards cardsObject;
    public Sprite defaultCardImage;

    public string apiCardData = @"";

    private bool isDataLoaded = false;

    // Start is called before the first frame update
    void Start()
{
    APIConnectionDeck apiConnectionDeck = GetComponent<APIConnectionDeck>();
    if (apiConnectionDeck != null)
    {
        apiConnectionDeck.GetData(() =>
        {
            // This code will run after the data is fetched
            if (!string.IsNullOrEmpty(apiConnectionDeck.apiCardData))
            {
                cardsObject = JsonUtility.FromJson<Cards>(apiConnectionDeck.apiCardData);
                if (cardsObject != null && cardsObject.cards != null)
                {
                    LoadCards(); // Now LoadCards is called here
                }
                else
                {
                    Debug.LogError("Failed to load cards data.");
                }
            }
            else
            {
                Debug.LogError("apiCardData is null or empty.");
            }
        });
    }
    else
    {
        Debug.LogError("APIConnectionDeck component not found.");
    }
}


    // Update is called once per frame
    void Update()
    {
        if (isDataLoaded)
        {
            LoadCards();
            isDataLoaded = false; // Prevents LoadCards from being called again
        }
    }

    private void LoadCards()
    {
        foreach (CardData card in cardsObject.cards)
        {
            GameObject newCard = Instantiate(CardButtonPrefab, content);
            Card cardComponent = newCard.GetComponent<Card>();
            cardComponent.cardData = card;
            cardComponent.cardButton = newCard.GetComponent<Button>();

            Sprite cardSprite = Resources.Load<Sprite>($"images/{card.Card_ID}");
            if (cardSprite != null)
            {
                cardComponent.SetCardImage(cardSprite);
            }
            else
            {
                Debug.LogError($"Card image not found for Card_ID: {card.Card_ID}");
                cardComponent.SetCardImage(defaultCardImage);
            }

            TextMeshProUGUI[] textComponents = newCard.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var textComponent in textComponents)
            {
                if (textComponent.name == "HP")
                {
                    textComponent.text = card.HP > 0 ? card.HP.ToString() : "";
                }
                else if (textComponent.name == "Speed")
                {
                    textComponent.text = card.Speed > 0 ? card.Speed.ToString() : "";
                }
                else if (textComponent.name == "Card_ID")
                {
                    textComponent.text = card.Card_ID > 0 ? card.Card_ID.ToString() : "";
                }
                else
                {
                    textComponent.text = "";
                }
            }

            Button cardButton = newCard.GetComponent<Button>();
            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(() => FindObjectOfType<CardSelection>().OnCardSelected(cardComponent));
            newCard.GetComponent<Image>().color = Color.white;
        }
    }
}
