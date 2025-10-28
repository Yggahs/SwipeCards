using UnityEngine;

public class ResizeCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SyncCollider();
    }
     void SyncCollider()
    {
        RectTransform rt = GetComponent<RectTransform>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        // Adatta la dimensione del collider alla dimensione del rettangolo
        bc.size = rt.rect.size;

        // Sistema anche l'offset se necessario
        bc.offset = rt.rect.center;
    }
}
