using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    private int currentHealth;
    private Flash flash;
    private KnockBack knockBack;
    private bool canKnock = true;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy)
        {
            TakeDamege(1, collision.transform);
        }
    }

    public void TakeDamege(int damegeAmount, Transform hitTransform)
    {
        if (!canKnock) { return; }
        knockBack.GetKnockBack(hitTransform.transform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canKnock = false;
        currentHealth -= damegeAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canKnock = true;
    }
}