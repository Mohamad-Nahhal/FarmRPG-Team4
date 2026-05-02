using UnityEngine;
using TMPro;

public class AnimalProductionManager : MonoBehaviour
{
    [SerializeField] private AnimalData[] _animals;
    [SerializeField] private GameObject _collectionPanel;
    [SerializeField] private TextMeshProUGUI _collectionText;

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
                    break;
                case "Milk":
                    _milk++;
                    break;
                case "Wool":
                    _wool++;
                    break;
            }
        }

        _animals[i].AdvanceDay();
    }

    UpdateCollectionUI();
}

    public void OpenCollectionPanel()
    {
        if (_collectionPanel != null)
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