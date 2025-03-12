using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int health;

    public CharacterController controller;
    private Vector3 impact;

    // Start is called before the first frame upddate
    void Start()
    {
        health = 5;
    }
    
    void Update()
    {
        if (impact.magnitude > 0.2f) // Ensure force is applied smoothly
        {
            controller.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime); // Gradually reduce force
        }
    }

    public void Hurt(int damage) {
        health -= damage;
        Debug.Log($"Health: {health}");
    }

    public void ApplyExplosionForce(Vector3 explosionPosition, float force)
    {
        Vector3 direction = transform.position - explosionPosition; // Get direction from explosion
        direction.y = 0.5f; // Add upward push
        impact += direction.normalized * force;
    }
}
