using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int _coins;
    private int _savedCoins;
    private string _playerPrefsKey = "coins";

    private int _buttonNo;
    private bool _infoButtonIsOpen = false;

    public TMP_Text _coinUI;
    public Button _purchaseButton;
    public InventorySlotSO[] _inventorySlotsSO;
    public ShopItemSO[] _shopItemsSO;
    public GameObject[] _shopPanlesGO;
    public ShopTemplate[] _shopPanels;
    public InventoryManager _inventoryManager;



    void Awake()
    {
        //Load saved money before game starts.
        _savedCoins = PlayerPrefs.GetInt(_playerPrefsKey);
        _coins = _savedCoins;
    }

    void Start()
    {
        //Activate all shop panels which we managed in editor.
        for (int i = 0; i < _shopItemsSO.Length; i++)
            _shopPanlesGO[i].SetActive(true);

        _coinUI.text = _savedCoins.ToString();
        LoadPanels();
        CheckPurchaseable();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reset();
    }


    // Set purchase button text.
    public void OpenPurchaseButton(int buttonNo)
    {
        if (_infoButtonIsOpen == false)
        {
            _purchaseButton.GetComponentInChildren<TMP_Text>().text = _shopItemsSO[buttonNo].price.ToString();
            _purchaseButton.GetComponent<Image>().sprite = _shopItemsSO[buttonNo].purchaseButtonSprite;
            _buttonNo = buttonNo;
            CheckPurchaseable();

        }
    }


    // Set info text.
    public void OpenInfoButton(int infoButtonNo)
    {

        if (_infoButtonIsOpen == false)
        {
            _shopPanels[infoButtonNo].infoImg.SetActive(true);
            _infoButtonIsOpen = true;
        }
        else if (_infoButtonIsOpen == true && _shopPanels[infoButtonNo].infoImg.activeSelf == true)
        {
            _shopPanels[infoButtonNo].infoImg.SetActive(false);
            _infoButtonIsOpen = false;
        }

    }


    // Decrease coins by price of item and update UI.
    public void PurchaseItem()
    {
        if (_coins >= _shopItemsSO[_buttonNo].price)
        {
            _coins = _coins - _shopItemsSO[_buttonNo].price;
            PlayerPrefs.SetInt(_playerPrefsKey, _coins);
            _savedCoins = PlayerPrefs.GetInt(_playerPrefsKey);
            _coinUI.text = _savedCoins.ToString();

            _shopItemsSO[_buttonNo].itemCount--;
            _shopPanels[_buttonNo].itemCountTxt.text = _shopItemsSO[_buttonNo].itemCount.ToString();

           

            for (int i = 0; i < _inventorySlotsSO.Length; i++)
            {

                if (_inventorySlotsSO[i].itemName == "")
                {

                    _inventorySlotsSO[i].itemCount = 1;
                    _inventorySlotsSO[i].itemName = _shopItemsSO[_buttonNo].itemName;
                    _inventorySlotsSO[i].iconSprite = _shopItemsSO[_buttonNo].iconSprite;
                    _inventorySlotsSO[i].background = _shopItemsSO[_buttonNo].backgroundSprite;
                    _inventoryManager.LoadPanels();

                    break;
                }
                else
                {

                    if (_inventorySlotsSO[i].itemName == _shopItemsSO[_buttonNo].itemName)
                    {

                        _inventorySlotsSO[i].itemCount++;
                        _inventoryManager.LoadPanels();
                        break;
                    }
                }
            }
            CheckPurchaseable();
        }

    }



    //Check our money amount and enable/disable purchase button.
    public void CheckPurchaseable()
    {

        if (_coins >= _shopItemsSO[_buttonNo].price && _shopItemsSO[_buttonNo].itemCount > 0)
            _purchaseButton.interactable = true;
        else
            _purchaseButton.interactable = false;

    }


    // Populates shop panels with data from corresponding shop items, updating name, item count, icon, and background sprite.
    public void LoadPanels()
    {
        for (int i = 0; i < _shopItemsSO.Length; i++)
        {
            _shopPanels[i].nameTxt.text = _shopItemsSO[i].itemName;
            _shopPanels[i].itemCountTxt.text = _shopItemsSO[i].itemCount.ToString();
            _shopPanels[i].iconImg.GetComponentInChildren<Image>().sprite = _shopItemsSO[i].iconSprite;
            _shopPanels[i].backgroundImg.GetComponent<Button>().GetComponent<Image>().sprite = _shopItemsSO[i].backgroundSprite;
            _shopPanels[i].descriptionTxt.text = _shopItemsSO[i].description;
        }

    }


    //Reset Money and write to screen.
    public void Reset()
    {
        _coins = 10000;
        PlayerPrefs.SetInt(_playerPrefsKey, _coins);
        _savedCoins = PlayerPrefs.GetInt(_playerPrefsKey);
        _coinUI.text = _savedCoins.ToString();
        CheckPurchaseable();
    }


}
