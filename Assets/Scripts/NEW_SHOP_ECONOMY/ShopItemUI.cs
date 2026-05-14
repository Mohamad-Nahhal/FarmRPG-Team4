using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _actionButtonText;

    public void Setup(FarmItemData item, string priceText, string buttonText, System.Action onClick)
    {
        if (item == null)
        {
            return;
        }

        if (_itemIcon != null)
        {
            _itemIcon.sprite = item.Icon;
            _itemIcon.enabled = item.Icon != null;
        }

        if (_itemNameText != null)
        {
            _itemNameText.text = item.ItemName;
        }

        if (_priceText != null)
        {
            _priceText.text = priceText;
        }

        if (_actionButtonText != null)
        {
            _actionButtonText.text = buttonText;
        }

        if (_actionButton != null)
        {
            _actionButton.onClick.RemoveAllListeners();
            _actionButton.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}