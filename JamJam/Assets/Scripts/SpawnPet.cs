using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPet : MonoBehaviour
{
    public Transform player;    // Reference to the player
    public Transform prefab;    // Prefab to spawn
    public float radius = 10f;  // Radius within which to spawn

    private bool hasSpawned = false; // Tracks if the object has been spawned

    void Update()
    {
        // Check if "E" is pressed and the object hasn't been spawned
        if (Input.GetKeyDown(KeyCode.E) && !hasSpawned)
        {
            SpawnTarget();
            hasSpawned = true; // Prevent further spawns
        }
    }

    void SpawnTarget()
    {
        // Generate a random position within the radius
        Vector3 position = new Vector3(
            player.position.x + Random.Range(-radius, radius),
            0f,
            player.position.z + Random.Range(-radius, radius)
        );

        // Adjust the position's Y-coordinate based on terrain height
        position.y = Terrain.activeTerrain.SampleHeight(position) + Terrain.activeTerrain.transform.position.y;

        // Instantiate the prefab at the calculated position
        Transform target = Instantiate(prefab, position, Quaternion.identity);

        // Adjust the position to account for the object's collider height
        target.position = new Vector3(
            target.position.x,
            target.position.y + target.GetComponent<Collider>().bounds.size.y / 2,
            target.position.z
        );
    }
}