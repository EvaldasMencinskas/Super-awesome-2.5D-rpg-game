using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMeniu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMeniuUi;
    public GameObject optionsMeniuPaused;

    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMeniuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        
        optionsMeniuPaused.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    void Pause()
    {
        pauseMeniuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (pauseMeniuUi == false)
        {
            if(GameIsPaused == true)
            {
                optionsMeniuPaused.SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
            
        }
        
    }
    void PauseOptions()
    {



    }
    public void LoadMeniu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMeniu");
        
    }
    

    public void QuitGame()
    {
        Application.Quit();
        
    }
}
