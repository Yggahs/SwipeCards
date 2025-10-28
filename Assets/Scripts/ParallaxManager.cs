using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform;
    [Range(0f, 1f)]
    public float parallaxMultiplier;
}

public class ParallaxManager : MonoBehaviour
{
    public Transform cardTransform;
    public ParallaxLayer[] layers;

    private Vector3 lastCardPosition;

    void Start()
    {

        if (cardTransform == null)
            cardTransform = GameObject.Find("Card(Clone)").transform;
        

        lastCardPosition = cardTransform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cardTransform.position - lastCardPosition;

        foreach (var layer in layers)
        {
            Vector3 newPos = layer.layerTransform.position + new Vector3(delta.x * layer.parallaxMultiplier, delta.y * layer.parallaxMultiplier, 0f);
            layer.layerTransform.position = newPos;
        }

        lastCardPosition = cardTransform.position;
    }
}
