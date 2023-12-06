using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText; // Reference to the TextMeshProUGUI component

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        UpdateHealthText(); // Update the text whenever the maximum health changes
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        UpdateHealthText(); // Update the text whenever the current health changes
    }

    void UpdateHealthText()
    {
        // Display current health / max health as text
        healthText.text = $"{slider.value} / {slider.maxValue}";
    }
}

