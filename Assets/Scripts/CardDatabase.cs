using System.Collections.Generic;
using UnityEngine;
using static CardStats;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> cards;

    public CardData GetCardsByIdandRarity(int id, Rarity rarity)
    {
        return cards.Find(card => card.collectorNumber == id && card.rarity == rarity);
    }
}
