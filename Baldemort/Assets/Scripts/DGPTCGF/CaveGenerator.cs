using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class CaveGenerator : MonoBehaviour
{
    public static CaveGenerator Instance;
    public int width = 50;
    public int height = 50;
    public float fillPercentage = 0.45f;
    public float enemySpawnProbability = 0.1f; // Adjust the probability as needed

    public GameObject playerPrefab;
    public int numberOfEnemies;
    private GameObject PPlayer;


    public Sprite wallSprite;
    public Sprite openSpaceSprite;


    public GameObject doorPrefab;
    public GameObject enemyPrefab;

    public GameObject secretDoorPrefab;

    private GameObject secretDoor;


    [SerializeField]
private int enemiesKilled = 0;

    [SerializeField]
    private int enemiesRequiredForSecretDoor = 5;
    private int[,] map;

    void Start()
    {
         Instance = this;
        GenerateCave();
        VisualizeMap();
        SpawnEnemies();
        SpawnPlayer();
        SpawnDoor(PPlayer.transform.position);

        if (secretDoor == null)
        {
            secretDoor = Instantiate(secretDoorPrefab, new Vector3(-1, -1, 0), Quaternion.identity);
            secretDoor.SetActive(true);
        }

        CinemachineVirtualCamera Vcam = FindObjectOfType<CinemachineVirtualCamera>();

        // If the camera is found, set the Follow target to the player
        if (Vcam != null && playerPrefab != null)
        {
            Vcam.Follow = playerPrefab.transform;
        }
    }


    void GenerateCave()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }


    }

    void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1; // Ensure the border is filled
                }
                else
                {
                    map[x, y] = (Random.Range(0f, 1f) < fillPercentage) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int surroundingWalls = GetSurroundingWallCount(x, y);

                if (surroundingWalls > 4)
                {
                    map[x, y] = 1;
                }
                else if (surroundingWalls < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }




    void VisualizeMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    CreateSprite(wallSprite, new Vector3(x, y, 0), true);
                }
                else
                {
                    CreateSprite(openSpaceSprite, new Vector3(x, y, 0), false);
                }
            }
        }
    }

    void CreateSprite(Sprite sprite, Vector3 position, bool addCollider)
    {
        GameObject spriteObject = new GameObject();
        spriteObject.transform.position = position;

        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // Optionally add a BoxCollider2D component
        if (addCollider)
        {
            BoxCollider2D collider = spriteObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
        }
    }


    void SpawnEnemies()
{
    for (int i = 0; i < numberOfEnemies; i++)
    {
        Vector3 enemyPosition = GetRandomOpenSpacePosition();
        GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

        // Set the sorting layer and order in layer for the enemy
        SetSortingLayer(enemy, "Characters", 1); // Adjust order as needed

        // Do NOT increment the enemiesKilled count here
        // enemiesKilled++;
    }
}


    void SpawnPlayer()
    {
        Debug.Log("SpawnPlayer called");

        // Find a suitable location for the door and player
        Vector3 doorPosition = GetRandomOpenSpacePosition();
        Vector3 playerPosition = GetRandomOpenSpacePosition();

        // Check if the positions are suitable
        if (CheckSurroundingSpace((int)doorPosition.x, (int)doorPosition.y, 1, 1) && CheckSurroundingSpace((int)playerPosition.x, (int)playerPosition.y, 1, 1))
        {
            // Instantiate the playerPrefab
            GameObject playerObject = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            PPlayer = playerObject; // Set PPlayer to the instantiated playerObject

            // Spawn the door


            // Set CinemachineVirtualCamera properties
            CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (virtualCamera != null && playerObject != null)
            {
                virtualCamera.Priority = 10;
                virtualCamera.Follow = playerObject.transform;
                virtualCamera.LookAt = playerObject.transform;

                Debug.Log("Assigned Follow Target to Player");
            }
            else
            {
                // Log errors if CinemachineVirtualCamera or playerObject is not found
                if (virtualCamera == null)
                {
                    Debug.LogError("CinemachineVirtualCamera not found in the scene.");
                }
                if (playerObject == null)
                {
                    Debug.LogError("Player object not instantiated.");
                }
            }

            // Set the sorting layer and order in layer for the player
            SetSortingLayer(PPlayer, "Characters", 2);
            Debug.Log("Player position set to: " + playerPosition);
        }
        else
        {
            // If positions are not suitable, retry (you might want to add a limit to retries)
            SpawnPlayer();
        }
    }


    void SpawnDoor(Vector3 position)
    {
        // Instantiate the doorPrefab
        GameObject doorObject = Instantiate(doorPrefab, position, Quaternion.identity);

        // Set the sorting layer and order in layer for the door
        SetSortingLayer(doorObject, "Characters", 3); // Adjust order as needed
    }






    Vector3 GetRandomOpenSpacePosition()
    {
        List<Vector3> openSpacePositions = new List<Vector3>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0)
                {
                    bool hasEnoughSpace = CheckSurroundingSpace(x, y, 1, 1); // Adjust the distance as needed

                    if (hasEnoughSpace)
                    {
                        openSpacePositions.Add(new Vector3(x, y, 0));
                    }
                }
            }
        }

        if (openSpacePositions.Count > 0)
        {
            // Return a random position from the list
            Vector2 randomPosition = openSpacePositions[Random.Range(0, openSpacePositions.Count)];
            return new Vector3(randomPosition.x, randomPosition.y, 0);
        }
        else
        {
            // If no open space is found, return a default position within map boundaries
            return new Vector3(Random.Range(1, width - 1), Random.Range(1, height - 1), 0);
        }
    }

    bool CheckSurroundingSpace(int x, int y, int distanceX, int distanceY)
    {
        for (int i = x - distanceX; i <= x + distanceX; i++)
        {
            for (int j = y - distanceY; j <= y + distanceY; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (map[i, j] == 1)
                    {
                        return false; // There is a wall nearby
                    }
                }
                else
                {
                    return false; // Coordinates are outside the map
                }
            }
        }

        return true; // There is enough space
    }


    void SetSortingLayer(GameObject obj, string sortingLayerName, int orderInLayer)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = orderInLayer;
        }
    }



    Vector3 CalculateDoorPosition(Vector3 playerPosition)
    {
        // Example: Shift the door position 12 units to the right of the player
        return new Vector3(playerPosition.x + 12, playerPosition.y, playerPosition.z);
    }

    void Update()
{
    Debug.Log("Current enemies killed: " + enemiesKilled);

    // Check if the required number of enemies have been killed
    if (enemiesKilled >= enemiesRequiredForSecretDoor)
    {
        Debug.Log("Enemies killed: " + enemiesKilled);
        // Spawn the secret door at a specific location
        SpawnSecretDoor();

        // Reset the enemiesKilled count to avoid spawning multiple doors
        enemiesKilled = 0;
        Debug.Log("Enemies killed reset to 0");
    }
}

    void SpawnSecretDoor()
    {
        // Find a suitable open space for the secret door
        Vector3 secretDoorPosition = GetRandomOpenSpacePosition();

        // Deactivate the secret door initially
        secretDoor.SetActive(false);

        // Set the sorting layer and order in layer for the secret door
        SetSortingLayer(secretDoor, "Characters", 4); // Adjust order as needed

        // Set the position of the secret door
        secretDoor.transform.position = secretDoorPosition;

        // Activate the secret door
        secretDoor.SetActive(true);
    }

    public void EnemyKilled()
    {
        // Increment the enemiesKilled count
        enemiesKilled++;

        // Log the current enemiesKilled count for debugging
        Debug.Log("Enemies killed: " + enemiesKilled);

        // Check if the condition for spawning the secret door is met
        if (enemiesKilled >= enemiesRequiredForSecretDoor)
        {
            // Spawn the secret door at a specific location
            SpawnSecretDoor();

            // Reset the enemiesKilled count to avoid spawning multiple doors
            enemiesKilled = 0;
            Debug.Log("Enemies killed reset to 0");
        }
    }

}
