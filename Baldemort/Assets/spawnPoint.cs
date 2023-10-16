using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public static Vector3 playerSpawnPoint;

    private void Start()
    {
        // Store the spawn point when the scene loads
        playerSpawnPoint = transform.position;
    }
}