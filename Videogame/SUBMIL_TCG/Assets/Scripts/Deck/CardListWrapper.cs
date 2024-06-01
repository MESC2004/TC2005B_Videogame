// Fernando Fuentes
// 29/05/2024
// Script that handles the deck of cards

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
