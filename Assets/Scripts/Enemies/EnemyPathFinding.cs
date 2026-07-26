using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockBack.isGetKnock) { return; }
        //受到击退的时候暂停位移
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        //翻转精灵
        if (moveDir.x < 0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}