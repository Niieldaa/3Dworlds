using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class StaffMagic : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    [SerializeField] private Camera fpsCam;
    public ParticleSystem flash;

    [SerializeField] private Animator anim;
    private bool shoot = false;

    public GameObject impactEffect;
    public AudioClip fireball;
    public GameObject rockEffect;
    public GameObject magicRockEffect;
    public GameObject woodEffect;

    public float spawnRadius = 10f;
    public GameObject lilBuddy;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Fire1 button is pressed down
        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(fireball, transform.position);
            shoot = true;  // Set shoot to true when firing starts
            anim.SetBool("shoot", true);  // Set the animator parameter to start the animation
            Shoot();
        }

        // Check if Fire1 button is released
        if (Input.GetButtonUp("Fire1"))
        {
            shoot = false;  // Set shoot to false when firing stops
            anim.SetBool("shoot", false);  // Reset the animation parameter
        }

        
        if (Input.GetButtonDown("Fire2"))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // Directly using Random.Range to test it works
        // float randomX = Random.Range(-spawnRadius, spawnRadius);
        // float randomZ = Random.Range(-spawnRadius, spawnRadius);

        // Print the generated X and Z values
        // Debug.Log($"Random X: {randomX}, Random Z: {randomZ}");

        // Create a random position and spawn the object
        //Vector3 randomPosition = new Vector3(randomX, player.position.y, randomZ) + player.position;
        //Instantiate(lilBuddy, randomPosition, Quaternion.identity);
    }


    void Shoot()
    {
        // Play the particle effect
        flash.Play();

        // Perform a raycast to detect hit
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Get the target component of the hit object
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                // Check the tag of the hit object and apply appropriate behavior
                if (hit.transform.CompareTag("Ghost"))
                {
                    target.TakeDamage(damage); // Apply damage to the target
                    GameObject magicRockGO =
                        Instantiate(magicRockEffect, hit.point,
                            Quaternion.LookRotation(hit.normal)); // Instantiate rock-specific effect
                    Destroy(magicRockGO, 2f); // Destroy effect after 2 seconds
                }

                if (hit.transform.CompareTag("Rock"))
                {
                    target.TakeDamage(damage); // Apply damage to the target
                    GameObject rockGO =
                        Instantiate(rockEffect, hit.point,
                            Quaternion.LookRotation(hit.normal)); // Instantiate rock-specific effect
                    Destroy(rockGO, 2f); // Destroy effect after 2 seconds
                }
                else if (hit.transform.CompareTag("Tree"))
                {
                    target.TakeDamage(damage); // Apply damage to the target
                    GameObject woodGO =
                        Instantiate(woodEffect, hit.point,
                            Quaternion.LookRotation(hit.normal)); // Instantiate wood-specific effect
                    Destroy(woodGO, 2f); // Destroy effect after 2 seconds
                }
                else
                {
                    // For any other object
                    target.TakeDamage(damage); // Apply damage to the target
                    GameObject genericImpactGO =
                        Instantiate(impactEffect, hit.point,
                            Quaternion.LookRotation(hit.normal)); // Generic impact effect
                    Destroy(genericImpactGO, 2f); // Destroy effect after 2 seconds
                }
            }
            else
            {
                // If the object hit doesn't have a target component (non-target object)
                GameObject impactGO =
                    Instantiate(impactEffect, hit.point,
                        Quaternion.LookRotation(hit.normal)); // Instantiate generic impact effect
                Destroy(impactGO, 2f); // Destroy effect after 2 seconds
            }
        }
    }
}