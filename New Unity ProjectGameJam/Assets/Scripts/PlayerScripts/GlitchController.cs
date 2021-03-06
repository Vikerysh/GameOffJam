using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlitchController : MonoBehaviour
{
    int collected;
    public int totalToCollect;
    public bool glichActive = false;
    TMP_Text text;

    void Start()
    {
        text = GameController.instance.popUpText;
    }
    // Update is called once per frame
    void Update()
    {
        if(glichActive){
            if(collected >= totalToCollect){
                RestoreGlitches();
            }
        }
        
    }

    public void StartGlitch(){
        glichActive = true;
    }
    public void EnergyCollected(int value){
        collected += value;
    }
    void RestoreGlitches(){

        GameController.instance.canJump = true;
        GameController.instance.canCharge = true;
        GameController.instance.onGlitchChangeCallback();
        glichActive = false;
        StartCoroutine(CompleteEvent());

    }

    IEnumerator CompleteEvent(){
        text.text = "Overload restored, J.U.M.P restored!";
        SoundManager.PlayTrack(SoundManager.Track.PowerUpGet);
        yield return new WaitForSeconds(5f);
        SoundManager.PlayTrack(SoundManager.Track.AmbientUnderworld);
        text.text = "";
    }

}
