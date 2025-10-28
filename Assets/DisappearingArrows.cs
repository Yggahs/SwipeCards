using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearingArrows : MonoBehaviour
{
    public float delay = 1.5f;
    public GameObject arrowL;
    public GameObject arrowR;

    public float fadeDuration = 20f;
    private SpriteRenderer spriteL;
    private SpriteRenderer spriteR;

    Color cL;
    Color cR;

    void Awake()
    {
        spriteL = arrowL.GetComponent<SpriteRenderer>();
        spriteR = arrowR.GetComponent<SpriteRenderer>();
        cL = spriteL.color;
        cR = spriteR.color;
        spriteL.color = new Color(cL.r, cL.g, cL.b, 0f);
        spriteR.color = new Color(cR.r, cR.g, cR.b, 0f);
    }

    void Start()
    {
        Invoke("Enable", delay);
    }

    public void Disable()
    {
        arrowL.SetActive(false);
        arrowR.SetActive(false);

        StopCoroutine(FadeIn());
        
    }

    public void Enable()
    {
        SetSpriteColors();
        arrowL.SetActive(true);
        arrowR.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void DisableDelay()
    {
        Invoke("Disable", delay);
    }

    public void EnableDelay()
    {
        Invoke("Enable", delay);
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            spriteL.color = new Color(1f, 1f, 1f, alpha);
            spriteR.color = new Color(1f, 1f, 1f, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        spriteL.color = new Color(1f, 1f, 1f, 1f);
        spriteR.color = new Color(1f, 1f, 1f, 1f);
    }

    void SetSpriteColors()
    {
        spriteL.color = new Color(cL.r, cL.g, cL.b, 0f);
        spriteR.color = new Color(cR.r, cR.g, cR.b, 0f);
    }
}
