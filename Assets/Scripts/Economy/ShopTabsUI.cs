using UnityEngine;

public class ShopTabsUI : MonoBehaviour
{
    public GameObject enterShopButton;
    public GameObject shopPanel;
    public GameObject mainMenuPanel;
    public GameObject buyPanel;
    public InventoryUI inventoryUI;
    public GameObject sellBackButton;
    private ShopUI shopUI;

    private void Awake()
    {
        shopUI = buyPanel.GetComponent<ShopUI>();
    }

    public void OpenBuyTab()
    {
        mainMenuPanel.SetActive(false);
        buyPanel.SetActive(true);

        if (shopUI != null)
        {
            shopUI.RefreshShop();
        }
    }

    public void OpenSellTab()
    {
        mainMenuPanel.SetActive(false);

        inventoryUI.gameObject.SetActive(true);
        inventoryUI.transform.SetAsLastSibling();

        inventoryUI.sellMode = true;
        inventoryUI.RefreshInventory();

        sellBackButton.SetActive(true);
    }

    public void BackToMenu()
    {
        buyPanel.SetActive(false);

        inventoryUI.sellMode = false;
        inventoryUI.RefreshInventory();

        sellBackButton.SetActive(false);

        inventoryUI.transform.SetAsFirstSibling();

        mainMenuPanel.SetActive(true);
    }

    public void ExitShop()
    {
        buyPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        shopPanel.SetActive(false);

        inventoryUI.sellMode = false;
        inventoryUI.RefreshInventory();

        sellBackButton.SetActive(false);

        enterShopButton.SetActive(true);
    }
}