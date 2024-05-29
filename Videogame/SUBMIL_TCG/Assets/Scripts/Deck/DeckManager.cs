using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    private List<int> playerDeck = new List<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCardToDeck(int cardID)
    {
        if (!playerDeck.Contains(cardID))
        {
            playerDeck.Add(cardID);
            Debug.Log("Card added to deck: " + cardID);
        }
    }

    public void RemoveCardFromDeck(int cardID)
    {
        if (playerDeck.Contains(cardID))
        {
            playerDeck.Remove(cardID);
            Debug.Log("Card removed from deck: " + cardID);
        }
    }

    public List<int> GetPlayerDeck()
    {
        return new List<int>(playerDeck);
    }
}
