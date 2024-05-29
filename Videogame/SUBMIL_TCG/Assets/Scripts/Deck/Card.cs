using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;
    public Button cardButton;

    void Awake()
    {
        if (cardButton == null)
        {
            cardButton = GetComponent<Button>();
        }
    }
}
