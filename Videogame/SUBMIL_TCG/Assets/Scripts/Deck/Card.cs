using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;
    public Button cardButton;
    public Image cardImage;

    void Awake()
    {
        if (cardButton == null)
        {
            cardButton = GetComponent<Button>();
        }
        if (cardImage == null)
        {
            cardImage = GetComponent<Image>();
        }
    }

    public void SetCardImage(Sprite sprite)
    {
        if (cardImage != null)
        {
            cardImage.sprite = sprite;
        }
    }
}
