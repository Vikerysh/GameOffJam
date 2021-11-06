using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;

    public Image HealthDisplay;
    public Slider Slider;


    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }

    void Update()
    {
        float fillValue = healthSystem.health / healthSystem.maxHealth;
        Slider.value = fillValue;
    }

    public void SetHealth(float health, float maxHealth)
    {
        Slider.value = health;
        Slider.maxValue = maxHealth;
    }
}
