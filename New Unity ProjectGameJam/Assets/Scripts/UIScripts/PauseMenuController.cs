using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseScreen;

    void Start()
    {
        DeactivatePauseScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                DeactivatePauseScreen();
            }
            else
            {
                ActivatePauseScreen();
            }
        }
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.SetActive(true);
        GameController.instance.pauseMenuActive = true;
        if (!GameController.instance.isPaused)
        {
            GameController.instance.PauseGame();
        }
    }

    public void DeactivatePauseScreen()
    {
        pauseScreen.SetActive(false);
        GameController.instance.pauseMenuActive = false;

        if (GameController.instance.isPaused)
        {
            GameController.instance.ResumeGame();
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
