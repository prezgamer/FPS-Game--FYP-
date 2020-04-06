using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;

    float fireRate;
    public float startingFireRate =2f;

    public ParticleSystem muzzleFlash;

    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (fireRate <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                fireRate = startingFireRate;
            }
        } else if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play(); //show muzzle flash

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                DestructableObject DO = hit.collider.gameObject.GetComponent<DestructableObject>();

                if (DO != null)
                {
                    DO.TakeDamage(damage);
                    Debug.Log("Hit Enemy: " + DO.health + " Hp");
                }
            }
        }
    }
}
