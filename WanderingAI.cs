using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{   
    // Fields for the fireball prefab and current instance
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    // Speed of the wandering game object and how far to look for obstacles 
    public float normalSpeed = 3.0f;
    public float obstacleRange = 5.0f;

    // State of the game object 
    private bool isAlive;

    public const float baseSpeed = 3.0f;

    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value) {
        normalSpeed = baseSpeed * value; 
    }

    private void Start() {
        // Set the game object to alive
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward only if alive
        if (isAlive) {
            transform.Translate(0, 0, normalSpeed * Time.deltaTime);
        }

        // Create a ray from the wandering game object, pointed in the same direction as that game object 
        Ray ray = new Ray(transform.position, transform.forward);

        // Data container, contains hit information
        RaycastHit hit;

        // Perform a raycast with a circular volume around the ray 
        if (Physics.SphereCast(ray, 0.75f, out hit)) {
            // Get a reference to the game object that was hit by the sphere cast
            GameObject hitObject = hit.transform.gameObject;

            // If the object hit was the player, shoot a fireball
            // Otherwise, if it wasn't the player and it's within the obstacle range, turn around a random angle
            if (hitObject.GetComponent<PlayerCharacter>()) {
                if (fireball == null) {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            } else if (hit.distance < obstacleRange) {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    // Public method to set to alive by outside scripts 
    public void SetAlive(bool alive) {
        isAlive = alive;
    }
}
