using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : SingleTon<UIFade>
{
    [SerializeField]
    private float fadeSpeed = 1f;

    [SerializeField]
    private Image fadeScreen;

    private IEnumerator fadeRoution;

    public void FadeToBlack()
    {
        if (fadeRoution != null)
        {
            StopCoroutine(fadeRoution);
        }
        fadeRoution = FadeRoution(1);
        StartCoroutine(fadeRoution);
    }

    public void FadeToColor()
    {
        if (fadeRoution != null)
        {
            StopCoroutine(fadeRoution);
        }
        fadeRoution = FadeRoution(0);
        StartCoroutine(fadeRoution);
    }

    private IEnumerator FadeRoution(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}