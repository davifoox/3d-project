using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMananger : MonoBehaviour
{
    
    void Awake(){

        if(PlayerPrefs.HasKey("HasDoneTutorial"))
        {
            SceneManager.LoadScene("World01");
            
        }

        else 
        {

            SceneManager.LoadScene("TutorialScene");
        }

        PlayerPrefs.SetString("HasDoneTutorial", "yes");
    }
}
