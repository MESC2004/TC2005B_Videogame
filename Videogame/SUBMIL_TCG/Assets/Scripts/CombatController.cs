using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class CombatController : MonoBehaviour
{
    public string apiCardData;
    [SerializeField] List<int> playerDeck = new List<int>() { 1, 2, 3, 4 };
    [SerializeField] List<int> enemyDeck = new List<int>() { 4, 5, 6 };
    [SerializeField] bool inCombat;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform PlayerPanelTop;
    [SerializeField] Transform PlayerPanelBottom;
    [SerializeField] Transform EnemyPanelTop;
    [SerializeField] Transform EnemyPanelBottom;

    public void prepareIdentityCards()
    {
        Transform PlayerPanelParent;
        Transform EnemyPanelParent;

        Cards cardsObject = JsonUtility.FromJson<Cards>("{\"cards\":" + apiCardData + "}");

        for (int i = 0; i < 3; i++)
        {
            int deckCardID = playerDeck[i];
            int enemyCardID = enemyDeck[i];

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

            GameObject newCard = Instantiate(cardPrefab, PlayerPanelParent);
            SetData(newCard, singleCardData);

            GameObject newEnemyCard = Instantiate(cardPrefab, EnemyPanelParent);
            SetData(newEnemyCard, enemyCardData);
        }
    }

    public void SetData(GameObject newCard, CardData singleCardData)
    {
        CardScript cardscript = newCard.GetComponent<CardScript>();
        cardscript.cardData = singleCardData;

        newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = singleCardData.Speed.ToString();
        newCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = singleCardData.HP.ToString();

        StartCoroutine(LoadCardImage(newCard.transform.GetChild(2).GetComponent<RawImage>(), singleCardData.ImageUrl));
    }

    IEnumerator LoadCardImage(RawImage img, string imageUrl)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }
}
