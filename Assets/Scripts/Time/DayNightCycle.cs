using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;
    [SerializeField] private Light2D _globalLight;

    private void Update()
    {
        float totalMinutes = _timeSystem.Hour * 60 + _timeSystem.Minute;
        float t = totalMinutes / (24f * 60f);
        _globalLight.intensity = GetLightIntensity(t);
    }

    private float GetLightIntensity(float t)
    {
        // t goes from 0 to 1 representing full day
        // 6:00 AM = 0.25, Noon = 0.5, 6:00 PM = 0.75, Midnight = 0 or 1
        if (t < 0.25f) return Mathf.Lerp(0.1f, 1f, t / 0.25f);       // dawn
        if (t < 0.5f) return 1f;                                        // morning to noon
        if (t < 0.75f) return Mathf.Lerp(1f, 0.3f, (t - 0.5f) / 0.25f); // afternoon to evening
        return Mathf.Lerp(0.3f, 0.1f, (t - 0.75f) / 0.25f);           // night
    }
}