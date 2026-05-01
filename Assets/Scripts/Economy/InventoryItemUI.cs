using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text itemNameText;
    public TMP_Text quantityText;
    public Button sellButton;

    private ItemData item;
    private InventoryUI inventoryUI;

    public void Setup(InventorySlot slot, InventoryUI ui, bool sellMode)
    {
        item = slot.item;
        inventoryUI = ui;

        itemIcon.sprite = slot.item.icon;
        itemNameText.text = slot.item.itemName;
        quantityText.text = "x" + slot.quantity;

        sellButton.gameObject.SetActive(sellMode);

        if (sellMode)
        {
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(SellThisItem);
        }
    }

    private void SellThisItem()
    {
        Shop shop = FindObjectOfType<Shop>();

        if (shop != null)
        {
            bool sold = shop.SellItem(item);

            if (sold)
            {
                inventoryUI.RefreshInventory();

                if (inventoryUI.shopUI != null)
                {
                    inventoryUI.shopUI.RefreshGold();
                }
            }
        }
    }
}