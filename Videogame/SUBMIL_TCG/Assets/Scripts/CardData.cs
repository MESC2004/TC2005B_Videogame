using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public int Card_ID;
    public int Type_ID;
    public string Name;
    public int HP;
    public int Speed;
    public int SpeedCost;
    public int Atk;
    public int Def;
    public string Passive;
    public string ImageUrl;
}

[System.Serializable]
public class Cards
{
    public List<CardData> cards;
}
