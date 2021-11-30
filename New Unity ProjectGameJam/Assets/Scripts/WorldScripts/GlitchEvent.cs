using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEvent : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CircleCollider2D circleCollider2D;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            
            GameController.instance.canJump = false;
            GameController.instance.canCharge = true;
            GameController.instance.onGlitchChangeCallback();
            other.gameObject.GetComponent<GlitchController>().StartGlitch();
        }
        StartCoroutine(PowerUpCollected());
    }

    IEnumerator PowerUpCollected(){
        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
        SoundManager.PlayTrack(SoundManager.Track.PowerUpGet);
        yield return new WaitForSeconds(5f);
        SoundManager.PlayTrack(SoundManager.Track.PowerUpRoom);
        Destroy(gameObject);
    }

}
