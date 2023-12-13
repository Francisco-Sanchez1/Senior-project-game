using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI ManaText; // Reference to the TextMeshProUGUI component

    public void SetMaxMana(float Mana)
    {
        slider.maxValue = Mana;
        UpdateManaText();
    }

    public void SetMana(float Mana)
    {
        slider.value = Mana;
        UpdateManaText();
    }

    void UpdateManaText()
    {
        ManaText.text = $"{slider.value} / {slider.maxValue}";
    }


}

