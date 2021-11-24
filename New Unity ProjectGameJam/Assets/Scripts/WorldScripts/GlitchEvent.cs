using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEvent : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            
            GameController.instance.canMove = false;
            GameController.instance.canCharge = true;
            GameController.instance.onGlitchChangeCallback();
        }

    }
}