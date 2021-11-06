using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;

        public HealthSystem(float health)
    {
        this.health = health;
        health = maxHealth;        
    }

    public float GetHealth()
    {
        return health; 
    }
    
   
    public void Damage( float damageAmount)
    {
        health -= damageAmount; 

        if(health<=0)
        {
            //trigger Death animation/ Game over screen/ reload game
        }
    }

    public void Heal(float healthAmount)
    {
        health += healthAmount;

        if(health>=maxHealth)
        {
            health = maxHealth;
        }
    }

}
