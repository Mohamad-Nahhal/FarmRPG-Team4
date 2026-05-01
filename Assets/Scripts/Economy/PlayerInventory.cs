using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public List<InventorySlot> items = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData item, int amount)
    {
        InventorySlot slot = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                slot = items[i];
                break;
            }
        }

        if (slot != null)
            slot.quantity += amount;
        else
            items.Add(new InventorySlot(item, amount));
    }

    public bool RemoveItem(ItemData item, int amount)
    {
        InventorySlot slot = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                slot = items[i];
                break;
            }
        }

        if (slot != null && slot.quantity >= amount)
        {
            slot.quantity -= amount;

            if (slot.quantity <= 0)
                items.Remove(slot);

            return true;
        }

        return false;
    }

    public int GetItemQuantity(ItemData item)
    {
        InventorySlot slot = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                slot = items[i];
                break;
            }
        }

        if (slot != null)
            return slot.quantity;
        else
            return 0;
    }
}