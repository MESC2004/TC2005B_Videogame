using UnityEngine;

public class Card : MonoBehaviour
{
    
    public CardData cardData;
    
    void Start()
    {

    }

    public void DisplayCardStats()
    {
        Debug.Log("Card Stats - ID: " + cardData.Card_ID + ", Name: " + cardData.Name + ", HP: " + cardData.HP + ", Speed: " + cardData.Speed + ", Speed Cost: " + cardData.SpeedCost + ", Atk: " + cardData.Atk + ", Def: " + cardData.Def + ", Passive: " + cardData.Passive);
    }
}