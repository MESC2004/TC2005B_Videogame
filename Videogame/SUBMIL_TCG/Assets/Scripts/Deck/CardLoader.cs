using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardLoader : MonoBehaviour
{
    public GameObject CardButtonPrefab;
    public Transform content;
    public TextMeshProUGUI WarningText;
    public Cards cardsObject = new Cards();

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

    void Start()
    {
        cardsObject = JsonUtility.FromJson<Cards>(apiCardData);

        if (cardsObject == null || cardsObject.cards == null)
        {
            Debug.LogError("Failed to load cards data.");
            return;
        }

        foreach (CardData card in cardsObject.cards)
        {
            GameObject newCard = Instantiate(CardButtonPrefab, content);
            Card cardComponent = newCard.GetComponent<Card>();
            cardComponent.cardData = card;
            cardComponent.cardButton = newCard.GetComponent<Button>();
            newCard.GetComponentInChildren<TextMeshProUGUI>().text = card.Name + " " + card.Card_ID;

            Button cardButton = newCard.GetComponent<Button>();
            cardButton.onClick.RemoveAllListeners();  // Asegúrate de que no haya listeners previos
            cardButton.onClick.AddListener(() => FindObjectOfType<CardSelectionManager>().OnCardSelected(cardComponent));
            newCard.GetComponent<Image>().color = Color.white;
        }

        if (WarningText != null)
        {
            WarningText.text = "";
            WarningText.gameObject.SetActive(false);
        }
    }

    public void ShowWarning(string message)
    {
        if (WarningText != null)
        {
            WarningText.text = message;
            WarningText.gameObject.SetActive(true);
            Invoke("HideWarning", 2.0f);
        }
    }

    void HideWarning()
    {
        if (WarningText != null)
        {
            WarningText.gameObject.SetActive(false);
        }
    }
}
