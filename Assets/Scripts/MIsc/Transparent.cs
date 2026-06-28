using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Transparent : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparentcyAmount = 0.5f;

    [Range(0, 1)]
    [SerializeField] private float transparentcyAmount_mp = 0.8f;

    [SerializeField] private float fadeTime = 0.3f;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (spriteRenderer) StartCoroutine(FadeRoutine(true));
            if (tilemap) StartCoroutine(FadeRoutine_TileMap(true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (spriteRenderer) StartCoroutine(FadeRoutine(false));
            if (tilemap) StartCoroutine(FadeRoutine_TileMap(false));
        }
    }

    private IEnumerator FadeRoutine(bool mark)
    {
        float transparentcyAmount_ = transparentcyAmount;
        if (!mark) transparentcyAmount_ = 1f;
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparentcyAmount_, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine_TileMap(bool mark)
    {
        float transparentcyAmount_ = transparentcyAmount_mp;
        if (!mark) transparentcyAmount_ = 1f;
        float elapsedTime = 0;
        float startValue = tilemap.color.a;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparentcyAmount_, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.b, tilemap.color.g, newAlpha);
            yield return null;
        }
    }
}