using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string letterName;

    public delegate void LetterPicked(string name, GameObject gameObject);
    public event LetterPicked OnLetterPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnLetterPicked(letterName, this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
