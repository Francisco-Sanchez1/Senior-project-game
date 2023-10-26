using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ShopKeeperNPC : MonoBehaviour

{
    private DamageUpgrade damageUpgrade;
    public GameObject ShopMenu;
    public GameObject DamageupTier1;
    public GameObject OutOfStock;
    public GameObject DialogueBox;
    public GameObject SoldOut1;
    public static bool ShopActive = false;
    public bool playerClose = false;


    void Update()
    {
        damageUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgrade>();
        if (playerClose == true && Input.GetKeyDown(KeyCode.E))
        {
            ShopActive = true;
            ShopMenu.SetActive(true);
            DialogueBox.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerClose = false;
            ShopActive = false;
            ShopMenu.SetActive(false);
            DialogueBox.SetActive(false);
        }
        if (damageUpgrade.damageUpgraded == true)
        {
            SoldOut1.SetActive(true);
            DamageupTier1.SetActive(false);

        }
        else if(damageUpgrade.damageUpgraded == false)
        {
            SoldOut1.SetActive(false);
            DamageupTier1.SetActive(true);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose= true;
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = false;

        }

    }




}