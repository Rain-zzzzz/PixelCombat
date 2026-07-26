using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;

    public void Attack()
    {
        Vector2 targetPosition = PlayerController.Instance.transform.position - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.right = targetPosition;
    }
}