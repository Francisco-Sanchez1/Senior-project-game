using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject archerSwarmPrefab;

    [SerializeField]
    private float swarmInterval = 3.5f;
    [SerializeField]
    private float archerSwarm = 10f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(swarmInterval, swarmerPrefab));
    }

    // Update is called once per frame
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
