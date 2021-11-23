using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameObject[] letters;

    public Image uiLetterT1;
    public Image uiLetterA;
    public Image uiLetterT2;
    public Image uiLetterU;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
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
        //criar aqui a lógica da quantidade de letras e condição de WIN
    }

    void UpdateLetterUI(string name)
    {
        if(name == "T1")
        {
            uiLetterT1.color = Color.white;
        }
        else if(name == "A")
        {
            uiLetterA.color = Color.white;
        }
        else if(name == "T2")
        {
            uiLetterT2.color = Color.white;
        }
        else if (name == "U")
        {
            uiLetterU.color = Color.white;
        }
    }
}
