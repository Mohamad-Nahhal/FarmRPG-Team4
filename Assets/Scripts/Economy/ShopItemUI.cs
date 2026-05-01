using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;

    private ItemData item;
    private Shop shop;
    private ShopUI shopUI;

    public void Setup(ItemData newItem, Shop newShop, ShopUI newShopUI)
    {
        item = newItem;
        shop = newShop;
        shopUI = newShopUI;

        itemIcon.sprite = item.icon;
        itemNameText.text = item.itemName;
        priceText.text = "Price: " + item.buyPrice;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyThisItem);
    }

    void BuyThisItem()
    {
        bool bought = shop.BuyItem(item);

        if (bought)
        {
            shopUI.RefreshShop();
        }
    }
}