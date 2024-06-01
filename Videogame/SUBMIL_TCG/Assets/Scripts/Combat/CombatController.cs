// Miguel Soria A01028033
// 24/05/2024
// Script to control the combat sequence of the game, including the deck, cards, and turn sequence.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
            ""Name"": ""Ego Needle"",
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


    [SerializeField] List<int> playerDeck = new List<int>(); /*{1, 2, 3, 7, 8, 8, 8, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8}*/

    public List<int> playerDiscard = new List<int>();
    [SerializeField] List<int> enemyDeck = new List<int>() {4, 5, 6, 7, 8, 8, 8, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7, 7, 7};

    public List<int> enemyDiscard = new List<int>();
    
    [SerializeField] Cards cardsObject;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform PlayerPanelTop;
    [SerializeField] Transform PlayerPanelBottom;
    [SerializeField] Transform EnemyPanelTop;
    [SerializeField] Transform EnemyPanelBottom;
    [SerializeField] Transform HandPanel;
    bool playerTurn;

    public string phase;

    public GameObject LosePanel;
    public GameObject WonPanel;

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

        LosePanel.SetActive(false);
        WonPanel.SetActive(false);
        
        LoadPlayerDeck();
        prepareIdentityCards();
        TurnSequence("Swap");
        // Randomize the rest of the playerDeck list
        playerDeck = playerDeck.OrderBy(x => Random.value).ToList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadPlayerDeck()
    {
        string json = PlayerPrefs.GetString("SelectedCards", "");
        if (!string.IsNullOrEmpty(json))
        {
            CardListWrapper wrapper = JsonUtility.FromJson<CardListWrapper>(json);
            if (wrapper != null && wrapper.cards != null)
            {
                playerDeck = wrapper.cards.Select(card => card.Card_ID).ToList();
            }
        }
    }

    public void SetData(GameObject newCard, CardData singleCardData)
    {
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

        // Set name (temporal)
        // newCard.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Name;

        // Load and set the card image sprite
        Sprite cardSprite = Resources.Load<Sprite>($"images/{cardscript.cardData.Card_ID}");
        if (cardSprite != null)
        {
            newCard.GetComponent<Image>().sprite = cardSprite;
        }
        else
        {
            Debug.LogError($"Card image not found for Card_ID: {cardscript.cardData.Card_ID}");
        }

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
                // Set top value to SpeedCost and bottom value to Atk
                newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.SpeedCost.ToString();
                newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Atk.ToString();
                break;
            case 3:
                // Defense Card
                // Set top value to SpeedCost and bottom value to Def
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
                // Leave values empty
                newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
    }

    public void DeckClick() {
        // Function so the deck button does not directly use DrawCard

        // Check if deck has less than 2 cards, add the discard pile to the deck
        if (playerDeck.Count < 2)
        {
            playerDeck.AddRange(playerDiscard);
            playerDiscard.Clear();
            // Randomize the deck list
            playerDeck = playerDeck.OrderBy(x => Random.value).ToList();
        }
        DrawCard();
        DisableDeckClick();
        // Call next phase
        TurnSequence("Play");
    }
    public void DrawCard()
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(DrawSingleCard());
        }
    }

    IEnumerator DrawSingleCard() {
        yield return new WaitForSeconds(0.5f);
        // Find card data in cardsObject
        CardData singleCardData = cardsObject.cards.Find(card => card.Card_ID == playerDeck[0]);

        // Wait half a second
        

        // Instantiate Card
        GameObject newCard = Instantiate(cardPrefab, HandPanel);
        SetData(newCard, singleCardData);

        // Add card to discard pile, remove from deck
        playerDiscard.Add(playerDeck[0]);
        playerDeck.RemoveAt(0);

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Card Drawn");
    }

    private void AllowIdentityCardClick() {
        // Activates click event for player identity cards
        foreach (Transform card in PlayerPanelTop)
        {
            card.GetComponent<Button>().interactable = true;
        }
        foreach (Transform card in PlayerPanelBottom)
        {
            card.GetComponent<Button>().interactable = true;
        }
    }

    private void DisableIdentityCardClick() {
        // Deactivate identity card click event
        foreach (Transform card in PlayerPanelTop)
        {
            card.GetComponent<Button>().interactable = false;
        }
        foreach (Transform card in PlayerPanelBottom)
        {
            card.GetComponent<Button>().interactable = false;
        }
    }

    private void AllowDeckClick() {
        // Activates click event for deck button
        GameObject.Find("DeckButton").GetComponent<Button>().interactable = true;
    }

    private void DisableDeckClick() {
        // Deactivate deck button click event
        GameObject.Find("DeckButton").GetComponent<Button>().interactable = false;
    }

    private void AllowHandClick() {
        // Activates click event for player hand cards
        foreach (Transform card in HandPanel)
        {
            card.GetComponent<Button>().interactable = true;
        }
    }

    private void DisableHandClick() {
        // Deactivate hand card click event
        foreach (Transform card in HandPanel)
        {
            card.GetComponent<Button>().interactable = false;
        }
    }
    public void TurnSequence(string phase)
    {
        /*
        Start turn
        enable identity card clickability
        Listen for click on identity cards (top or bottom panel)
        use cardclicked function
        disable identity card clickability
        enable deck button clickability
        listen for deck click
        use cardClicked() function
        disable deck button clickability
        enable hand card clickability
        listen for hand card click
        if card is type 2 or 3, disable clickability, call cardClicked function
        if card is type 4 or 5, call cardClicked function, destroy card
        make all necessary calculations to the top enemy card
        if card is type 2, destroy at the end of player turn
        if card is type 3, destroy at the end of enemy turn
        end turn
        */

        // If two of the player's identity cards are dead, end the game
        if (PlayerPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && (PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 | PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0) | PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0)
        {
            Debug.Log("Player has lost");
            LosePanel.SetActive(true);  // Shows Lose Screen
            return;
        }

        // If two of the enemy's identity cards are dead, end the game
        if (EnemyPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && (EnemyPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 | EnemyPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0) | EnemyPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && EnemyPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0)
        {
            Debug.Log("Player has won");
            WonPanel.SetActive(true); // Shows Win Screen
            return;
        }

        // Delete middle card if there are 3 cards
        if (HandPanel.childCount == 3)
        {
            StartCoroutine(DestroyTrue(HandPanel.GetChild(1).gameObject));
        }

        // Disable deck button clickability and hand cards clickability
        GameObject.Find("DeckButton").GetComponent<Button>().interactable = false;
        foreach (Transform card in HandPanel)
        {
            card.GetComponent<Button>().interactable = false;
        }

        if (phase == "Swap")
        {
            // Allow for swapping of identity cards
            AllowIdentityCardClick();
        }
        else if (phase == "Draw")
        {
            // Allow deck click
            AllowDeckClick();
        }
        else if (phase == "Play")
        {
            // Allow for playing of hand cards
            AllowHandClick(); 
        }
        else if (phase == "End")
        {
            // End of player turn
            // Disable hand clickability
            DisableHandClick();
            
            // Apply attack stat from player top card to the HP of the enemy top card, going through the enemy defense as well
            GameObject playerTopCard = PlayerPanelTop.GetChild(0).gameObject;
            GameObject enemyTopCard = EnemyPanelTop.GetChild(0).gameObject;

            // Check if the enemy defense is higher than the player attack
            if (playerTopCard.GetComponent<CardScript>().cardData.Atk < enemyTopCard.GetComponent<CardScript>().cardData.Def)
            {
                // If the enemy defense is higher, substract the difference from the enemy defense
                enemyTopCard.GetComponent<CardScript>().cardData.Def -= playerTopCard.GetComponent<CardScript>().cardData.Atk;
            }
            else
            {
                // If the player attack is higher, substract the difference from the enemy HP
                enemyTopCard.GetComponent<CardScript>().cardData.HP -= playerTopCard.GetComponent<CardScript>().cardData.Atk - enemyTopCard.GetComponent<CardScript>().cardData.Def;
                // Update enemy TMP
                enemyTopCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = enemyTopCard.GetComponent<CardScript>().cardData.HP.ToString();
            }

            // Check if the enemy card is dead, do not allow HP to fall below 0
            if (enemyTopCard.GetComponent<CardScript>().cardData.HP <= 0)
            {
               // Not allow  HP to be negative
                enemyTopCard.GetComponent<CardScript>().cardData.HP = 0;
                enemyTopCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = enemyTopCard.GetComponent<CardScript>().cardData.HP.ToString();
            }

            // Delete the used card if it is an attack card from the bottom panel
            foreach (Transform card in PlayerPanelBottom)
            {
                if (card.GetComponent<CardScript>().cardData.Type_ID == 2)
                {
                    StartCoroutine(DestroyTrue(card.gameObject));
                }
            }
           

            playerTurn = false;

            // Go to enemy logic
            EnemyTurn();
        }
    }

    
    public void EnemyTurn() {
        // AI TODO

        GameObject enemyTopCard = EnemyPanelTop.GetChild(0).gameObject;
        GameObject playerTopCard = PlayerPanelTop.GetChild(0).gameObject;

        // If three of the player's identity cards are dead, end the game
        if (PlayerPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && (PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 | PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0) | PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0)
        {
            Debug.Log("Player has lost");
            return;
        }

        // If three of the enemy's identity cards are dead, end the game
        if (EnemyPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && (EnemyPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 | EnemyPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0) | EnemyPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 && EnemyPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0)
        {
            Debug.Log("Player has won");
            return;
        }

        // Draw 2 cards for the enemy non visually
        List<int> enemyHand = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            enemyHand.Add(enemyDeck[0]);
            enemyDeck.RemoveAt(0);
        }

        // Check if top card speed is less than the speed of a bottom panel card or if HP is 0, and bottom card has more than 0 hp, swap
        foreach (Transform card in EnemyPanelBottom)
        {
            if (enemyTopCard.GetComponent<CardScript>().cardData.Speed < card.GetComponent<CardScript>().cardData.Speed || enemyTopCard.GetComponent<CardScript>().cardData.HP <= 0 && card.GetComponent<CardScript>().cardData.HP > 0)
            {
                // Manual swap, no swap function for enemy yet
                // Must swap the enemytoppanel card for a card in enemybottompanel
                // Store parent transforms
                Transform topCardParent = enemyTopCard.transform.parent;
                Transform bottomCardParent = EnemyPanelBottom;

                // If card is not dead, update speed
                if (enemyTopCard.GetComponent<CardScript>().cardData.HP > 0)
                {
                    enemyTopCard.GetComponent<CardScript>().cardData.Speed = cardsObject.cards.Find(card => card.Card_ID == enemyTopCard.GetComponent<CardScript>().cardData.Card_ID).Speed;
                    enemyTopCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyTopCard.GetComponent<CardScript>().cardData.Speed.ToString();
                }

                // Swap parents
                enemyTopCard.transform.SetParent(bottomCardParent);
                card.transform.SetParent(topCardParent);

                // Update top card
                enemyTopCard = card.gameObject;   

                // Break the loop
                break;
            }
        }

        // Check if any cards are playable by the enemy from the speed
        
        foreach (int cardID in enemyHand)
        {
            CardData cardData = cardsObject.cards.Find(card => card.Card_ID == cardID);
            if (cardData.SpeedCost <= enemyTopCard.GetComponent<CardScript>().cardData.Speed)
            {
                // check for type 5 cards, play those first
                // check if there are any type 2 or 3 cards, if not, end turn
                // If there are type 2 or 3 cards, check for type 4 cards, play those, then type 3 or 4 cards
                // If there are no type 4 cards, play either a type 2 or type 3 card, depending on if the top card has more attack or defense
                // If the top card has more attack, play a type 2 card, if it has more defense, play a type 3 card
                // If the top card has equal attack and defense, play a type 2 card
                // If there are no type 2 or 3 cards, end turn
                // If there are no cards to play, end turn

                // Play the card
                // Instantiate in the middle of the bottom panel
                GameObject newCard = Instantiate(cardPrefab, EnemyPanelBottom);
                SetData(newCard, cardData);
                // Set the card to middle of bottom enemy panel
                newCard.transform.SetSiblingIndex(1);

                // Substract speed cost to speed of the top card
                enemyTopCard.GetComponent<CardScript>().cardData.Speed -= cardData.SpeedCost;
                enemyTopCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyTopCard.GetComponent<CardScript>().cardData.Speed.ToString();

                // Check if the player defense is higher than the enemy attack
                if (cardData.Atk < playerTopCard.GetComponent<CardScript>().cardData.Def)
                {
                    // If the player defense is higher, substract the difference from the player defense
                    playerTopCard.GetComponent<CardScript>().cardData.Def -= cardData.Atk;
                    // Destroy used card
                    StartCoroutine(DestroyTrue(newCard));
                    // Destroy player defense card
                    foreach (Transform playerCard in PlayerPanelBottom)
                    {
                        if (playerCard.GetComponent<CardScript>().cardData.Type_ID == 3)
                        {
                            StartCoroutine(DestroyTrue(playerCard.gameObject));
                        }
                    }
                }
                else
                {
                    // If the enemy attack is higher, substract the difference from the player HP
                    playerTopCard.GetComponent<CardScript>().cardData.HP -= cardData.Atk - playerTopCard.GetComponent<CardScript>().cardData.Def;
                    // Update player TMP
                    playerTopCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerTopCard.GetComponent<CardScript>().cardData.HP.ToString();
                    // Destroy used card
                    StartCoroutine(DestroyTrue(newCard));
                }
            }
        }

        // End of enemy turn
        // Apply attack stat from enemy top card to the HP of the player top card, going through the player defense as well
        
        // Start of player turn
        TurnSequence("Swap");
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
                TurnSequence("Draw");
                DisableIdentityCardClick();
                break;
            case 2:
                // Attack Card
                Debug.Log("Attack Card Clicked");
                AttackCardClick(clickedCard);
                // Disable hand clickability, go to End phase
                DisableHandClick();
                TurnSequence("End");
                break;
            case 3:
                // Defense Card
                Debug.Log("Defense Card Clicked");
                DefenseCardClick(clickedCard);
                // Disable hand clickability, go to End phase
                DisableHandClick();
                TurnSequence("End");
                break;
            case 4:
                // Effect card
                Debug.Log("Effect Card Clicked");
                EffectCardClicked(clickedCard);
                break;
            case 5:
                // Draw card
                // Draw 2 cards
                Debug.Log("Draw Card Clicked");
                DrawCardClicked(clickedCard);
                // Disable clicabillity of the card
                clickedCard.GetComponent<Button>().interactable = false;
                break;
        }
    }

    public void Swap(GameObject clickedCard)
    {
        // Swap the top card with the clicked card

        // Get top card
        GameObject topCard = PlayerPanelTop.GetChild(0).gameObject;

        // Check if the top card is the clicked card, speed stays the same
        if (topCard == clickedCard) {
            Debug.Log("Top card is clicked card");
            return;
        }
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

    public void EffectCardClicked(GameObject clickedCard) {
        // Add all stats from the effect card to the top card
        // Move to the middle of the bottom panel

        // Get top card
        GameObject topCard = PlayerPanelTop.GetChild(0).gameObject;

        // Move clicked card to the middle of the bottom panel
        clickedCard.transform.SetParent(PlayerPanelBottom);
        clickedCard.transform.SetSiblingIndex(1);

        // Make card not clickable
        clickedCard.GetComponent<Button>().interactable = false;

        if (clickedCard.GetComponent<CardScript>().cardData.Card_ID == 15) {
            // Check for specific case of defense reduction
            topCard = EnemyPanelTop.GetChild(0).gameObject;
        }

        // Add all stats from the effect card to the target card

        CardData topCardData = topCard.GetComponent<CardScript>().cardData;
        CardData clickedCardData = clickedCard.GetComponent<CardScript>().cardData;

        topCardData.HP += clickedCardData.HP;
        topCardData.Atk += clickedCardData.Atk;
        topCardData.Def += clickedCardData.Def;

        // Destroy clicked card with a destroy function that waits for a delay before destroying
        StartCoroutine(DestroyTrue(clickedCard));  
    }

    public void DrawCardClicked(GameObject clickedCard) {
        // Draw 2 cards
        // Destroy clicked card

        // Draw 2 cards
        StartCoroutine(DrawSingleCard());
        StartCoroutine(DrawSingleCard());
        StartCoroutine(DestroyTrue(clickedCard));
    }
    IEnumerator DestroyTrue(GameObject clickedCard) {
        // Wait for a delay before destroying the clicked card
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Destroying" + clickedCard);
        Destroy(clickedCard);
    }
}