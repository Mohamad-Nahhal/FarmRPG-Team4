using UnityEngine;
using TMPro;

public class NPCTimeManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private TMP_Text _hourText;

    private int _currentDay = 1;
    [SerializeField] private int _currentHour = 8;
    public int CurrentHour => _currentHour;

    private void Start()
    {
        UpdateUI();
    }

    public void AdvanceHour()
    {
        _currentHour++;
        if (_currentHour >= 24)
        {
            _currentHour = 0;
            _currentDay++;
        }

        UpdateUI();
    }

    public void AdvanceDay()
    {
        _currentDay++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _dayText.text = "Day: "+_currentDay;
        _hourText.text = "Hour: "+_currentHour + ":00";
    }

    public int GetCurrentHour()
    {
        return _currentHour;
    }

    public int GetCurrentDay()
    {
        return _currentDay;
    }
}