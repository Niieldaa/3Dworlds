using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text healthText;  // Declare healthText to display player health

    private int health = 100;  // Set a starting value for health (e.g., 100)
    private int score = 0;  // Define the score variable

    private void Start()
    {
        // Initialize the score and health to 0 at the start
        UpdateScore(score, health);
    }

    // Update the UI text for score and health
    public void UpdateScore(int playerScore, int playerHealth)
    {
        scoreText.text = "Score: " + playerScore;
        healthText.text = "Health: " + playerHealth;
    }

    // Call this method to add score
    public void AddScore(int points)
    {
        score += points;  // Increase score by the given points
        UpdateScore(score, health);  // Update the score and health text
    }
    
    public void AddHealth(int health)
    {
        UpdateScore(score, health);  // Update the score and health text
    }
}