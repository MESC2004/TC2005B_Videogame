using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CardSelectionManager : MonoBehaviour
{
    public Transform PlayerDeckPanelContent;
    public GameObject CardButtonPrefab;
    public Button saveButton;
    public TextMeshProUGUI WarningText;  // Para mostrar advertencias
    private const int maxCards = 23;
    private const int maxType1Cards = 3;
    private List<CardData> selectedCards = new List<CardData>();
    private int type1Count = 0;

    void Start()
    {
        UpdateSaveButtonState();
        LoadSelectedCards();  // Carga las cartas seleccionadas al iniciar
    }

    public void OnCardSelected(Card card)
    {
        // if (selectedCards.Count >= maxCards || 
        //     (type1Count < 1 && selectedCards.Count >= 20) || 
        //     (type1Count < 2 && selectedCards.Count >= 21) || 
        //     (type1Count < 3 && selectedCards.Count >= 22))
        // {
        //     ShowWarning("You cannot select more cards without required type 1 cards.");
        //     return;
        // }

        if (card.cardData.Type_ID == 1 && type1Count >= maxType1Cards)
        {
            ShowWarning("Maximum number of Type 1 cards selected.");
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
        // Clear all current card buttons
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in PlayerDeckPanelContent)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            Destroy(child);
        }

        // Add selected cards as buttons
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
                    // else if ((type1Count < 1 && selectedCards.Count >= 20) || 
                    //          (type1Count < 2 && selectedCards.Count >= 21) || 
                    //          (type1Count < 3 && selectedCards.Count >= 22))
                    // {
                    //     isDisabled = true;
                    // }
                    if ((cardComponent.cardData.Type_ID == 1) && !selectedCards.Contains(cardComponent.cardData)) {
                        cardButton.interactable = !isDisabled;
                    }
                    if (selectedCards.Count >= maxCards)
                    {
                        cardButton.interactable = false;
                    }else if(selectedCards.Count < maxCards && (cardComponent.cardData.Type_ID != 1)){
                        cardButton.interactable = true;
                    }
                    if ((cardComponent.cardData.Type_ID != 1) && (selectedCards.Count - type1Count >= 20)) {
                        cardButton.interactable = false;
                    }

                    // child.GetComponent<Image>().color = isDisabled ? Color.gray : Color.white;
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

    public void ShowSaveWarning()
    {
        ShowWarning("You must have exactly 23 cards, including 3 type 1 cards.");
    }

    public void ShowWarning(string message)
    {
        if (WarningText != null)
        {
            WarningText.text = message;
            WarningText.gameObject.SetActive(true);
            Invoke("HideWarning", 2.0f);
        }
    }

    void HideWarning()
    {
        if (WarningText != null)
        {
            WarningText.gameObject.SetActive(false);
        }
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
