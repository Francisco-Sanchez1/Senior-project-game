using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [Range(1, 100)] public int maxStackSize = 100;

}