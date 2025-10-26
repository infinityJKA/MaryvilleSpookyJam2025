using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Epic Awesome Custom Stuff/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public String itemName;
    public Sprite sprite;

    [Header("Place puzzle stuff")]
    public bool canPlaced = false;
    public GameObject objectWhenPlaced;
    public String placedInteractLine;

}