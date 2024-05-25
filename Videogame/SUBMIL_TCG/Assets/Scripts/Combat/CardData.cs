using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    [SerializeField] public int Card_ID;
    [SerializeField] public int Type_ID;
    [SerializeField] public string Name;
    [SerializeField] public int HP;
    [SerializeField] public int Speed;
    [SerializeField] public int SpeedCost;
    [SerializeField] public int Atk;
    [SerializeField] public int Def;
    [SerializeField] public string Passive;
    // Start is called before the first frame update
    
}


public class Cards
    {
        public List<CardData> cards;
    } 