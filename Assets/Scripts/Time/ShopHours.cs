using UnityEngine;
using TMPro;

public class ShopHours : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;
    [SerializeField] private int _openHour = 8;
    [SerializeField] private int _closeHour = 18;
    [SerializeField] private TextMeshProUGUI _shopStatusText;

    public bool IsOpen { get; private set; }

    private void Update()
    {
        int currentHour = _timeSystem.Hour;
        IsOpen = currentHour >= _openHour && currentHour < _closeHour;

        if (IsOpen)
            _shopStatusText.text = "Shop is Open";
        else
            _shopStatusText.text = "Shop is Closed";
    }

    
}