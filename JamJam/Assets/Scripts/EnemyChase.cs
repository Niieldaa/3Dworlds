using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float chaseDistance = 10f; // Distance within which the enemy will start chasing the player
    public float moveSpeed = 5f; // Speed at which the enemy will move

    private void Update()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the chase distance, move the enemy towards the player
        if (distanceToPlayer < chaseDistance)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Move the enemy towards the player
        Vector3 direction = (player.position - transform.position).normalized; // Get direction towards the player
        transform.position += direction * moveSpeed * Time.deltaTime; // Move in that direction
    }
}