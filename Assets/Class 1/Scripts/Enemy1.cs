using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject deathEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(deathEffect,
            transform.position, 
            Quaternion.identity);
        Destroy(effect, 0.7f);
        Destroy(gameObject);
    }
}
