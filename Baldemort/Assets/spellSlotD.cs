using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to access the UI components.

public class SpellSlotD : MonoBehaviour
{
    public Image uiImage; // Reference to the UI Image component you want to change.
    public Sprite spellSlotAImage; // Reference to the sprite you want to assign.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Assign the sprite to the UI Image component.
            uiImage.sprite = spellSlotAImage;
        }
    }
}