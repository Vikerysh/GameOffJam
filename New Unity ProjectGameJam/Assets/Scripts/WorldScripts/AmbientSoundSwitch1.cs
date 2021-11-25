using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundSwitch1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayTrack(SoundManager.Track.AmbientUnderworld);
    }

}
