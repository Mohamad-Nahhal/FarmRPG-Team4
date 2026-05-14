using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmShopUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private PlayerGold _playerGold;
    [SerializeField] private ShopHours _shopHours;

    [Header("UI")]
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private ShopItemUI _shopItemPrefab;
    [SerializeField] private GameObject _buyInterfacePanel;
    [SerializeField] private GameObject _mainShopMenu;
    [SerializeField] private GameObject _sellInterfacePanel;
    [SerializeField] private Transform _sellContentParent;
    [SerializeField] private Button _sellBackButton;
    [SerializeField] private Button _buyBackButton;

    [Header("Shop Settings")]
    [SerializeField] private KeyCode _openShopKey = KeyCode.P;
    [SerializeField] private List<ShopItemData> _shopItems = new List<ShopItemData>();

    private bool _isOpen;
    private bool _isBuyMode = true;

    private void Awake()
    {
        if (_inventorySystem == null)
            _inventorySystem = FindAnyObjectByType<InventorySystem>();

        if (_playerGold == null)
            _playerGold = FindAnyObjectByType<PlayerGold>();

        if (_shopHours == null)
            _shopHours = FindAnyObjectByType<ShopHours>();

        _buyButton.onClick.AddListener(OpenBuyInterface);
        _sellButton.onClick.AddListener(OpenSellInterface);
        _closeButton.onClick.AddListener(CloseShop);
        _sellBackButton.onClick.AddListener(BackToMainMenu);
        _buyBackButton.onClick.AddListener(BackToMainMenu);

        CloseShop();
    }

    private void OnEnable()
    {
        if (_playerGold != null)
            _playerGold.GoldChanged += RefreshUI;

        if (_inventorySystem != null)
            _inventorySystem.InventoryChanged += RefreshUI;
    }

    private void OnDisable()
    {
        if (_playerGold != null)
            _playerGold.GoldChanged -= RefreshUI;

        if (_inventorySystem != null)
            _inventorySystem.InventoryChanged -= RefreshUI;
    }

    private void Update()
    {
        if (_isOpen && _shopHours != null && !_shopHours.IsOpen)
        {
            CloseShop();
            return;
        }

        if (Input.GetKeyDown(_openShopKey))
        {
            if (_isOpen)
            {
                CloseShop();
            }
            else
            {
                if (_shopHours != null && !_shopHours.IsOpen)
                {
                    return;
                }

                OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        _isOpen = true;
        _shopPanel.SetActive(true);

        _mainShopMenu.SetActive(true);
        _buyInterfacePanel.SetActive(false);
        _sellInterfacePanel.SetActive(false);

        RefreshUI();
    }

    public void CloseShop()
    {
        _isOpen = false;
        _shopPanel.SetActive(false);

        _mainShopMenu.SetActive(true);
        _buyInterfacePanel.SetActive(false);
        _sellInterfacePanel.SetActive(false);
    }

    private void ShowBuyTab()
    {
        _isBuyMode = true;
        RefreshUI();
    }

    private void ShowSellTab()
    {
        _isBuyMode = false;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (_goldText != null && _playerGold != null)
            _goldText.text = "Gold: " + _playerGold.Gold;

        ClearContent();

        if (_isBuyMode)
            ShowBuyItems();
        else
            ShowSellItems();
    }

    private void ShowBuyItems()
    {
        foreach (ShopItemData shopItem in _shopItems)
        {
            if (shopItem == null || shopItem.Item == null)
                continue;

            ShopItemUI itemUI = Instantiate(_shopItemPrefab, _contentParent);

            itemUI.Setup(
                shopItem.Item,
                shopItem.BuyPrice + " Gold",
                "Buy",
                () => BuyItem(shopItem)
            );
        }
    }

    private void ShowSellItems()
    {
        ClearSellContent();

        if (_inventorySystem == null)
            return;

        IReadOnlyList<InventorySlotData> slots = _inventorySystem.Slots;

        foreach (InventorySlotData slot in slots)
        {
            if (slot == null || slot.Item == null || slot.Quantity <= 0)
                continue;

            ShopItemData shopItem = FindShopItem(slot.Item);

            if (shopItem == null)
                continue;

            ShopItemUI itemUI = Instantiate(_shopItemPrefab, _sellContentParent);

            itemUI.Setup(
                slot.Item,
                "Owned: " + slot.Quantity + " | Sell: " + shopItem.SellPrice + " Gold",
                "Sell",
                () => SellItem(shopItem)
            );
        }
    }

    private void BuyItem(ShopItemData shopItem)
    {
        if (!_playerGold.CanAfford(shopItem.BuyPrice))
        {
            Debug.Log("Not enough gold.");
            return;
        }

        bool added = _inventorySystem.AddItem(shopItem.Item, 1);

        if (!added)
        {
            Debug.Log("Inventory is full.");
            return;
        }

        _playerGold.SpendGold(shopItem.BuyPrice);
        RefreshUI();
    }

    private void SellItem(ShopItemData shopItem)
    {
        bool removed = _inventorySystem.RemoveItem(shopItem.Item, 1);

        if (!removed)
        {
            Debug.Log("You do not have this item.");
            return;
        }

        _playerGold.AddGold(shopItem.SellPrice);
        RefreshUI();
    }

    private ShopItemData FindShopItem(FarmItemData item)
    {
        foreach (ShopItemData shopItem in _shopItems)
        {
            if (shopItem != null && shopItem.Item == item)
                return shopItem;
        }

        return null;
    }

    private void ClearContent()
    {
        for (int i = _contentParent.childCount - 1; i >= 0; i--)
            Destroy(_contentParent.GetChild(i).gameObject);
    }

    private void ClearSellContent()
    {
        for (int i = _sellContentParent.childCount - 1; i >= 0; i--)
            Destroy(_sellContentParent.GetChild(i).gameObject);
    }

    private void OpenBuyInterface()
    {
        _mainShopMenu.SetActive(false);
        _buyInterfacePanel.SetActive(true);
        _sellInterfacePanel.SetActive(false);
        ShowBuyTab();
    }

    private void OpenSellInterface()
    {
        _mainShopMenu.SetActive(false);
        _buyInterfacePanel.SetActive(false);
        _sellInterfacePanel.SetActive(true);
        ShowSellTab();
    }

    private void BackToMainMenu()
    {
        _mainShopMenu.SetActive(true);
        _buyInterfacePanel.SetActive(false);
        _sellInterfacePanel.SetActive(false);
    }
}