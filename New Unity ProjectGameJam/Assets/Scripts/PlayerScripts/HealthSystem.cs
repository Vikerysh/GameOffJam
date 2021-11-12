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
            KillPlayer();
        } 
        
        else {
            //Set Invincibility Frames
            StartCoroutine(InvicibilityFrames());
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
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.ReloadScene();
    }
}