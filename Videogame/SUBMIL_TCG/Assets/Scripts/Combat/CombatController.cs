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

    public string apiCardData = @"";

    public List<int> playerDeck = new List<int>(); /*{1, 2, 3, 7, 8, 8, 8, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8}*/

    public List<int> playerDiscard = new List<int>();
    public List<int> enemyDeck = new List<int>() {4, 5, 6, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7, 7, 7};

    public List<int> enemyDiscard = new List<int>();
    
    [SerializeField] Cards cardsObject;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform PlayerPanelTop;
    [SerializeField] Transform PlayerPanelBottom;
    [SerializeField] Transform EnemyPanelTop;
    [SerializeField] Transform EnemyPanelBottom;
    [SerializeField] Transform HandPanel;
    public List<int> enemyHand = new List<int>();

    public string phase;

    public GameObject LosePanel;
    public GameObject WonPanel;

    public void prepareIdentityCards()
    {
        // Instatiates first 3 cards of the player's deck on the appropriate panels

        // Local variables for parent reference
        Transform PlayerPanelParent;
        Transform EnemyPanelParent;

        // Json data into object
        cardsObject = JsonUtility.FromJson<Cards>(apiCardData);
        if (cardsObject == null){
            Debug.LogError("cardsObject is null!");
            //return; // Exit the method to prevent further null reference errors
            }
        else if (cardsObject.cards == null){
            Debug.LogError("cardsObject.cards is null!");
            return; // Exit the method to prevent further null reference errors
        }
        //Debug.Log("Cards Object: " + cardsObject);


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

        // Randomize both decks
        playerDeck = playerDeck.OrderBy(x => Random.value).ToList();
        enemyDeck = enemyDeck.OrderBy(x => Random.value).ToList();
        TurnSequence("Swap");
    } 

    void Start()
    {
        LosePanel.SetActive(false);
        WonPanel.SetActive(false);

        LoadPlayerDeck();

        APIConnectionCombat apiConnectionCombat = GetComponent<APIConnectionCombat>();
        apiConnectionCombat.GetData(prepareIdentityCards); // Pass prepareIdentityCards as the callback
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are enough children before trying to access them
        if (PlayerPanelTop.childCount > 0 && PlayerPanelBottom.childCount > 1)
        {
            // If two of the player's identity cards are dead, end the game
            if (PlayerPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 
                && (PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 
                | PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0) 
                | PlayerPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 
                && PlayerPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0)
            {
                Debug.Log("Player has lost");
                Lose();
            }
        }

        // Similar check for enemy's identity cards
        if (EnemyPanelTop.childCount > 0 && EnemyPanelBottom.childCount > 1)
        {
            // If two of the enemy's identity cards are dead, end the game
            if (EnemyPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 &&
                (EnemyPanelBottom.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0 ||
                EnemyPanelBottom.GetChild(1).GetComponent<CardScript>().cardData.HP <= 0))
            {
                Debug.Log("Player has won");
                Win();
            }
        }
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
        Debug.Log("Player Deck: " + string.Join(", ", playerDeck));
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
        cardscript.cardData.Speed_Cost = singleCardData.Speed_Cost;
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
                // Set top value to Speed_Cost and bottom value to Atk
                newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Speed_Cost.ToString();
                newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Atk.ToString();
                break;
            case 3:
                // Defense Card
                // Set top value to Speed_Cost and bottom value to Def
                newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cardscript.cardData.Speed_Cost.ToString();
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

        // Delete middle card if there are 3 cards
        if (PlayerPanelBottom.childCount == 3)
        {
            StartCoroutine(DestroyTrue(PlayerPanelBottom.GetChild(1).gameObject));
        }

        // Disable deck button clickability and hand cards clickability
        GameObject.Find("DeckButton").GetComponent<Button>().interactable = false;
        GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
        foreach (Transform card in HandPanel)
        {
            card.GetComponent<Button>().interactable = false;
        }

        if (phase == "Swap")
        {
            // if top card is dead, manually swap with a card in the bottom panel, not using swap coz its bugged
            if (PlayerPanelTop.GetChild(0).GetComponent<CardScript>().cardData.HP <= 0)
            {
                // Store parent transforms
                Transform topCardParent = PlayerPanelTop;
                Transform bottomCardParent = PlayerPanelBottom;

                // Swap parents
                PlayerPanelTop.GetChild(0).transform.SetParent(bottomCardParent);
                PlayerPanelBottom.GetChild(0).transform.SetParent(topCardParent);
                // Go to next phase
                TurnSequence("Draw");
            }
            else {
            // Allow for swapping of identity cards
            AllowIdentityCardClick();
            }
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
            // Allow end turn button
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;
        }
        else if (phase == "End")
        {
            // End of player turn
            // Disable hand clickability
            DisableHandClick();
            // Disallow end turn button
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
            
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

            // Set attack back to 0
            playerTopCard.GetComponent<CardScript>().cardData.Atk = 0;
           
            // Go to enemy logic
            EnemyTurn();
        }
    }

    public void EndTurnButtonClick()
    {
        // Call the turn sequence function with the "End" phase
        TurnSequence("End");
    }

    public void Lose() 
    {
            LosePanel.SetActive(true);  // Shows Lose Screen
            // Should avoid enemy from playing
            TurnSequence("Swap");
            return;
    }

    public void Win()
    {
            WonPanel.SetActive(true); // Shows Win Screen
            // Should avoid enemy from playing
            TurnSequence("Swap");
            return;
    }

    void CheckEnemyDeck() {
        // Check if enemy deck has less than 3 cards, add the discard pile to the deck if so
        if (enemyDeck.Count < 3)
        {
            enemyDeck.AddRange(enemyDiscard);
            enemyDiscard.Clear();

            // Randomize the deck list
            enemyDeck = enemyDeck.OrderBy(x => Random.value).ToList();
        }
    }
    
    public void EnemyTurn() {
    // For managing timings, called as a coroutine, also convenient for the transition from PlayerTurn to EnemyTurn
    StartCoroutine(EnemyTurnRoutine());
    }

private IEnumerator EnemyTurnRoutine() {
    // AI TODO

    GameObject enemyTopCard = EnemyPanelTop.GetChild(0).gameObject;
    GameObject playerTopCard = PlayerPanelTop.GetChild(0).gameObject;

    // Draw 3 cards for the enemy non visually if deck has more than 3, if not, add discard pile to deck and scramble
    CheckEnemyDeck();
    for (int i = 0; i < 3; i++) {
        enemyHand.Add(enemyDeck[0]);
        enemyDeck.RemoveAt(0);
    }

    // Check if top card speed is less than the speed of a bottom panel card or if HP is 0, and bottom card has more than 0 hp, swap
    yield return StartCoroutine(SwapEnemyCards(enemyTopCard));

    // Sort enemy hand by speed cost from lowest to highest
    enemyHand.Sort((a, b) => cardsObject.cards.Find(card => card.Card_ID == a).Speed_Cost.CompareTo(cardsObject.cards.Find(card => card.Card_ID == b).Speed_Cost));

    // Instantiate all type 17 cards in the enemy hand one by one
    while (enemyHand.Contains(17)) {
        yield return StartCoroutine(InstantiateAndHandleCard(17));

        // Add 2 cards to the enemy hand if cards in deck > 2
        if (enemyDeck.Count > 2) {
            for (int i = 0; i < 2; i++) {
                enemyHand.Add(enemyDeck[0]);
                enemyDeck.RemoveAt(0);
            }
        }
    }

    // Check for type 2 cards and play attack boost (Card_ID 14) cards, then play the attack card
    // if (enemyHand.Contains(7) || enemyHand.Contains(8) || enemyHand.Contains(9)) {
    //     // Play attack boost card
    //     if (enemyHand.Contains(14)) {
    //         yield return StartCoroutine(InstantiateAndHandleCard(14));
    //     }

        // Play attack card
        foreach (int cardID in enemyHand) {
            if (cardsObject.cards.Find(card => card.Card_ID == cardID).Type_ID == 2) {
                // Ckeck for attack boost cards (ID 14)
                if (enemyHand.Contains(14)) {
                    yield return StartCoroutine(InstantiateAndHandleCard(14));
                }
                // Play the attack card and then break to avoid playing multiple attack cards
                yield return StartCoroutine(InstantiateAndHandleCard(cardID));  
                // Go to player turn
                TurnSequence("Swap");
                yield break;
            }
        }

    
    // } else {
    //     // If there are no attack cards, play the card with the lowest speed cost
    //     yield return StartCoroutine(InstantiateAndHandleCard(enemyHand[0]));
    //     TurnSequence("Swap");
    // }
}

private void applyEnemyDamage() {
    // Applies the attack of the enemy top card to the player top card's HP, updates TMPs. Resets attack to 0
    GameObject enemyTopCard = EnemyPanelTop.GetChild(0).gameObject;
    GameObject playerTopCard = PlayerPanelTop.GetChild(0).gameObject;

    // Check if the player defense is higher than the enemy attack
    if (enemyTopCard.GetComponent<CardScript>().cardData.Atk < playerTopCard.GetComponent<CardScript>().cardData.Def) {
        // If the player defense is higher, substract the difference from the player defense
        playerTopCard.GetComponent<CardScript>().cardData.Def -= enemyTopCard.GetComponent<CardScript>().cardData.Atk;
    } else {
        // If the enemy attack is higher, substract the difference from the player HP
        playerTopCard.GetComponent<CardScript>().cardData.HP -= enemyTopCard.GetComponent<CardScript>().cardData.Atk - playerTopCard.GetComponent<CardScript>().cardData.Def;
        // Update player TMP
        playerTopCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerTopCard.GetComponent<CardScript>().cardData.HP.ToString();
    }

    // Check if the player card is dead, do not allow HP to fall below 0
    if (playerTopCard.GetComponent<CardScript>().cardData.HP <= 0) {
        // Not allow HP to be negative
        playerTopCard.GetComponent<CardScript>().cardData.HP = 0;
        playerTopCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerTopCard.GetComponent<CardScript>().cardData.HP.ToString();
    }

    // Reset top enemy card atk to 0
    enemyTopCard.GetComponent<CardScript>().cardData.Atk = 0;
}

// Coroutine to instantiate and handle a card
private IEnumerator InstantiateAndHandleCard(int cardID) {
    yield return StartCoroutine(instantiateEnemyCard(cardID));
    // Deal Atk equal to the enemy top card Atk to the player top card
    applyEnemyDamage();
    yield return StartCoroutine(DestroyEnemyCard());

    // Remove the card from the hand after it has been handled
    enemyHand.Remove(cardID);
}

// Coroutine to instantiate an enemy card
private IEnumerator instantiateEnemyCard(int cardID) {
    // Find card data in cardsObject
    CardData singleCardData = cardsObject.cards.Find(card => card.Card_ID == cardID);

    yield return new WaitForSeconds(1.0f);

    // Instantiate card
    GameObject newCard = Instantiate(cardPrefab, EnemyPanelBottom);
    SetData(newCard, singleCardData);

    // Set to middle of bottom enemy panel
    newCard.transform.SetSiblingIndex(1);

    // Add card to discard pile
    enemyDiscard.Add(cardID);

    Debug.Log("Card Instantiated");

    // Substract speed cost to speed of the top card
    GameObject enemyTopCard = EnemyPanelTop.GetChild(0).gameObject;
    enemyTopCard.GetComponent<CardScript>().cardData.Speed -= singleCardData.Speed_Cost;
    enemyTopCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyTopCard.GetComponent<CardScript>().cardData.Speed.ToString();

    // Add Atk, Def, HP to the top enemy card
    enemyTopCard.GetComponent<CardScript>().cardData.HP += singleCardData.HP;
    enemyTopCard.GetComponent<CardScript>().cardData.Atk += singleCardData.Atk;
    enemyTopCard.GetComponent<CardScript>().cardData.Def += singleCardData.Def;
}

// Coroutine to destroy the enemy card in the middle of the bottom panel
private IEnumerator DestroyEnemyCard() {
    yield return new WaitForSeconds(1.5f);
    Destroy(EnemyPanelBottom.GetChild(1).gameObject);
}

// Coroutine to swap enemy cards
private IEnumerator SwapEnemyCards(GameObject enemyTopCard) {
    yield return new WaitForSeconds(2.0f);
    foreach (Transform card in EnemyPanelBottom) {
        if (enemyTopCard.GetComponent<CardScript>().cardData.Speed < card.GetComponent<CardScript>().cardData.Speed || (enemyTopCard.GetComponent<CardScript>().cardData.HP <= 0 && card.GetComponent<CardScript>().cardData.HP > 0)) {
            // Manual swap, no swap function for enemy yet
            // Must swap the enemytoppanel card for a card in enemybottompanel
            // Store parent transforms
            Transform topCardParent = enemyTopCard.transform.parent;
            Transform bottomCardParent = EnemyPanelBottom;

            // If card is not dead, update speed
            if (enemyTopCard.GetComponent<CardScript>().cardData.HP > 0) {
                enemyTopCard.GetComponent<CardScript>().cardData.Speed = cardsObject.cards.Find(cardData => cardData.Card_ID == enemyTopCard.GetComponent<CardScript>().cardData.Card_ID).Speed;
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
        if (clickedCard.GetComponent<CardScript>().cardData.Speed_Cost > topCard.GetComponent<CardScript>().cardData.Speed) {
            Debug.Log("Not enough speed");
            return;
        }

        // Move clicked card to the middle of the bottom panel
        clickedCard.transform.SetParent(PlayerPanelBottom);
        clickedCard.transform.SetSiblingIndex(1);

        // Make card not clickable
        clickedCard.GetComponent<Button>().interactable = false;

        // Substract speed cost to speed of the top card
        topCard.GetComponent<CardScript>().cardData.Speed -= clickedCard.GetComponent<CardScript>().cardData.Speed_Cost;
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
        if (clickedCard.GetComponent<CardScript>().cardData.Speed_Cost > topCard.GetComponent<CardScript>().cardData.Speed) {
            Debug.Log("Not enough speed");
            return;
        }

        // Move clicked card to the middle of the bottom panel
        clickedCard.transform.SetParent(PlayerPanelBottom);
        clickedCard.transform.SetSiblingIndex(1);

        // Make card not clickable
        clickedCard.GetComponent<Button>().interactable = false;

        // Substract speed cost to speed of the top card
        topCard.GetComponent<CardScript>().cardData.Speed -= clickedCard.GetComponent<CardScript>().cardData.Speed_Cost;
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
        // Update TMP HP
        topCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = topCardData.HP.ToString();
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
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Destroying" + clickedCard);
        Destroy(clickedCard);
    }
}