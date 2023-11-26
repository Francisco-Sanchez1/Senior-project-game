using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ShopKeeperNPC : MonoBehaviour

{
    private DamageUpgrade damageUpgrade;
    public GameObject ShopMenu;
    public GameObject DamageupZap;
    public GameObject DamageupFire;
    public GameObject DamageupIce;
    public GameObject DamageupDark;
    public GameObject OutOfStock;
    public GameObject DialogueBox;
    public GameObject SoldOut1;
    public GameObject SoldOut2;
    public GameObject SoldOut3;
    public GameObject SoldOut4;
    public static bool ShopActive = false;
    public bool playerClose = false;
    private PlayerDataInitializer playerDataInitializer;




    public DamageUpgradeIce damageUpgradeIce;
    public DamageUpgradeDark damageUpgradeDark;
    public DamageUpgradeZap damageUpgradeZap;
    public DamageUpgradeFire damageUpgradeFire;

    void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();
        damageUpgradeIce = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeIce>();
        damageUpgradeDark = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeDark>();
        damageUpgradeZap = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeZap>();
        damageUpgradeFire = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeFire>();

    }
    void Update()
    {
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
        //ZAP
        if (playerDataInitializer.zap1Up == true)
        {

            SoldOut1.SetActive(true);
            DamageupZap.SetActive(false);

        }
        else if (playerDataInitializer.zap1Up == false)
        {
            SoldOut1.SetActive(false);
            DamageupZap.SetActive(true);
        }
        //ICE
        if (playerDataInitializer.ice1Up == true)
        {
            SoldOut2.SetActive(true);
            DamageupIce.SetActive(false);

        }
        else if (playerDataInitializer.ice1Up == false)
        {
            SoldOut2.SetActive(false);
            DamageupZap.SetActive(true);
        }
        //FIRE
        if (playerDataInitializer.fire1Up == true)
        {
            SoldOut3.SetActive(true);
            DamageupFire.SetActive(false);

        }


        else if (playerDataInitializer.fire1Up == false)
        {
            SoldOut3.SetActive(false);
            DamageupFire.SetActive(true);
        }

        //DARK
        if (playerDataInitializer.dark1Up == true)
        {
            SoldOut4.SetActive(true);
            DamageupDark.SetActive(false);

        }


        else if (playerDataInitializer.dark1Up == false)
        {
            SoldOut4.SetActive(false);
            DamageupDark.SetActive(true);
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