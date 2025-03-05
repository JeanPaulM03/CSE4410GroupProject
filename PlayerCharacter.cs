using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{   
    // Varaible for the Player's health 
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 5; // Set Player's health
    }

    // Method to call to deal damage to the player 
    public void Hurt(int damage) {
        health -= damage;
        Debug.Log($"Health: {health}");
    }
}
