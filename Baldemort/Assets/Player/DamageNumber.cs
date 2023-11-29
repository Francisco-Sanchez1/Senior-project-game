using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public GameObject canvas;
    public GameObject damageTextPrefab;

    public void ShowDamageNumber(float damageAmount)
    {
        GameObject damageTextInstance = Instantiate(damageTextPrefab, canvas.transform);
        TMP_Text damageText = damageTextInstance.GetComponent<TMP_Text>();
        damageText.text = damageAmount.ToString();
        Destroy(damageTextInstance, 1.0f); // Destroy the damage number after 1 second
    }
}
