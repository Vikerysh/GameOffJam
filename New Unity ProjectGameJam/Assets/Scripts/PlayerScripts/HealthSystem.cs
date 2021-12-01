using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int maxHealth;
    [SerializeField]
    private float invincibilityDurationSeconds;

    bool isInvincible = false;
    [SerializeField] private float gameOverReloadTime;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Animator anim;

    public HealthSystem(int health)
    {
        this.health = health;
        health = maxHealth;        
    }

    public int GetHealth()
    {
        return health; 
    }
    
   
    public void Damage( int damageAmount)
    {
        //Stop function if we're invincible
        if(isInvincible) return;

        health -= damageAmount; 
        
        if(health<=0)
        {
            GameController.instance.canMove = false;
            GameController.instance.onGlitchChangeCallback;
            KillPlayer();
        } 
        
        else {
            //Set Invincibility Frames
            StartCoroutine(InvicibilityFrames());
            anim.SetTrigger("Hit");
            SoundManager.PlaySound(SoundManager.Sound.PlayerHit);
        }
    }

    public void Heal(int healthAmount)
    {
        health += healthAmount;

        if(health>=maxHealth)
        {
            health = maxHealth;
        }
    }

    private IEnumerator InvicibilityFrames()
    {
        //Player Becomes Invincible
        isInvincible = true; 

        yield return new WaitForSeconds(invincibilityDurationSeconds);
        //Player loses Invincibility
        isInvincible = false;
    }

    public void KillPlayer()
    {
        //dealwithdeath, anmimations, vfx, sfx etc
        //trigger gameoverscreen in certain situations? or always?

        ReloadScene(); //reloads the current scene in case we want to get the player back into action and avoid a gameoverscreen
    }
    
    public void ReloadScene()
    {
        GameOverPanel.SetActive(true);
        StartCoroutine(GameOverScreen());
    }

    private IEnumerator GameOverScreen()
    {      
        GameOverPanel.GetComponent<Animator>().SetBool("GameOverAnim", true);
        GameController.instance.cam.gameObject.GetComponent<FollowPlayer>().ToggleFollow(false);
            yield return new WaitForSeconds(2f);
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.ReloadScene();
    }
}
