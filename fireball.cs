using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{   
    public float speed = 10.0f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        // Get a reference to the PlayerCharacter component, if there is one
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        // If player is not null, then the fireball has hit the player
        if (player != null) {
            Debug.Log("Player Hit!");
            player.Hurt(damage);
        }

        // Destroy game object
        Destroy(gameObject);
    }
}