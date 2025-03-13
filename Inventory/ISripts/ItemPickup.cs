using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;



    void Pickup()
    {
        if (Item != null)
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("ItemPickup: Item is null!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { } // Ensure the player has the "Player" tag
        {
            Pickup();
        }
    }
}

