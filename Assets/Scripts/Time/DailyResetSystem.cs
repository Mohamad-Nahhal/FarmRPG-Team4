using UnityEngine;
using UnityEngine.Events;

public class DailyResetSystem : MonoBehaviour
{
    public UnityEvent OnDailyReset;

    public void TriggerDailyReset()
    {
        Debug.Log("Daily reset triggered!");
        OnDailyReset.Invoke();
    }
}