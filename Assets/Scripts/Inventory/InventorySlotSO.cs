using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventorySlot", menuName = "ScriptableObjects/InventorySlot")]
public class InventorySlotSO : ScriptableObject
{
    public string itemName;
    public int slotNumber;
    public Sprite iconSprite;
    public Sprite background;
    public int itemCount;
}
