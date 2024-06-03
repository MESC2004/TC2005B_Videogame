// Fernando Fuentes
// 29/05/2024
// Script that handles saving the deck of cards

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CanSaveDeck : MonoBehaviour
{
    public Button saveButton;

    void Start()
    {
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveSelection);
        }
        else
        {
            Debug.LogError("Save unasigned");
        }
    }

    void SaveSelection()
    {
        CardSelection cardSelection = FindObjectOfType<CardSelection>();
        if (cardSelection != null)
        {
            if (cardSelection.CanSaveDeck())
            {
                List<CardData> selectedCards = cardSelection.GetSelectedCards();
            
                List<CardData> sortedCards = selectedCards.OrderByDescending(card => card.Type_ID == 1).ToList();
            
                PlayerPrefs.SetString("SelectedCards", JsonUtility.ToJson(new CardListWrapper(sortedCards)));
                GameObject objectToDestroy = GameObject.Find("DeckManager");
                Destroy(objectToDestroy);
                SceneManager.LoadScene("Title");
            }
        }
    }
}
