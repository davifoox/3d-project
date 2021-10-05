using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject[] letters;

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
