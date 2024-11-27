using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Player's health
    private int damage = 10;  // Damage amount per collision
    [SerializeField] private UIManager uiManager; // Reference to UI Manager
    private bool canTakeDamage = true;  // To control damage frequency
    private float damageCooldown = 1f;  // Cooldown time between damage events (in seconds)

    
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Ghost" tag
        if (other.CompareTag("Ghost") && canTakeDamage)
        {
            // Apply damage
            TakeDamage(damage);

            // Start the cooldown to prevent taking damage too quickly
            Debug.Log("Ghost collided with player! Starting cooldown...");
            StartCoroutine(DamageCooldown());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        // Check if the collided object has the "Ghost" tag
        if (other.CompareTag("Ghost") && canTakeDamage)
        {
            // Apply damage
            TakeDamage(damage);

            // Start the cooldown to prevent taking damage too quickly
            Debug.Log("Ghost collided with player! Starting cooldown...");
            StartCoroutine(DamageCooldown());
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
            // Destroy(gameObject);
            // Load the Game Over scene
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public int GetHealth()  // Getter method to access health
    {
        return health;
    }

    void Update()
    {
        // Update the UI with the current health
        uiManager.AddHealth(health);
        Debug.Log("Player health: " + health);
    }

    // Coroutine for damage cooldown
    private IEnumerator DamageCooldown()
    {
        Debug.Log("Damage cooldown started...");
        canTakeDamage = false;  // Disable damage
        yield return new WaitForSeconds(damageCooldown);  // Wait for the cooldown period
        canTakeDamage = true;  // Re-enable damage
        Debug.Log("Damage cooldown ended.");
    }
}