using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRoom : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            SoundManager.PlayTrack(SoundManager.Track.PowerUpRoom);
        }
    }
    public void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            SoundManager.PlayTrack(SoundManager.Track.AmbientUnderworld);
        }
    }
}
