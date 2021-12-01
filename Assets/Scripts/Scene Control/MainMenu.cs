using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    public Animator animator;
    public Image tFade;

    public void PlayGame()
    {
        SceneManager.LoadScene("World01");

    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("TutorialScene");

    }
    public void PlayCredits()
    {
        StartCoroutine(FadingCredits());

    }
    public void QuitGame()
    {
        Application.Quit();

    }


    IEnumerator FadingCredits()
    {

        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Credits");

    }
}
