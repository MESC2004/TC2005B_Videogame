using UnityEngine;
using TMPro;

public class Cards : MonoBehaviour
{
    public GameObject CardButtonPrefab;
    public Transform content; 

    void Start()
    {
        for (int i = 1; i <= 18; i++)
        {
            GameObject newCard = Instantiate(CardButtonPrefab, content);
            newCard.GetComponentInChildren<TextMeshProUGUI>().text = "Card " + i;
        }
    }
}
