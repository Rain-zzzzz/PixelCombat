using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyFVX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageSource>())
        {
            Instantiate(destroyFVX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}