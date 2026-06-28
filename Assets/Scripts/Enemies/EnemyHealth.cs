using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(int damage)
    {
        knockBack.GetKnockBack(PlayerController.instance.transform, 5f);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDestroyEnemy(damage));
    }

    private IEnumerator CheckDestroyEnemy(int damage)
    {
        yield return new WaitForSeconds(flash.GetRestoreDefaultMatTime() - 0.1f);
        DestroyEnemy(damage);
    }

    private void DestroyEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}