// Miguel Soria A01028033
// 24/05/2020
// Script that handles the behavior of a card detecting clicks. Sends data to combat scene.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    public CombatController combatController;
    

    [SerializeField] bool inCombat;

    public CardData cardData;

    void Start()
    {
        combatController = FindObjectOfType<CombatController>();
        if (combatController == null)
        {
            Debug.LogError("CombatController not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {

        Debug.Log(this.cardData.Type_ID);
        combatController.CardClicked(this.cardData, this.gameObject);
        Debug.Log("Card Clicked");
    }

    
}
