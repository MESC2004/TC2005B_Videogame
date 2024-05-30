// Miguel Soria A01028033
// 24/05/2024
// Script to control the combat sequence of the game, including the deck, cards, and turn sequence.

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
            ""Name"": ""Heathcliff"",
            ""HP"": 20,
            ""Speed"": 1,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""Pierce Cards have 50% less effect""
            },
            {
            ""Card_ID"": 2,
            ""Type_ID"": 1,
            ""Name"": ""Faust"",
            ""HP"": 15,
            ""Speed"": 2,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""If an attack boost card is used, heal 50% of damage dealt""
            },
            {
            ""Card_ID"": 3,
            ""Type_ID"": 1,
            ""Name"": ""Don Quixote"",
            ""HP"": 10,
            ""Speed"": 3,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""If a pierce card is used, pierce the defense 50% more""
            },
            {
            ""Card_ID"": 4,
            ""Type_ID"": 1,
            ""Name"": ""Ishmael"",
            ""HP"": 20,
            ""Speed"": 1,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""Healing cards heal 25% more""
            },
            {
            ""Card_ID"": 5,
            ""Type_ID"": 1,
            ""Name"": ""Outis"",
            ""HP"": 15,
            ""Speed"": 2,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""Attack cards cost 1 more speed (Except weak punch)""
            },
            {
            ""Card_ID"": 6,
            ""Type_ID"": 1,
            ""Name"": ""Yi Sang"",
            ""HP"": 10,
            ""Speed"": 3,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""Ego weapons increase 50% more damage""
            },
            {
            ""Card_ID"": 7,
            ""Type_ID"": 2,
            ""Name"": ""Opportunistic Slash"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 3,
            ""Atk"": 7,
            ""Def"": 0,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 8,
            ""Type_ID"": 2,
            ""Name"": ""Blunt Hit"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 2,
            ""Atk"": 4,
            ""Def"": 0,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 9,
            ""Type_ID"": 2,
            ""Name"": ""Weak Punch"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 1,
            ""Atk"": 2,
            ""Def"": 0,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 10,
            ""Type_ID"": 3,
            ""Name"": ""Strong Block"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 3,
            ""Atk"": 0,
            ""Def"": 10,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 11,
            ""Type_ID"": 3,
            ""Name"": ""Shield"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 2,
            ""Atk"": 0,
            ""Def"": 5,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 12,
            ""Type_ID"": 3,
            ""Name"": ""Arm Block"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 1,
            ""Atk"": 0,
            ""Def"": 3,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 13,
            ""Type_ID"": 4,
            ""Name"": ""Ego Armor"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 3,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 14,
            ""Type_ID"": 4,
            ""Name"": ""Ego Weapon"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 0,
            ""Atk"": 2,
            ""Def"": 0,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 15,
            ""Type_ID"": 4,
            ""Name"": ""Ego Needie"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": -4,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 16,
            ""Type_ID"": 4,
            ""Name"": ""Healing Ampule"",
            ""HP"": 4,
            ""Speed"": 0,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""N/A""
            },
            {
            ""Card_ID"": 17,
            ""Type_ID"": 5,
            ""Name"": ""Ego Claw"",
            ""HP"": 0,
            ""Speed"": 0,
            ""SpeedCost"": 0,
            ""Atk"": 0,
            ""Def"": 0,
            ""Passive"": ""N/A""
            }
        ]
    }";


    // Lista de IDs de las cartas en el deck del jugador y la IA.
    [SerializeField] List<int> playerDeck = new List<int>() {1, 2, 3, 7, 8, 8, 8, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8};
    [SerializeField] List<int> enemyDeck = new List<int>() {4, 5, 6, 7, 8, 8, 8, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8};
    
    [SerializeField] Cards cardsObject;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform PlayerPanelTop;
    [SerializeField] Transform PlayerPanelBottom;
    [SerializeField] Transform EnemyPanelTop;
    [SerializeField] Transform EnemyPanelBottom;
    [SerializeField] Transform HandPanel;


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

            int deckCardID = playerDeck[0];
            int enemyCardID = enemyDeck[0];

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
            // newCard

            // Instantiate Enemy Card
            GameObject newEnemyCard = Instantiate(cardPrefab, EnemyPanelParent);
            SetData(newEnemyCard, enemyCardData);
            // Disable click on enemy card
            newEnemyCard.GetComponent<Button>().interactable = false;

            // Remove card in position i from the deck
            playerDeck.RemoveAt(0);
            enemyDeck.RemoveAt(0);
            
        } 
    } 

    

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
            cardscript.cardData = singleCardData;

        // Load card image dynamically
        Sprite cardImage = Resources.Load<Sprite>("Card_Visuals/" + singleCardData.Name); 
        if (cardImage != null)
        {
            newCard.GetComponent<Image>().sprite = cardImage;
        }
        else
        {
            Debug.LogError("Failed to load card image: " + singleCardData.Name);
        }

        // Set TMP values based on card type
        switch (singleCardData.Type_ID)
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

    public void DrawCard()
    {
        for (int i = 0; i < 3; i++)
        {
            DrawSingleCard();
        }
    }

    void DrawSingleCard() {
        // Draw a card from the deck

        // Find card data in cardsObject
        CardData singleCardData = cardsObject.cards.Find(card => card.Card_ID == playerDeck[0]);

        // Instantiate Card
        GameObject newCard = Instantiate(cardPrefab, HandPanel);
        SetData(newCard, singleCardData);

        // Remove card in position 0 from the deck
        playerDeck.RemoveAt(0);
    }

    public void TurnSequence()
    {
        // Listen for click on draw button
        // Draw 3 cards
        // Allow click on cards from hand
        // Listen for click on card
        // If card is identity card, swap bottom card with top card
        // If card is attack card, move to bottom panel
        // If card is defense card, move to bottom panel
        // If card is effect card, apply effect
        // If card is draw card, draw 2 cards
        // Apply stats and effects to the card in the top panel
        // Use stats to determine damage to opposing card
        // End Turn
        // Repeat for enemy

        DrawCard();

    }

    public void CardClicked(CardData cardData, GameObject clickedCard)
    {
        /* Listen for click on card
        If card is identity card, swap bottom card with top card
        Apply stats and effects to the card in the top panel
        If card is attack card, add to attack of the card in the top panel
        If card is defense card, add to defense of the card in the top panel
        If card is draw card, draw 2 cards */

        // Get card data from any card object, hand or table
        // Listener for card click

        // Check card type
        switch (cardData.Type_ID)
        {
            case 1:
                // Identity Card
                Debug.Log("Identity Card Clicked, Swapping");
                Swap(clickedCard);
                break;
            case 2:
                // Attack Card
                Debug.Log("Attack Card Clicked");
                AttackCardClick(clickedCard);
                break;
            case 3:
                // Defense Card
                Debug.Log("Defense Card Clicked");
                DefenseCardClick(clickedCard);
                break;
            case 4:
                // Effect card
                // Apply effect
                break;
            case 5:
                // Draw card
                // Draw 2 cards
                DrawCard();
                break;
        }
    }

    public void Swap(GameObject clickedCard)
    {
        // Swap the top card with the clicked card

        // Get top card
        GameObject topCard = PlayerPanelTop.GetChild(0).gameObject;
        // Update speed attribute and text of top card to original speed from apiCardData
        topCard.GetComponent<CardScript>().cardData.Speed = cardsObject.cards.Find(card => card.Card_ID == topCard.GetComponent<CardScript>().cardData.Card_ID).Speed;
        topCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = topCard.GetComponent<CardScript>().cardData.Speed.ToString(); 

        // Store parent transforms
        Transform topCardParent = topCard.transform.parent;
        Transform clickedCardParent = clickedCard.transform.parent;

        // Swap parents
        topCard.transform.SetParent(clickedCardParent);
        clickedCard.transform.SetParent(topCardParent);
    }

    public void AttackCardClick(GameObject clickedCard) {
        // Get attack card from hand
        // Check if speed cost is less than speed of the top card
        // Move to the middle of the bottom panel
        // Add to attack of the card in the top panel
        // Add to speed cost of the card in the top panel

        // Get top card
        GameObject topCard = PlayerPanelTop.GetChild(0).gameObject;

        // Check if speed cost is less than speed of the top card
        if (clickedCard.GetComponent<CardScript>().cardData.SpeedCost > topCard.GetComponent<CardScript>().cardData.Speed) {
            Debug.Log("Not enough speed");
            return;
        }

        // Move clicked card to the middle of the bottom panel
        clickedCard.transform.SetParent(PlayerPanelBottom);
        clickedCard.transform.SetSiblingIndex(1);

        // Make card not clickable
        clickedCard.GetComponent<Button>().interactable = false;

        // Substract speed cost to speed of the top card
        topCard.GetComponent<CardScript>().cardData.Speed -= clickedCard.GetComponent<CardScript>().cardData.SpeedCost;
        topCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = topCard.GetComponent<CardScript>().cardData.Speed.ToString();

        // Add attack to the top card
        topCard.GetComponent<CardScript>().cardData.Atk += clickedCard.GetComponent<CardScript>().cardData.Atk;
    }

    public void DefenseCardClick(GameObject clickedCard) {
        // Get defense card from hand
        // Check if speed cost is less than speed of the top card
        // Move to the middle of the bottom panel
        // Add to defense of the card in the top panel
        // Add to speed cost of the card in the top panel

        // Get top card
        GameObject topCard = PlayerPanelTop.GetChild(0).gameObject;

        // Check if speed cost is less than speed of the top card
        if (clickedCard.GetComponent<CardScript>().cardData.SpeedCost > topCard.GetComponent<CardScript>().cardData.Speed) {
            Debug.Log("Not enough speed");
            return;
        }

        // Move clicked card to the middle of the bottom panel
        clickedCard.transform.SetParent(PlayerPanelBottom);
        clickedCard.transform.SetSiblingIndex(1);

        // Make card not clickable
        clickedCard.GetComponent<Button>().interactable = false;

        // Substract speed cost to speed of the top card
        topCard.GetComponent<CardScript>().cardData.Speed -= clickedCard.GetComponent<CardScript>().cardData.SpeedCost;
        topCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = topCard.GetComponent<CardScript>().cardData.Speed.ToString();

        // Add defense to the top card
        topCard.GetComponent<CardScript>().cardData.Def += clickedCard.GetComponent<CardScript>().cardData.Def;
    }
}


