using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public GameObject impactEffect;
    public float health = 50;
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject impactGO = Instantiate(impactEffect);
        Destroy(impactGO, 2f);
        Destroy(gameObject);
    }
}
