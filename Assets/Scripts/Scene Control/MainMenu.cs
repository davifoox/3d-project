using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialScene");

    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("TutorialScene");

    }
    public void PlayCredits()
    {
        SceneManager.LoadScene("Credits");

    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
