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

    public Image uiImageA; // Reference to the UI Image component you want to change.
    public Image uiImageB;
    public Image uiImageC;
    public Image uiImageD;

    public Sprite spellSlotAImage;
    public Sprite spellSlotBImage;
    public Sprite spellSlotCImage;
    public Sprite spellSlotDImage;

    public Sprite emptySpellA;
    public Sprite emptySpellB;
    public Sprite emptySpellC;
    public Sprite emptySpellD;

    // Boolean flags to check if spells are unlocked
    public bool isSpellAUnlocked = false;
    public bool isSpellBUnlocked = false;
    public bool isSpellCUnlocked = false;
    public bool isSpellDUnlocked = false;

    // selected spell
    public enum SpellType { A, B, C, D };
    public SpellType selectedSpell;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isSpellAUnlocked)
    {
        // Only assign the sprite to the UI Image component if the spell is unlocked.
        selectedSpell = SpellType.A;
        uiImageA.sprite = spellSlotAImage;
        uiImageB.sprite = emptySpellB;
        uiImageC.sprite = emptySpellC;
        uiImageD.sprite = emptySpellD;
        Debug.Log(selectedSpell);
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow) && isSpellBUnlocked)
    {
        selectedSpell = SpellType.B;
        uiImageA.sprite = emptySpellA;
        uiImageB.sprite = spellSlotBImage;
        uiImageC.sprite = emptySpellC;
        uiImageD.sprite = emptySpellD;
        Debug.Log(selectedSpell);
    }
    if (Input.GetKeyDown(KeyCode.RightArrow) && isSpellCUnlocked)
    {
        selectedSpell = SpellType.C;
        uiImageA.sprite = emptySpellA;
        uiImageB.sprite = emptySpellB;
        uiImageC.sprite = spellSlotCImage;
        uiImageD.sprite = emptySpellD;
        Debug.Log(selectedSpell);
    }
    if (Input.GetKeyDown(KeyCode.DownArrow) && isSpellDUnlocked)
    {
        selectedSpell = SpellType.D;
        uiImageA.sprite = emptySpellA;
        uiImageB.sprite = emptySpellB;
        uiImageC.sprite = emptySpellC;
        uiImageD.sprite = spellSlotDImage;
        Debug.Log(selectedSpell);
    }
    }
}