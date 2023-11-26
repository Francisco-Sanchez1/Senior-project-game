using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject archerSwarmPrefab;

    [SerializeField]
    private float swarmInterval = 3.5f;
    [SerializeField]
    //private float archerSwarm = 10f;
    public float detectionRadius;
    public bool spawn;
    public float spawnTimer;
    public float spawnTimeFull = 5f;

    [SerializeField]
    private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(target.position, transform.position);

        if (distanceToPlayer < detectionRadius && canSpawn)
        {
            spawnEnemy(swarmerPrefab);
            canSpawn = false; // Prevent spawning until the timer resets
        }

        if (!canSpawn)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                canSpawn = true; // Allow spawning after the timer reaches zero
                spawnTimer = spawnTimeFull;
            }
        }
    }


    // Update is called once per frame
    private void spawnEnemy(GameObject enemy)
    {
        // Define the spawn radius around the EnemySpawner
        float spawnRadius = 1.5f; // You can adjust this value to your desired radius.

        // Generate random coordinates within the spawn radius
        float randomX = transform.position.x + Random.Range(-spawnRadius, spawnRadius);
        float randomY = transform.position.y + Random.Range(-spawnRadius, spawnRadius);

        // Create a new Vector3 with the random coordinates
        Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

        // Offset the enemy's position if needed
        // You can add/subtract values from the spawnPoint position as needed.

        GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        spawn = true;
    }


}
