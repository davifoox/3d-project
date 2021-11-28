using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public  bool gameIsPaused = false;
    private Timer timer;
    public GameObject pauseMenuUI, optionsMenuUI;
    
    
    [SerializeField] private bool otherMenus;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Input.GetKeyDown(KeyCode.Escape) && !optionsMenuUI.activeInHierarchy)
        {
            if(gameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

            }
            
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;

    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0;

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

    }
    public void Quit()
    {
        Application.Quit();

    }
    public void PlayAgain()
    {
       
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

    }
}
