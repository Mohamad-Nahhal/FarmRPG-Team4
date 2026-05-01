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
        if (_animalData != null && _animalInfoUI != null)
        {
            _animalInfoUI.ShowAnimalInfo(_animalData);
        }
    }
}