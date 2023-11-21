using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataInitializer : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxMana = 100f;
    public float currentMana;
    public int coins;

    public int minZap;
    public int maxZap;

    public int minIce;
    public int maxIce;

    public int minDark;
    public int maxDark;

    public int minFire;
    public int maxFire;

    public bool zap1Up;
    public bool fire1Up;
    public bool dark1Up;
    public bool ice1Up;

    
    public void Start()
    {

        // Ensure that this object is not destroyed when loading a new scene

        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load player data when the game starts THIS IS WHAT CAUSES THE CURRENT HEALTH TO NOT REDUCE CAN PROBABLY USE THIS FOR A SAVE GAME TYPE SCENARIO WHERE THE PLAYER PRESSES PLAY INSTEAD OF NEW GAME
        //LoadPlayerData();
    }


    private void LoadPlayerData()
    {
        PlayerCntrl player = FindObjectOfType<PlayerCntrl>();
        // Load current health if available, otherwise, use the default max health
        if (PlayerPrefs.HasKey("PlayerCurrentHealth"))
        {
            float savedHealth = PlayerPrefs.GetFloat("PlayerCurrentHealth");
            currentHealth = savedHealth;
            player.healthBar.SetHealth(savedHealth);
        }
        else
        {
            //currentHealth = maxHealth;
            //player.healthBar.SetHealth(maxHealth);
        }

        // Load current mana if available, otherwise, use the default max mana
        if (PlayerPrefs.HasKey("PlayerCurrentMana"))
        {
            float savedMana = PlayerPrefs.GetFloat("PlayerCurrentMana");
            currentMana = savedMana;
            player.manaBar.SetMana(savedMana);
        }
        else
        {
            //currentMana = maxMana;
            //player.manaBar.SetMana(maxMana);
        }
        if (PlayerPrefs.HasKey("PlayerCoins"))
        {
            int savedCoins = PlayerPrefs.GetInt("PlayerCoins");
            coins = savedCoins;


        }
        // Load other player data here, similar to health and mana
        //DAMAGE UPGRADE DATA

        //ZAP STUFF
        if (PlayerPrefs.HasKey("PlayerMinZap"))
        {
            float SavedMinZap = PlayerPrefs.GetFloat("PlayerMinZap");
            minZap = PlayerPrefs.GetInt("PlayerMinZap");
            maxZap = PlayerPrefs.GetInt("PlayerMaxZap");
        }

        if (PlayerPrefs.HasKey("PlayerZap1Up")){
            PlayerPrefs.SetInt("PlayerZap1Up", (zap1Up ? 1 : 0));
            zap1Up = (PlayerPrefs.GetInt("PlayerZap1Up") != 0);
        }


        //ICE STUFF
        if (PlayerPrefs.HasKey("PlayerMinIce"))
        {
            float SavedMinZap = PlayerPrefs.GetFloat("PlayerMinIce");
            minIce = PlayerPrefs.GetInt("PlayerMinIce");
            maxIce = PlayerPrefs.GetInt("PlayerMaxIce");
        }

        if (PlayerPrefs.HasKey("PlayerIce1Up"))
        {
            PlayerPrefs.SetInt("PlayerIce1Up", (ice1Up ? 1 : 0));
            ice1Up = (PlayerPrefs.GetInt("PlayerIce1Up") != 0);
        }

        //FIRE STUFF
        if (PlayerPrefs.HasKey("PlayerMinFire"))
        {
            float SavedMinZap = PlayerPrefs.GetFloat("PlayerMinFire");
            minFire = PlayerPrefs.GetInt("PlayerMinFire");
            maxFire = PlayerPrefs.GetInt("PlayerMaxFire");
        }
        if (PlayerPrefs.HasKey("PlayerFire1Up"))
        {
            PlayerPrefs.SetInt("PlayerFire1Up", (fire1Up ? 1 : 0));
            fire1Up = (PlayerPrefs.GetInt("PlayerFire1Up") != 0);
        }

        //DARK STUFF
        if (PlayerPrefs.HasKey("PlayerMinDark"))
        {
            float SavedMinZap = PlayerPrefs.GetFloat("PlayerMinDark");
            minDark = PlayerPrefs.GetInt("PlayerMinDark");
            maxDark = PlayerPrefs.GetInt("PlayerMaxDark");
        }
        if (PlayerPrefs.HasKey("PlayerDark1Up"))
        {
            PlayerPrefs.SetInt("PlayerDark1Up", (dark1Up ? 1 : 0));
            dark1Up = (PlayerPrefs.GetInt("PlayerDark1Up") != 0);
        }

    }

    public void DeadEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            int isEnemyDead = PlayerPrefs.GetInt(enemy.name + "_isEnemyDead", 0);

            if (isEnemyDead == 1)
            {
                enemy.SetActive(false);
                Debug.Log(enemy.name + " is set as dead.");
            }
        }
    }

    public void LoadSpawnerStates()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Breakable");
        Debug.Log("List of spawners! " + spawners);
        foreach (GameObject spawner in spawners)
        {
            int isDead = PlayerPrefs.GetInt(spawner.name + "_IsDead", 0);

            if (isDead == 1)
            {
                spawner.SetActive(false);
                Debug.Log(spawner.name + " is set as dead.");
            }
        }
    }

    public void ResetSavedData()
    {
        // Reset all player-related saved data to their default values
        PlayerPrefs.DeleteAll(); // This clears all PlayerPrefs

        // You might also reset the variables in this script to their initial values
        currentHealth = maxHealth;
        currentMana = maxMana;
        // Reset other variables to default values
    }

    // You can also add a method to save the player data if necessary
    private void SavePlayerData()
      {
          PlayerCntrl player = FindObjectOfType<PlayerCntrl>();
          if (player != null)
          {
              PlayerPrefs.SetFloat("PlayerCurrentHealth", currentHealth);
              PlayerPrefs.SetFloat("PlayerCurrentMana", currentMana);
              PlayerPrefs.SetInt("PlayerCoins", player.coins);
              // Save other player data as needed

              // Store the max health and max mana values
              PlayerPrefs.SetFloat("PlayerMaxHealth", maxHealth);
              PlayerPrefs.SetFloat("PlayerMaxMana", maxMana);

            PlayerPrefs.SetInt("PlayerMinZap", minZap);
            PlayerPrefs.SetInt("PlayerMaxZap", maxZap);

            PlayerPrefs.SetInt("PlayerMinIce", minIce);
            PlayerPrefs.SetInt("PlayerMaxIce", maxIce);

            PlayerPrefs.SetInt("PlayerMinFire", minFire);
            PlayerPrefs.SetInt("PlayerMaxFire", maxFire);

            PlayerPrefs.SetInt("PlayerMinDark", minDark);
            PlayerPrefs.SetInt("PlayerMaxDark", maxDark);
            PlayerPrefs.Save();
          }
      }
  
    private void OnApplicationQuit()
    {
        // Save the player data when the application is closed
        //SavePlayerData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // Save the player data when the application is paused (e.g., on mobile)
            //SavePlayerData();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Initialize player data when a new scene is loaded
        Debug.Log("I am a loading scene!");
        LoadPlayerData();
        LoadSpawnerStates();
        DeadEnemy();
    }
}
