using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public TMP_Text healthText;

    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = health + "/" + slider.maxValue;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
