using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatController : MonoBehaviour
{

    string apiCardData = @"{
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

    // Lista de IDs de las cartas en el deck del jugador y la IA.
    [SerializeField] List<int> playerDeck = new List<int>() {1, 2, 3, 4};
    [SerializeField] List<int> enemyDeck = new List<int>() {4, 5, 6};
    [SerializeField] bool inCombat;
    [SerializeField] Cards cardsObject;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform PlayerPanelTop;
    [SerializeField] Transform PlayerPanelBottom;
    [SerializeField] Transform EnemyPanelTop;
    [SerializeField] Transform EnemyPanelBottom;


    void prepareIdentityCards()
    {
        // Instatiates first 3 cards of the player's deck on the appropriate panels

        // Local variables for parent reference
        Transform PlayerPanelParent;
        Transform EnemyPanelParent;

        // Json data into object
        cardsObject = JsonUtility.FromJson<Cards>(apiCardData);

        // Loop through cards in the deck (probably first 3 (identity cards))
        for (int i = 0; i < 3; i++) {

            int deckCardID = playerDeck[i];
            int enemyCardID = enemyDeck[i];

            // Find card data in cardsObject
            CardData singleCardData = cardsObject.cards.Find(card => card.Card_ID == deckCardID);
            CardData enemyCardData = cardsObject.cards.Find(card => card.Card_ID == enemyCardID);

            if (i == 0)
            {
                PlayerPanelParent = PlayerPanelTop;
                EnemyPanelParent = EnemyPanelTop;
            }
            else
            {
                PlayerPanelParent = PlayerPanelBottom;
                EnemyPanelParent = EnemyPanelBottom;
            }
            // Instantiate Card
            GameObject newCard = Instantiate(cardPrefab, PlayerPanelParent);
            SetData(newCard, singleCardData);

            // Instantiate Enemy Card
            GameObject newEnemyCard = Instantiate(cardPrefab, EnemyPanelParent);
            SetData(newEnemyCard, enemyCardData);
            
        } 
    } 
    // Start is called before the first frame update
    void Start()
    {
        prepareIdentityCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(GameObject newCard, CardData singleCardData) {
        // Set card values given a newly instantiated card and the data of a single card

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

            switch (cardscript.cardData.Type_ID)
            {
                // Sets the appropriate TMP values according to the card type
                case 1:
                    // Identity Card
                    // Set Top TMP to Speed and bottom TMP to health
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Speed.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.HP.ToString();
                    break;
                case 2:
                    // Attack Card
                    // Set top value to SpeedCost and bootom value to Atk
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.SpeedCost.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Atk.ToString();
                    break;
                case 3:
                    // Defense Card
                    // Set top value to SpeedCost and bootom value to Def
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.SpeedCost.ToString();
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Def.ToString();
                    break;
                case 4:
                    // Effect card
                    // Leave values empty
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    break;
                case 5:
                    // Draw card
                    //leave values empty
                    newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    break;
            }
    }
}
