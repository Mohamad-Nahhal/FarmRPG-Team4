using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform _morningLocation;
    [SerializeField] private Transform _afternoonLocation;
    [SerializeField] private Transform _eveningLocation;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector2 _offset;

    private Transform _targetLocation;
    private NPCTimeManager _timeManager;

    private void Start()
    {
        _timeManager = FindFirstObjectByType<NPCTimeManager>();
        UpdateTargetLocation();
    }

    private void Update()
    {
        UpdateTargetLocation();
        MoveToTarget();
    }

    private void UpdateTargetLocation()
    {
        if (_timeManager == null) return;

        int hour = _timeManager.CurrentHour;

        if (hour < 10)
            _targetLocation = _morningLocation;
        else if (hour < 14)
            _targetLocation = _afternoonLocation;
        else
            _targetLocation = _eveningLocation;
    }

    private void MoveToTarget()
    {
        if (_targetLocation == null) return;

        Vector2 targetPosition = (Vector2)_targetLocation.position + _offset;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            _speed * Time.deltaTime
        );
    }
}