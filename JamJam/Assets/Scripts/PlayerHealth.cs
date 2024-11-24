using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Player's health
    private int damage = 10;
    [SerializeField] private UIManager uiManager;

    void OnTriggerStay(Collider collision)
    {
        // Check if the collided object has the "Ghost" tag
        if (collision.gameObject.CompareTag("Ghost"))
        {
            // Apply damage
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if health is zero or below
        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            // Handle player death, e.g., destroy the player object
            Destroy(gameObject);
        }
    }

    public int GetHealth()  // Getter method to access health
    {
        return health;
    }

    public void Update()
    {
        uiManager.AddHealth(health);
        print("Player health: " + health);
    }
}