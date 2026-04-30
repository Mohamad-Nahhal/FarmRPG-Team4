using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public Shop shop;
    public TMP_Text goldText;
    public Transform itemContainer;
    public GameObject itemPrefab;

    private void Start()
    {
        RefreshShop();
        RefreshGold();
    }

   public void RefreshGold()
{
    goldText.text = "Gold: " + PlayerEconomy.Instance.currentGold;
}

    public void RefreshShop()
    {
        RefreshGold();

        for (int i = itemContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(itemContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < shop.itemsForSale.Count; i++)
        {
            ItemData item = shop.itemsForSale[i];

            GameObject newItem = Instantiate(itemPrefab, itemContainer);
            ShopItemUI itemUI = newItem.GetComponent<ShopItemUI>();
            itemUI.Setup(item, shop, this);
        }
    }
}