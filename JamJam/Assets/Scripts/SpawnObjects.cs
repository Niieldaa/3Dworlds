using System.Collections;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefab; // Enemy prefab to spawn
    public Terrain terrain; // Reference to the terrain
    public Transform player; // Reference to the player's transform
    public float spawnRadius = 20f; // Radius around the player to spawn enemies
    public float yOffset = 0.5f; // Height offset for enemy placement
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public float spawnInterval = 10f; // Time between spawns
    public Collider triggerZone; // The trigger collider that starts spawning

    private int enemyCount;
    private Coroutine spawnCoroutine; // Reference to the running coroutine

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player") && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(EnemyDrop());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player") && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // Stop the spawning coroutine
            spawnCoroutine = null; // Clear the coroutine reference
        }
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < maxEnemies)
        {
            // Generate a random point within a circle around the player
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            float randX = player.position.x + randomPoint.x;
            float randZ = player.position.z + randomPoint.y;

            // Clamp the positions to ensure they're within terrain bounds
            // clamp is when its between minimum and maximum..
            randX = Mathf.Clamp(randX, terrain.transform.position.x, terrain.transform.position.x + terrain.terrainData.size.x);
            randZ = Mathf.Clamp(randZ, terrain.transform.position.z, terrain.transform.position.z + terrain.terrainData.size.z);

            // Get the terrain height at the generated position
            float yVal = terrain.SampleHeight(new Vector3(randX, 0, randZ)) + yOffset;

            // Spawn the enemy prefab
            Instantiate(prefab, new Vector3(randX, yVal, randZ), Quaternion.identity);

            enemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
