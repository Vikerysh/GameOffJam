using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    #region Singleton
    
    public static GameController instance;
    
    void Awake(){
        instance = this;
        SoundManager.Initialize();
    }
    #endregion
    public GameObject player;
    public Camera cam;

    public bool isPaused;
    public bool pauseMenuActive;

    public delegate void onGlitchChange();
    public onGlitchChange onGlitchChangeCallback;

    public bool canMove, canShoot;

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

    public void CanMove(bool x){
        canMove = x;
        onGlitchChangeCallback.Invoke();
    }
    public void CanShoot(bool x){
        canShoot = x;
        onGlitchChangeCallback.Invoke();
    }

    public SoundAudioClip[] soundAudioClipArray;
    public SoundAudioTrack[] soundAudioTrackArray;
    [System.Serializable]
    public class SoundAudioClip{
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    [System.Serializable]
    public class SoundAudioTrack{
        public SoundManager.Track track;
        public AudioClip audioClip;
    }

}
