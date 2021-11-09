using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    #region Singleton
    
    public static GameController instance;
    
    void Awake(){
        instance = this;
    }
    #endregion
    public GameObject player;
    public Camera cam;

    public bool isPaused;
    public bool pauseMenuActive;

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

}
