using UnityEngine;
using TMPro;
using UnityEngine.Events;
public enum Season { Spring, Summer, Fall, Winter }
public class TimeSystem : MonoBehaviour
{
    [SerializeField] private int _day = 1;
    [SerializeField] private int _hour = 6;
    [SerializeField] private int _minute = 0;
    [SerializeField] private float _timeSpeed = 60f;
    [SerializeField] private TextMeshProUGUI _timeText;
    public UnityEvent OnNewDay;
    public int Day => _day;
    public int Hour => _hour;
    public int Minute => _minute;
    private float _timer;
    private bool _timeChanged = false;
    [SerializeField] private int _daysPerSeason = 7;
    public Season CurrentSeason { get; private set; } = Season.Spring;
    private void Update()
    {
        UpdateTime();
        UpdateUI();
    }
    private void UpdateTime()
    {
        _timer += Time.deltaTime * _timeSpeed;
        if (_timer >= 1f)
        {
            _timer = 0f;
            _minute++;
            _timeChanged = true;
            if (_minute >= 60)
            {
                _minute = 0;
                _hour++;
                if (_hour >= 24)
                {
                    _hour = 0;
                    _day++;
                    OnNewDay.Invoke();
                    CurrentSeason = (Season)((_day - 1) / _daysPerSeason % 4);
                }
            }
        }
    }
    private void UpdateUI()
    {
        if (!_timeChanged) return;
        _timeChanged = false;
        _timeText.text = "Day " + _day + " | " + CurrentSeason + "\n" + _hour.ToString("00") + ":" + _minute.ToString("00");
    }
    public void SetTime(int day, int hour, int minute)
    {
        _day = day;
        _hour = hour;
        _minute = minute;
        CurrentSeason = (Season)((_day - 1) / _daysPerSeason % 4);
        _timeChanged = true;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SleepToNextDay()
    {
        _day++;
        _hour = 6;
        _minute = 0;
        _timer = 0f;

        CurrentSeason = (Season)((_day - 1) / _daysPerSeason % 4);

        OnNewDay?.Invoke();

        _timeChanged = true;
    }
}