using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMagic : MonoBehaviour
{

    public float damage = 10f;

    public float range = 100f;

    [SerializeField] private Camera fpsCam;
    public ParticleSystem flash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        flash.Play();
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
        {
            Debug.Log(Hit.transform.name);

            Target target = Hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        
        
    }
}
