using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
public class CardStats : ScriptableObject
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic
    }

    public enum Condition
    {
        Poor,
        Good,
        Mint
    }

    public enum Foil
    {
        NonFoil,
        Foil,
    }

    // public string cardName;

    // public Texture2D cardTexture;

    [System.Serializable]
    public class CardData
    {
        public Rarity rarity;
        public Condition condition;
        public Foil foil;
        public int collectorNumber;
        public float scoreValue;
        public string cardName;
        public Sprite cardSprite;
    }

    public CardData data;
}
