using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ShopButton", menuName = "ScriptableObjects/ShopButton")]
public class ShopItemSO : ScriptableObject
{ 

    public string itemName;
    public Sprite backgroundSprite;
    public Sprite iconSprite;
    public int price;
    public string description;
    public Sprite purchaseButtonSprite;
    public int itemCount;
}
