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
    if (t < 0.25f) return Mathf.Lerp(0.1f, 1f, t / 0.25f);                    // dawn
    if (t < 0.5f) return 1f;                                                    // day
    if (t < 0.625f) return Mathf.Lerp(1f, 0.5f, (t - 0.5f) / 0.125f);        // evening
    if (t < 0.75f) return Mathf.Lerp(0.5f, 0.3f, (t - 0.625f) / 0.125f);     // dusk
    return Mathf.Lerp(0.3f, 0.1f, (t - 0.75f) / 0.25f);                       // night
}
}