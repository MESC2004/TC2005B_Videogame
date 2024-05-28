using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelect : MonoBehaviour
{
    public Transform SelectableCardsPanel;
    public Transform Panel;
    public GameObject CardButtonPrefab;
    void Start()
    {
        foreach (Transform card in SelectableCardsPanel)
        {
            Button cardButton = card.GetComponent<Button>();
            cardButton.onClick.AddListener(()=> OnCardSelected(card.gameObject));
        }
    }
     void OnCardSelected(GameObject card)
    {
        GameObject newCard = Instantiate(CardButtonPrefab, Panel);
        newCard.GetComponent<Card>().cardData = card.GetComponent<Card>().cardData;
        newCard.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponentInChildren<TextMeshProUGUI>().text;
        newCard.GetComponent<Button>().onClick.AddListener(() => OnCardDeselected(newCard));
    }


     void OnCardDeselected(GameObject card)
    {
        Destroy(card);
    }
}

