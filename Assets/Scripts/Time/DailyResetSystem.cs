using UnityEngine;
using UnityEngine.Events;

public class DailyResetSystem : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;
    public UnityEvent OnDailyReset;

    private void OnEnable()
    {
        _timeSystem.OnNewDay.AddListener(TriggerDailyReset);
    }

    private void OnDisable()
    {
        _timeSystem.OnNewDay.RemoveListener(TriggerDailyReset);
    }

    public void TriggerDailyReset()
    {
        Debug.Log("Daily reset triggered!");
        OnDailyReset.Invoke();
    }
}