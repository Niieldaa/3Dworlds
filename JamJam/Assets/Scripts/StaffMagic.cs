using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);  // Apply damage to the target
            }
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

        }
    }
}