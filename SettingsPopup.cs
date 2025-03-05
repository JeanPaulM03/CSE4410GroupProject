using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    // Method to open the UI pop-up
    public void Open() {
        gameObject.SetActive(true);
    }

    // Method to close the UI pop-up
    public void Close() {
        gameObject.SetActive(false);
    }

    // Handles the submission of the player's name from the UI
    public void OnSubmitName(string name) {
        Debug.Log(name);
    }

    // Processes changes to the game speed setting and broadcasts the change to other components
    public void OnSpeedValue(float speed) {
        Debug.Log($"Speed: {speed}");
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
}
