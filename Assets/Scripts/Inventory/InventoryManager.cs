using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public InventorySlotSO[] _inventorySlotsSO;
    public GameObject[] _inventoryPanelsGO;
    public InventoryTemplate[] _inventoryPanels;



    void Start()
    {
        //Activate all shop panels which we managed in editor.
        for (int i = 0; i < _inventorySlotsSO.Length; i++)
            _inventoryPanelsGO[i].SetActive(true);

        LoadPanels();
    }
    
    public void LoadPanels()
    {
        for (int i = 0; i < _inventorySlotsSO.Length; i++)
        {            
                if (_inventorySlotsSO[i].itemName != "")
                {
                    _inventoryPanels[i].nameTxt.text = _inventorySlotsSO[i].itemName;
                    _inventoryPanels[i].itemCountTxt.text = _inventorySlotsSO[i].itemCount.ToString();
                    _inventoryPanels[i].iconImg.GetComponentInChildren<Image>().sprite = _inventorySlotsSO[i].iconSprite;
                    _inventoryPanels[i].backgroundImg.GetComponent<Image>().sprite = _inventorySlotsSO[i].background;
                }
        }

    }
}
