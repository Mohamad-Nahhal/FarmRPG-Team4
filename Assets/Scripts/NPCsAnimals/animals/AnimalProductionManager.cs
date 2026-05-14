using UnityEngine;
using TMPro;

public class AnimalProductionManager : MonoBehaviour
{
    [SerializeField] private AnimalData[] _animals;
    [SerializeField] private GameObject _collectionPanel;
    [SerializeField] private TextMeshProUGUI _collectionText;
    [SerializeField] private InventorySystem _inventorySystem;

[SerializeField] private FarmItemData eggItem;
[SerializeField] private FarmItemData milkItem;
[SerializeField] private FarmItemData woolItem;

    private int _eggs = 0;
    private int _milk = 0;
    private int _wool = 0;

    private void Start()
    {
        if (_collectionPanel != null)
            _collectionPanel.SetActive(false);

        UpdateCollectionUI();
    }

    public void ProduceItemsForNewDay()
{
    for (int i = 0; i < _animals.Length; i++)
    {
        if (_animals[i] == null) continue;

        if (_animals[i].CanProduce())
        {
            string item = _animals[i].GetProducedItem();

            switch (item)
            {
                case "Egg":
    _eggs++;
    _inventorySystem.AddItem(eggItem, 1);
    break;

case "Milk":
    _milk++;
    _inventorySystem.AddItem(milkItem, 1);
    break;

case "Wool":
    _wool++;
    _inventorySystem.AddItem(woolItem, 1);
    break;
            }
        }

        _animals[i].AdvanceDay();
    }

    UpdateCollectionUI();
}
    public void OpenCollectionPanel()
    {
        Debug.Log("OPEN METHOD CALLED");
    _collectionPanel.SetActive(true);
    }

    public void CloseCollectionPanel()
    {
        if (_collectionPanel != null)
            _collectionPanel.SetActive(false);
    }

    private void UpdateCollectionUI()
    {
        if (_collectionText != null)
        {
            _collectionText.text =
                "Eggs: " + _eggs + "\n" +
                "Milk: " + _milk + "\n" +
                "Wool: " + _wool;
        }
    }
    
}