using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxMana(float Mana)
    {
        slider.maxValue = Mana;
        slider.value = Mana;
    }

    public void SetMana(float Mana)
    {
        slider.value = Mana;
    }
}