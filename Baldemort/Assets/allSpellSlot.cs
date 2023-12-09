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

    // Integer flags to check if spells are unlocked (0 for locked, 1 for unlocked)
    public int isSpellAUnlocked = 0;
    public int isSpellBUnlocked = 0;
    public int isSpellCUnlocked = 1;
    public int isSpellDUnlocked = 0;

    // selected spell represented as an integer
    // 0 for A, 1 for B, 2 for C, 3 for D
    public int selectedSpell;

    void Start()
    {
        LoadSpellData();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isSpellAUnlocked == 1)
        {
            selectedSpell = 0; // Spell A
            uiImageA.sprite = spellSlotAImage;
            uiImageB.sprite = emptySpellB;
            uiImageC.sprite = emptySpellC;
            uiImageD.sprite = emptySpellD;
            Debug.Log(selectedSpell);
            SaveSpellData();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isSpellBUnlocked == 1)
        {
            selectedSpell = 1; // Spell B
            uiImageA.sprite = emptySpellA;
            uiImageB.sprite = spellSlotBImage;
            uiImageC.sprite = emptySpellC;
            uiImageD.sprite = emptySpellD;
            Debug.Log(selectedSpell);
            SaveSpellData();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isSpellCUnlocked == 1)
        {
            selectedSpell = 2; // Spell C
            uiImageA.sprite = emptySpellA;
            uiImageB.sprite = emptySpellB;
            uiImageC.sprite = spellSlotCImage;
            uiImageD.sprite = emptySpellD;
            Debug.Log(selectedSpell);
            SaveSpellData();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isSpellDUnlocked == 1)
        {
            selectedSpell = 3; // Spell D
            uiImageA.sprite = emptySpellA;
            uiImageB.sprite = emptySpellB;
            uiImageC.sprite = emptySpellC;
            uiImageD.sprite = spellSlotDImage;
            Debug.Log(selectedSpell);
            SaveSpellData();
        }
    }

    public void SaveSpellData()
    {
        PlayerPrefs.SetInt("isSpellAUnlocked", isSpellAUnlocked);
        PlayerPrefs.SetInt("isSpellBUnlocked", isSpellBUnlocked);
        PlayerPrefs.SetInt("isSpellCUnlocked", isSpellCUnlocked);
        PlayerPrefs.SetInt("isSpellDUnlocked", isSpellDUnlocked);
        PlayerPrefs.SetInt("selectedSpell", selectedSpell);
        PlayerPrefs.Save();
    }

    public void LoadSpellData()
    {
        isSpellAUnlocked = PlayerPrefs.GetInt("isSpellAUnlocked", 1);
        isSpellBUnlocked = PlayerPrefs.GetInt("isSpellBUnlocked", 1);
        isSpellCUnlocked = PlayerPrefs.GetInt("isSpellCUnlocked", 1);
        isSpellDUnlocked = PlayerPrefs.GetInt("isSpellDUnlocked", 1);
        selectedSpell = PlayerPrefs.GetInt("selectedSpell", 2);

        UpdateSpellUI();
    }

    private void UpdateSpellUI()
    {
    uiImageA.sprite = (selectedSpell == 0 && isSpellAUnlocked == 1) ? spellSlotAImage : emptySpellA;
    uiImageB.sprite = (selectedSpell == 1 && isSpellBUnlocked == 1) ? spellSlotBImage : emptySpellB;
    uiImageC.sprite = (selectedSpell == 2 && isSpellCUnlocked == 1) ? spellSlotCImage : emptySpellC;
    uiImageD.sprite = (selectedSpell == 3 && isSpellDUnlocked == 1) ? spellSlotDImage : emptySpellD;
    }
}