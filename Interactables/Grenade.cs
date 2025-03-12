using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect; // explosion effect from prefab
    public float delay = 3f;

    public float explosionForce = 20f;
    public float radius = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Explode), delay);
    }
    
    private void Explode()
    {
        // Check nearby colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // add force to objects collided
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            CharacterController characterController = collider.GetComponent<CharacterController>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
        //Destroy(explosionEffect);
    }
}
