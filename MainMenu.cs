using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        // change gamescene to our game scene
        SceneManager.LoadScene("FPSGame"); 
    }

    // Update is called once per frame
     public void QuitGame()
    {
        // quit game
        Application.Quit();
        Debug.Log("Game Quit"); 
    }
}
