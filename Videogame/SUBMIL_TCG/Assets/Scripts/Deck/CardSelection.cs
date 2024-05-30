using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CardSelection : MonoBehaviour
{
    public Transform PlayerDeckPanelContent;
    public GameObject CardButtonPrefab;
    public Button saveButton;
    private const int maxCards = 23;
    private const int maxType1Cards = 3;
    private List<CardData> selectedCards = new List<CardData>();
    private int type1Count = 0;

    void Start()
    {
        UpdateSaveButtonState();
        LoadSelectedCards();
    }

    public void OnCardSelected(Card card)
    {
        if (card.cardData.Type_ID == 1 && type1Count >= maxType1Cards)
        {
            return;
        }

        if (card.cardData.Type_ID == 1)
        {
            card.cardButton.interactable = false;
        }

        selectedCards.Add(card.cardData);
        if (card.cardData.Type_ID == 1) type1Count++;
        UpdatePlayerDeck();
        UpdateCardButtonsState();
        UpdateSaveButtonState();
    }

    public void OnCardDeselected(CardData card)
    {
        selectedCards.Remove(card);
        if (card.Type_ID == 1) type1Count--;
        UpdatePlayerDeck();
        UpdateCardButtonsState();
        UpdateSaveButtonState();
    }

    void UpdatePlayerDeck()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in PlayerDeckPanelContent)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            Destroy(child);
        }

        foreach (CardData card in selectedCards)
        {
            GameObject newCard = Instantiate(CardButtonPrefab, PlayerDeckPanelContent);
            Card cardComponent = newCard.GetComponent<Card>();
            cardComponent.cardData = card;
            cardComponent.cardButton = newCard.GetComponent<Button>();
            newCard.GetComponentInChildren<TextMeshProUGUI>().text = card.Name + " " + card.Card_ID;
            Button cardButton = newCard.GetComponent<Button>();
            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(() => OnCardDeselected(card));
        }
    }

    void UpdateCardButtonsState()
    {
        CardLoader cardLoader = FindObjectOfType<CardLoader>();
        foreach (Transform child in cardLoader.content)
        {
            Card cardComponent = child.GetComponent<Card>();
            if (cardComponent != null)
            {
                Button cardButton = cardComponent.cardButton;
                if (cardButton != null)
                {
                    bool isDisabled = false;
                    if (cardComponent.cardData.Type_ID == 1 && type1Count >= maxType1Cards)
                    {
                        isDisabled = true;
                    }
                    if ((cardComponent.cardData.Type_ID == 1) && !selectedCards.Contains(cardComponent.cardData))
                    {
                        cardButton.interactable = !isDisabled;
                    }
                    if (selectedCards.Count >= maxCards)
                    {
                        cardButton.interactable = false;
                    }
                    else if (selectedCards.Count < maxCards && (cardComponent.cardData.Type_ID != 1))
                    {
                        cardButton.interactable = true;
                    }
                    if ((cardComponent.cardData.Type_ID != 1) && (selectedCards.Count - type1Count >= 20))
                    {
                        cardButton.interactable = false;
                    }
                }
            }
        }
    }

    void UpdateSaveButtonState()
    {
        if (saveButton != null)
        {
            bool canSave = selectedCards.Count == maxCards && type1Count == maxType1Cards;
            saveButton.interactable = canSave;
        }
    }

    public bool CanSaveDeck()
    {
        return selectedCards.Count == maxCards && type1Count == maxType1Cards;
    }

    public List<CardData> GetSelectedCards()
    {
        return selectedCards;
    }

    void LoadSelectedCards()
    {
        string json = PlayerPrefs.GetString("SelectedCards", "");
        if (!string.IsNullOrEmpty(json))
        {
            CardListWrapper wrapper = JsonUtility.FromJson<CardListWrapper>(json);
            if (wrapper != null && wrapper.cards != null)
            {
                selectedCards = wrapper.cards;
                type1Count = selectedCards.Count(card => card.Type_ID == 1);
                UpdatePlayerDeck();
                UpdateCardButtonsState();
                UpdateSaveButtonState();
            }
        }
    }
}
