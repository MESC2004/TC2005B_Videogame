using System.Collections.Generic;

[System.Serializable]
public class CardListWrapper
{
    public List<CardData> cards;

    public CardListWrapper(List<CardData> cards)
    {
        this.cards = cards;
    }
}
