using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50;
    private UIManager uiManager;
    
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die(20);
        }
    }

    void Die(int points)
    {
        uiManager.AddScore(points);
        Destroy(gameObject);
    }
}
