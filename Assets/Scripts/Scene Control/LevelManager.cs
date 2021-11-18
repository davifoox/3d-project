using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject[] letters;

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
        //criar aqui a lógica da quantidade de letras e condição de WIN
    }
}
