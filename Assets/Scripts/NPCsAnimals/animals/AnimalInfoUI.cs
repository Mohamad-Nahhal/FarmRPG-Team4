using UnityEngine;
using TMPro;

public class AnimalInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _hungerText;
    [SerializeField] private TextMeshProUGUI _happinessText;
    [SerializeField] private TextMeshProUGUI _feedbackText;
    [SerializeField] private TextMeshProUGUI _hungerMeterText;

    private AnimalData _currentAnimal;

    private void Start()
    {
        if (_panel != null)
            _panel.SetActive(false);

        if (_feedbackText != null)
            _feedbackText.text = "";
    }

    public void ShowAnimalInfo(AnimalData animal)
    {
        _currentAnimal = animal;

        if (_panel != null)
            _panel.SetActive(true);

        RefreshUI();
    }

    public void HidePanel()
    {
        if (_panel != null)
            _panel.SetActive(false);

        if (_feedbackText != null)
            _feedbackText.text = "";
    }

    public void FeedAnimal()
    {
        if (_currentAnimal == null) return;

        _currentAnimal.FeedAnimal();

        if (_feedbackText != null)
            _feedbackText.text = "Fed!";

        RefreshUI();
    }

    public void PetAnimal()
    {
        if (_currentAnimal == null) return;

        _currentAnimal.PetAnimal();

        if (_feedbackText != null)
            _feedbackText.text = "Petted!";

        RefreshUI();
    }

    private void RefreshUI()
{
    if (_currentAnimal == null) return;

    _nameText.text = "Name: " + _currentAnimal.AnimalName;
    _typeText.text = "Type: " + _currentAnimal.Type;
    _hungerText.text = "Hunger: " + _currentAnimal.HungerLevel;
    _happinessText.text = "Mood: " + _currentAnimal.GetMoodState();

    if (_hungerMeterText != null)
        _hungerMeterText.text = "Hunger: " + _currentAnimal.HungerLevel + "/100";
}
private void Update()
{
    if (_panel != null && _panel.activeSelf && _currentAnimal != null)
    {
        RefreshUI();
    }
}
}