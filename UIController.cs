using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{   
    [SerializeField] TMP_Text scoreLabel; // Reference to the UI text element that displays the player's score
    [SerializeField] SettingsPopup settingsPopup; // Reference to the settings popup window

    private int score; // Tracks the player's current score

    // Method for when the object becomes enabled and active
    private void OnEnable() {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    // Method for when the object becomes disabled and inactive
    private void OnDisable() {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    // Event handler for when an enemy is hit
    private void OnEnemyHit() {
        score += 1;
        scoreLabel.text = score.ToString();
    }

    // Sets the initial score to zero and closes any open settings popup
    private void Start() {
        score = 0;
        scoreLabel.text = score.ToString();

        settingsPopup.Close();
    }

    // Update is called once per frame
    void Update() {
       // scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSetting() {
        // Debug.Log("Opening Settings...");
        settingsPopup.Open();
    }
}
