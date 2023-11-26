using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        // Check if the current health value exceeds the new maximum health
        if (slider.value > health)
        {
            slider.value = health;
        }
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}

