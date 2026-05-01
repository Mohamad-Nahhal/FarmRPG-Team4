using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ItemData> itemsForSale = new List<ItemData>();

    public bool BuyItem(ItemData item)
    {
        if (!itemsForSale.Contains(item))
            return false;


        if (PlayerEconomy.Instance.SpendGold(item.buyPrice))
        {
            PlayerInventory.Instance.AddItem(item, 1);
            return true;
        }

        return false;
    }

    public bool SellItem(ItemData item)
    {

        if (PlayerInventory.Instance.RemoveItem(item, 1))
        {
            PlayerEconomy.Instance.AddGold(item.sellPrice);
            return true;
        }

        return false;
    }
}