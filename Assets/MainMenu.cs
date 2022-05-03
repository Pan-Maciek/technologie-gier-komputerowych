using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PressPlayButton()
    {
        // TODO Make sure "MainMenu" scene has an index of 0 and "Game" scene has an index of 1.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PressQuitButton()
    {
        Debug.Log("Triggered 'Quit'");
        Application.Quit();
    }
}
