using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndArea : MonoBehaviour
{
    private FollowPlayer followPlayer;

    void Start(){
        followPlayer = GameController.instance.cam.gameObject.GetComponent<FollowPlayer>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            followPlayer.ToggleFollow(false);
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            followPlayer.ToggleFollow(true);
        }
    }
}
