using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allSpellSlot : MonoBehaviour
{
    [SerializeField]
    private GameObject spellSlotA;

    [SerializeField]
    private GameObject spellSlotB;

    [SerializeField]
    private GameObject spellSlotC;

    [SerializeField]
    private GameObject spellSlotD;

    // Your script logic here

    public Image uiImageA; // Reference to the UI Image component you want to change.
    public Image uiImageB;
    public Image uiImageC;
    public Image uiImageD;

    public Sprite spellSlotAImage;
    public Sprite spellSlotBImage;
    public Sprite spellSlotCImage;
    public Sprite spellSlotDImage;

    public Sprite emptySpell;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Assign the sprite to the UI Image component.
            uiImageA.sprite = spellSlotAImage;
            uiImageB.sprite = emptySpell;
            uiImageC.sprite = emptySpell;
            uiImageD.sprite = emptySpell;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Assign the sprite to the UI Image component.
            uiImageA.sprite = emptySpell;
            uiImageB.sprite = spellSlotBImage;
            uiImageC.sprite = emptySpell;
            uiImageD.sprite = emptySpell;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Assign the sprite to the UI Image component.
            uiImageA.sprite = emptySpell;
            uiImageB.sprite = emptySpell;
            uiImageC.sprite = spellSlotCImage;
            uiImageD.sprite = emptySpell;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Assign the sprite to the UI Image component.
            uiImageA.sprite = emptySpell;
            uiImageB.sprite = emptySpell;
            uiImageC.sprite = emptySpell;
            uiImageD.sprite = spellSlotDImage;
        }
    }
}
