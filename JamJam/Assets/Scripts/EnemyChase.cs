using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float chaseDistance = 10f; // Distance within which the enemy will start chasing the player
    public float moveSpeed = 5f; // Speed at which the enemy will move
    public int damageAmount = 10;
    private PlayerHealth playerHealth;
    [SerializeField] private AudioClip[] Clips; // Array of audio clips for random sounds
    private AudioSource audioSource; // Reference to the enemy's AudioSource component

    private void Start()
    {
        // Find the player's transform dynamically by tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        
        player = playerObject.transform;
        
        // Get the AudioSource component attached to the enemy
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            // If no AudioSource is found, log an error
            Debug.LogError("No AudioSource found on " + gameObject.name);
        }
    }

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

    private void ChasePlayer()
    {
        // Move the enemy towards the player :)
        Vector3 direction = (player.position - transform.position).normalized; // Get direction towards the player
        transform.position += direction * moveSpeed * Time.deltaTime; // Move in that direction

        // Randomly select a clip from the array
        var randomClip = Clips[Random.Range(0, Clips.Length)];

        // Play the randomly selected audio clip on the enemy's AudioSource
        if (audioSource != null && !audioSource.isPlaying) // Make sure the audio source isn't already playing
        {
            audioSource.clip = randomClip; // Set the selected clip
            audioSource.Play(); // Play the audio clip
        }
    }
}
