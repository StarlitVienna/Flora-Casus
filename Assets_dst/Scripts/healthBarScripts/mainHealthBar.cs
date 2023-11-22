using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainHealthBar : MonoBehaviour
{
    public PlayerController player;
    public Slider slider;


    public void setMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }
    public void setHealth(float health)
    {
        slider.value = health;
    }
}
