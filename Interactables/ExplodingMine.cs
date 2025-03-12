using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingMine : MonoBehaviour
{
    public GameObject explosionEffect;

    public float radius = 30f;
    public float explosionForce = 40f;

    private void OnTriggerEnter(Collider other)
    {
            // Check if the player triggered the mine, apply force
            CharacterController character = other.GetComponent<CharacterController>();
            if (character != null)
            {
                // Manually apply explosion force for CharacterController
                PlayerCharacter playerScript = character.GetComponent<PlayerCharacter>();
                if (playerScript != null)
                {
                    playerScript.ApplyExplosionForce(transform.position, explosionForce);
                }
            }
        Explode();
        Debug.Log("EXPLOSION");
     }

    private void Explode()
    {
        // Check nearby colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // add force to objects collided
        foreach (Collider collider in colliders)
        {

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);

            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}