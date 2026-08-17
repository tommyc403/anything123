using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public float FlashDuration = 0.33f;

    private Image FlashImage;
    private Color imageColor;

    private void Start()
    {
        FlashImage = GetComponent<Image>();
        imageColor = FlashImage.color;
    }

    public void DoScreenFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    public IEnumerator FlashRoutine()
    {
        float timer = 0f;
        float t = 0f;
        float alphaFrom = 1f;
        float alphaTo = 0f;


        while (t < 1f)
        {
            timer += Time.deltaTime;
            t = Mathf.Clamp01(timer / FlashDuration);

            float alpha = Mathf.Lerp(alphaFrom, alphaTo, t);
            Color col = imageColor;
            col.a = alpha;

            FlashImage.color = col;
            yield return new WaitForEndOfFrame();
        }
    }


}
