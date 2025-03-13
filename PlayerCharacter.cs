using System.Collections;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{   
    private int health;
    private int maxHealth = 5;
    private float regenDelay = 10f;
    private Coroutine regenCoroutine;

    void Start()
    {
        health = maxHealth; 
    }

    public void Hurt(int damage) 
    {
        health -= damage;
        Debug.Log($"Health: {health}");
        
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }
        regenCoroutine = StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(regenDelay);
        
        while (health < maxHealth)
        {
            health++;
            Debug.Log($"Regenerating... Health: {health}");
            yield return new WaitForSeconds(1f); // Adjust rate of regeneration
        }
    }
}

