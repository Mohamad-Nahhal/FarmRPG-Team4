using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemContainer;
    public GameObject itemPrefab;
    public bool sellMode = false;

    public ShopUI shopUI;

    private void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        for (int i = itemContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(itemContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < PlayerInventory.Instance.items.Count; i++)
        {
            InventorySlot slot = PlayerInventory.Instance.items[i];

            GameObject newItem = Instantiate(itemPrefab, itemContainer);
            InventoryItemUI itemUI = newItem.GetComponent<InventoryItemUI>();
            itemUI.Setup(slot, this, sellMode);
        }
    }
}