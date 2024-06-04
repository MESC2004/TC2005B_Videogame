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
    public Cards cardsObject = new Cards();
    public Sprite defaultCardImage;

    public string apiCardData; 
    void Start()
    {
        cardsObject = JsonUtility.FromJson<Cards>(apiCardData);

        if (cardsObject == null || cardsObject.cards == null)
        {
            Debug.LogError("Failed to load cards data.");
            return;
        }

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
                    if (card.Card_ID == 7 || card.Card_ID == 8 || card.Card_ID == 9)
                    {
                        textComponent.text = card.Atk > 0 ? card.Atk.ToString() : "";
                    }
                    else if (card.Card_ID == 10 || card.Card_ID == 11 || card.Card_ID == 12)
                    {
                        textComponent.text = card.Def > 0 ? card.Def.ToString() : "";
                    }
                    else
                    {
                        textComponent.text = card.HP > 0 ? card.HP.ToString() : "";
                    }
                }
                else if (textComponent.name == "Speed")
                {
                    if (card.Card_ID == 7 || card.Card_ID == 8 || card.Card_ID == 9 || card.Card_ID == 10 || card.Card_ID == 11 || card.Card_ID == 12)
                    {
                        textComponent.text = card.SpeedCost > 0 ? card.SpeedCost.ToString() : "";
                    }
                    else
                    {
                        textComponent.text = card.Speed > 0 ? card.Speed.ToString() : "";
                    }
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
