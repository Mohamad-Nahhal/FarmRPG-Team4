using UnityEngine;

public class AnimalData : MonoBehaviour
{
    [SerializeField] private string _animalName;
    [SerializeField] private AnimalType _animalType;
    [SerializeField] [Range(0, 100)] private int _hungerLevel = 0;
    [SerializeField] [Range(0, 100)] private int _happinessLevel = 80;

    private bool _wasFedToday = false;

    public string AnimalName => _animalName;
    public AnimalType Type => _animalType;
    public int HungerLevel => _hungerLevel;
    public int HappinessLevel => _happinessLevel;

    public void FeedAnimal()
{
    _hungerLevel -= 25;

    if (_hungerLevel < 0)
        _hungerLevel = 0;

    _wasFedToday = true;

    Debug.Log(_animalName + " fed. Hunger: " + _hungerLevel);
}

    public void PetAnimal()
    {
        _happinessLevel += 15;

        if (_happinessLevel > 100)
            _happinessLevel = 100;

        Debug.Log(_animalName + " was petted.");
    }

    public void AdvanceDay()
{
    _hungerLevel += 25;

    if (_hungerLevel > 100)
        _hungerLevel = 100;

    if (_hungerLevel >= 70)
    {
        _happinessLevel -= 20;

        if (_happinessLevel < 0)
            _happinessLevel = 0;
    }

    _wasFedToday = false;

    Debug.Log(_animalName + " hunger is now: " + _hungerLevel);
}
    public bool CanProduce()
    {
        return _wasFedToday && _hungerLevel < 70;
    }

    public string GetProducedItem()
    {
        switch (_animalType)
        {
            case AnimalType.Chicken:
                return "Egg";
            case AnimalType.Cow:
                return "Milk";
            case AnimalType.Sheep:
                return "Wool";
            default:
                return "";
        }
    }

    public string GetHungerState()
    {
        if (_hungerLevel >= 70) return "Very Hungry";
        if (_hungerLevel >= 40) return "Hungry";
        return "Full";
    }

    public string GetMoodState()
    {
        if (_hungerLevel >= 70) return "Sad";
        if (_happinessLevel >= 70) return "Happy";
        if (_happinessLevel >= 40) return "Normal";
        return "Sad";
    }
}