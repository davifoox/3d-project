using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelTutorial : MonoBehaviour
{
    private GameObject[] letters;

    public Image uiLetterT1;
    public Image uiLetterA;
    public Image uiLetterT2;
    public Image uiLetterU;

    [Header("Fade")]

    public Animator animator;
    public Image tFade;

    

    

    private void Update()
    {
       /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    */
    }

    private void OnEnable()
    {
        if (letters == null)
            letters = GameObject.FindGameObjectsWithTag("Letter");

        foreach (GameObject l in letters)
        {
            l.GetComponent<Letter>().OnLetterPicked += PickLetter;
        }
    }

    private void OnDisable()
    {
        if (letters == null)
            letters = GameObject.FindGameObjectsWithTag("Letter");

        foreach (GameObject l in letters)
        {
            if(l != null)
                l.GetComponent<Letter>().OnLetterPicked -= PickLetter;
        }
    }

    void PickLetter(string name, GameObject letterGameObject)
    {
        letterGameObject.GetComponent<Letter>().OnLetterPicked -= PickLetter;
        Debug.Log("PEGOU LETRA " + name);
        UpdateLetterUI(name);
    }

    void UpdateLetterUI(string name)
    {
        if(name == "T1")
            uiLetterT1.color = Color.white;
        else if(name == "A")
            uiLetterA.color = Color.white;
        else if(name == "T2")
            uiLetterT2.color = Color.white;
        else if (name == "U")
            uiLetterU.color = Color.white;

        if(uiLetterT1.color == Color.white && uiLetterA.color == Color.white
            && uiLetterT2.color == Color.white && uiLetterU.color == Color.white)
        {
            
            
            StartCoroutine(NextLevelFade());
            Debug.Log("Pr�ximo n�vel!");
        }
    }

    IEnumerator NextLevelFade()
    {
        
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

    }

    public void NextLevelCall()
    {

        


    }
}
