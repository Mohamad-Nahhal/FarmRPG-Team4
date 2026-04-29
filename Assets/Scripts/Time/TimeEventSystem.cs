using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class TimeEvent
{
    public string eventName;
    public int hour;
    public int minute;
    public UnityEvent onTimeReached;
    [HideInInspector] public bool firedToday = false;
}

public class TimeEventSystem : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;
    [SerializeField] private List<TimeEvent> _timeEvents;

    private void Update()
    {
        int currentTotalMinutes = _timeSystem.Hour * 60 + _timeSystem.Minute;

        foreach (TimeEvent timeEvent in _timeEvents)
        {
            int eventTotalMinutes = timeEvent.hour * 60 + timeEvent.minute;

            if (!timeEvent.firedToday && currentTotalMinutes >= eventTotalMinutes)
            {
                timeEvent.firedToday = true;
                Debug.Log("Time event fired: " + timeEvent.eventName);
                timeEvent.onTimeReached.Invoke();
            }
        }
    }

    public void ResetDailyEvents()
    {
        foreach (TimeEvent timeEvent in _timeEvents)
        {
            timeEvent.firedToday = false;
        }
    }
}