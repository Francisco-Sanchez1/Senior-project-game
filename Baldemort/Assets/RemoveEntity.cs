using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEntity : MonoBehaviour
{


    void Update()
    {
       

        Destroy(gameObject, 8f);
    }
}
