using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <author>Jason Kulp</author>
/// <summary>
/// Handles the logic for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "";

    /// <author>Jason Kulp</author>
    /// <summary>
    /// Method for the start button to load the game scene
    /// </summary>
    public void StartGame()
    {
        if(sceneToLoad != "" && sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    /// <author>Jason Kulp</author>
    /// <summary>
    /// Method for the quit button to quit the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
