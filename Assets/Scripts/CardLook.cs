using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static CardStats;

public class CardLook : MonoBehaviour
{
    public CardData cardData;

    public SpriteRenderer background;
    public SpriteRenderer border;

    public TMP_Text cardName;
    public TMP_Text collectorNumber;

    public GameObject foilOverlay;

    void Start()
    {
        ApplyLook();
    }

    void ApplyLook()
    {
        
        switch (cardData.rarity)
        {
            case Rarity.Common:
                border.color = Color.gray;
                break;
            case Rarity.Uncommon:
                border.color = Color.blue;
                break;
            case Rarity.Rare:
                border.color = Color.yellow;
                break;
            case Rarity.Epic:
                border.color = new Color(152f, 9f, 247f);
                break;
        }

        if (cardData.foil == Foil.Foil)
            foilOverlay.SetActive(true);

        
        background.sprite = cardData.cardSprite;
        ResizeSpriteToUnit(background, 0.9f);
        collectorNumber.text = cardData.collectorNumber.ToString();

        cardName.text = cardData.cardName;


    }

    void ResizeSpriteToUnit(SpriteRenderer sr, float targetUnitSize = 1f)
    {
        Sprite sprite = sr.sprite;
        float spriteWidth = sprite.rect.width / sprite.pixelsPerUnit;
        float spriteHeight = sprite.rect.height / sprite.pixelsPerUnit;

        sr.transform.localScale = new Vector3(
            targetUnitSize / spriteWidth,
            targetUnitSize / spriteHeight,
            1f
        );
    }
}
