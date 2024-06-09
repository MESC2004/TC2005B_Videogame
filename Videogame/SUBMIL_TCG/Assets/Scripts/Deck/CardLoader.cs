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

            // Assign tmp values according to card Type_ID (Miguel Soria)
            switch (card.Type_ID)
            {
                case 1:
                    // Set first TMP of the object to Speed, second TMP to HP
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.Speed.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.HP.ToString();
                    
                    break;
                case 2:
                    // Set the first TMP to Speed_Cost, second TMP to Attack
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.Speed_Cost.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.Atk.ToString();
                    
                    break;
                case 3:
                    // First tmp = Speed_Cost, second TMP = Def
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.Speed_Cost.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.Def.ToString();
                    break;
                case 4:
                    // Both TMPS empty string
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    break;
                case 5:
                    // both TMPS empty string
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    break;
            } 

            Button cardButton = newCard.GetComponent<Button>();
            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(() => FindObjectOfType<CardSelection>().OnCardSelected(cardComponent));
            newCard.GetComponent<Image>().color = Color.white;
        }
    }
}
