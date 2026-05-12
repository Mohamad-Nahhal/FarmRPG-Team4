using UnityEngine;

public class AnimalClickHandler : MonoBehaviour
{
    [SerializeField] private AnimalInfoUI _animalInfoUI;
    private AnimalData _animalData;

    private void Start()
    {
        _animalData = GetComponent<AnimalData>();
    }

    private void OnMouseDown()
{
    Debug.Log("Chicken clicked");

    if (_animalData == null)
        Debug.LogError("AnimalData is missing on chicken");

    if (_animalInfoUI == null)
        Debug.LogError("AnimalInfoUI is not assigned");

    if (_animalData != null && _animalInfoUI != null)
        _animalInfoUI.ShowAnimalInfo(_animalData);
}
} 