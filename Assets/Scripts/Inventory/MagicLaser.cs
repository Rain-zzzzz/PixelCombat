using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private bool isDrowing = true;
    private float laserLength;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFollowMouse();
    }

    public void UpdateLaserRange(float laserLength)
    {
        this.laserLength = laserLength;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float passTime = 0f;
        while (spriteRenderer.size.x < laserLength && isDrowing)
        {
            passTime += Time.deltaTime;
            float linearT = passTime / laserGrowTime;
            //sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(0, laserLength, linearT), 1f);
            //collider
            capsuleCollider.size = new Vector2(Mathf.Lerp(0, laserLength, linearT), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2((Mathf.Lerp(0, laserLength, linearT) * 0.5f), capsuleCollider.offset.y);
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger)
        {
            isDrowing = false;
        }
    }
}