using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundSwitch : MonoBehaviour
{
    public bool OpeningScene;
    // Start is called before the first frame update
    void Start()
    {
        if(OpeningScene){
            SoundManager.PlayTrack(SoundManager.Track.EnergeticOverworld);
        }else {
            SoundManager.PlayTrack(SoundManager.Track.AmbientOverworld);
        }
    }

}
