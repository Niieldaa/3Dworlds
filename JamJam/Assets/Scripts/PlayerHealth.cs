using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    private int damage = 5;
    
    void OnTriggerEnter(Collider collision)
    {
        // Check if the collided object has the "Enemy" or "DamageDealer" tag
        if (collision.gameObject.CompareTag("Ghost"))
        {
            // Check if playerHealth is available, and apply damage
            if (health != null)
            {
                TakeDamage(damage);
            }
        }
    }

    public void Update()
    {
        print("Player health: " + health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if health is zero or below to handle player death
        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            // Add any additional logic for player death
            Destroy(gameObject);  // Destroy player object as an example
        }
    }
}