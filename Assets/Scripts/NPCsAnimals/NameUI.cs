using UnityEngine;

public class NameUI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);

    private Camera cam;
    private RectTransform rectTransform;

    private void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null || cam == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + worldOffset);
        rectTransform.position = screenPos;
    }
}