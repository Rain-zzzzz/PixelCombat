using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float time = 0.2f;
    private Rigidbody2D rb;

    public bool isGetKnock { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform damageSource, float knockBackThtust)
    {
        isGetKnock = true;
        Vector2 difference = knockBackThtust * rb.mass * (transform.position - damageSource.position).normalized;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(GetKnockRoutine());
    }

    private IEnumerator GetKnockRoutine()
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector2.zero;
        isGetKnock = false;
    }
}