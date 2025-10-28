using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CardStats;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject card;

    public TMP_Text scoreText;
    public float score = 0f;

    [SerializeField]
    public float foilOdds = 0.025f;

    public CardDatabase cardDatabase;
    Dictionary<Rarity, float> rarityChances = new Dictionary<Rarity, float>()
    {
        { Rarity.Common, 0.6f },
        { Rarity.Uncommon, 0.25f },
        { Rarity.Rare, 0.1f },
        { Rarity.Epic, 0.05f },
    };

    Dictionary<Condition, float> conditionChances = new Dictionary<Condition, float>()
    {
        { Condition.Poor, 0.02f },
        { Condition.Good, 0.95f },
        { Condition.Mint, 0.03f },
    };

    Dictionary<Condition, float> conditionModifiers = new Dictionary<Condition, float>()
    {
        { Condition.Poor, 0.9f },
        { Condition.Good, 1f },
        { Condition.Mint, 1.5f },
    };


    public void Awake()
    {
        InstantiateCard();
    }
    public void Start()
    {
        scoreText.text = score.ToString();
        //InstantiateCard();
    }
    public void InstantiateCard()
    {
        scoreText.text = score.ToString();

        float foilRNG = Random.value;
        float rarityRNG = Random.value;
        float conditionRNG = Random.value;

        Rarity rarity = getRandomRarity(rarityRNG);
        Foil foil = getRandomFoil(foilRNG);
        Condition condition = getRandomCondition(conditionRNG);

        CardData baseCard = GetRandomCardByRarity(rarity);
        if (baseCard == null)
        {
            Debug.LogWarning("Nessuna carta trovata con rarità: " + rarity);
            return;
        }

        GameObject newCard = Instantiate(card, transform.position, Quaternion.identity);

        gameObject.GetComponent<ParallaxManager>().cardTransform = newCard.transform;

        CardLook cardScript = newCard.GetComponent<CardLook>();

        cardScript.cardData = baseCard;
        cardScript.cardData.scoreValue = getValue(cardScript.cardData.rarity) * (foil == Foil.Foil ? 2 : 1) * conditionModifiers[condition];
        cardScript.cardData.foil = foil;
        cardScript.cardData.condition = condition;

        Debug.Log($"Carta: {baseCard.cardName} | Rarità: {rarity} | Foil: {foil} | Condizione: {condition} | Score: {cardScript.cardData.scoreValue}");

        gameObject.GetComponent<DisappearingArrows>().EnableDelay();
    }

    private CardData GetRandomCardByRarity(Rarity rarity)
    {
        List<CardData> candidates = cardDatabase.cards.FindAll(card => card.rarity == rarity);
        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }

    private int getValue(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 1;
            case Rarity.Uncommon:
                return 2;
            case Rarity.Rare:
                return 5;
            case Rarity.Epic:
                return 10;
            default:
                return 0;
        }
    }

    private Rarity getRandomRarity(float rarityRng)
    {
        float cumulative = 0;
        foreach (var kvp in rarityChances)
        {
            cumulative += kvp.Value;
            if (rarityRng < cumulative)
            {
                return kvp.Key;
            }
        }

        return Rarity.Common;
    }

    private Foil getRandomFoil(float foilRng)
    {

        if (foilRng < foilOdds)
        {
            return Foil.Foil;
        }

        return Foil.NonFoil;

    }

    private Condition getRandomCondition(float conditionRng)
    {
        float cumulative = 0;
        foreach (var kvp in conditionChances)
        {
            cumulative += kvp.Value;
            if (conditionRng < cumulative)
            {
                return kvp.Key;
            }
        }

        return Condition.Good;
    }
}
