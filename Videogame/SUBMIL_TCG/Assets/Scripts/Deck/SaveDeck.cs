using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

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
            Debug.LogError("Save button is not assigned.");
        }
    }

    void SaveSelection()
    {
        CardSelectionManager cardSelectionManager = FindObjectOfType<CardSelectionManager>();
        if (cardSelectionManager != null)
        {
            if (cardSelectionManager.CanSaveDeck())
            {
                List<CardData> selectedCards = cardSelectionManager.GetSelectedCards();
                PlayerPrefs.SetString("SelectedCards", JsonUtility.ToJson(new CardListWrapper(selectedCards)));
                SceneManager.LoadScene("Title");
            }
            else
            {
                cardSelectionManager.ShowSaveWarning();
            }
        }
    }
}
