using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatController : MonoBehaviour
{

    public string identityCardData = @"{
        ""cards"": 
        [
            {
            ""Card_ID"": 1,
            ""Type_ID"": 1,
            ""Name"": ""Goblin"",
            ""HP"": 10,
            ""Speed"": 5,
            ""SpeedCost"": 2,
            ""Atk"": 3,
            ""Def"": 1,
            ""Passive"": ""None""
            },
            {
            ""Card_ID"": 2,
            ""Type_ID"": 1,
            ""Name"": ""Orc"",
            ""HP"": 15,
            ""Speed"": 3,
            ""SpeedCost"": 3,
            ""Atk"": 5,
            ""Def"": 2,
            ""Passive"": ""None""
            },
            {
            ""Card_ID"": 3,
            ""Type_ID"": 1,
            ""Name"": ""Orc"",
            ""HP"": 15,
            ""Speed"": 3,
            ""SpeedCost"": 3,
            ""Atk"": 5,
            ""Def"": 2,
            ""Passive"": ""None""
            },
            {
            ""Card_ID"": 4,
            ""Type_ID"": 1,
            ""Name"": ""Orc"",
            ""HP"": 15,
            ""Speed"": 3,
            ""SpeedCost"": 3,
            ""Atk"": 5,
            ""Def"": 2,
            ""Passive"": ""None""
            },
            {
            ""Card_ID"": 5,
            ""Type_ID"": 1,
            ""Name"": ""Orc"",
            ""HP"": 15,
            ""Speed"": 3,
            ""SpeedCost"": 3,
            ""Atk"": 5,
            ""Def"": 2,
            ""Passive"": ""None""
            },
            {
            ""Card_ID"": 6,
            ""Type_ID"": 1,
            ""Name"": ""Orc"",
            ""HP"": 15,
            ""Speed"": 3,
            ""SpeedCost"": 3,
            ""Atk"": 5,
            ""Def"": 2,
            ""Passive"": ""None""
            }
        ]
    }";

    // Lista de IDs de las cartas en el deck del jugador.
    [SerializeField] public List<int> playerDeck = new List<int>() {1, 2};
    [SerializeField] bool inCombat;
    [SerializeField] Cards cardsObject;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPanel;
    // Start is called before the first frame update


    void prepareCards()
    {
        // Json data into object
        cardsObject = JsonUtility.FromJson<Cards>(identityCardData);

        // Loop through cards in cardsObject
        for (int i = 0; i < playerDeck.Count; i++) {

            int deckCardID = playerDeck[i];

            // Find card data in cardsObject
            CardData singleCardData = cardsObject.cards.Find(card => card.Card_ID == deckCardID);

            // Instantiate Card
            GameObject newCard = Instantiate(cardPrefab, cardPanel);
            
            // Set card values
            CardScript cardscript = newCard.GetComponent<CardScript>();
            cardscript.cardData = new CardData();
            cardscript.cardData.Card_ID = singleCardData.Card_ID;
            cardscript.cardData.Type_ID = singleCardData.Type_ID;
            cardscript.cardData.Name = singleCardData.Name;
            cardscript.cardData.HP = singleCardData.HP;
            cardscript.cardData.Speed = singleCardData.Speed;
            cardscript.cardData.SpeedCost = singleCardData.SpeedCost;
            cardscript.cardData.Atk = singleCardData.Atk;
            cardscript.cardData.Def = singleCardData.Def;
            cardscript.cardData.Passive = singleCardData.Passive;

            
        } 
    } 
    // Start is called before the first frame update
    void Start()
    {
        prepareCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
