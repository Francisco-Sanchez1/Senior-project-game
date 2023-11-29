using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public void FixedUpdate()
    {
        // Play explosion sound or particle effects if necessary

        // Destroy the bomb object
        Destroy(gameObject);
    }
}
