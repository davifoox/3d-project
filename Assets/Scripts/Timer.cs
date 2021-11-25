using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public TMP_Text timerText;
    public GameObject endGameUi;
   
    public bool isTheEnd;
    private PauseMenu pause;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0)
        {
            
            timeValue -= Time.deltaTime;
            

        }
        else
        {
            timeValue = 0;
               
        }
        if(timeValue <= 0)
        {
            GameIsEnd();

        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            
            timeToDisplay = 0;

        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void GameIsEnd()
    {

        endGameUi.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
            
    }
}
