using UnityEngine;

public class FarmGridInputTester : MonoBehaviour
{
    [SerializeField] private FarmGridManager _farmGridManager;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private StaminaSystem _staminaSystem;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private KeyCode _farmActionKey = KeyCode.E;
    [SerializeField] private KeyCode _advanceDayKey = KeyCode.N;

    [Header("Stamina Costs")]
    [SerializeField] private int _hoeCost = 10;
    [SerializeField] private int _wateringCanCost = 5;
    [SerializeField] private int _plantingCost = 5;
    [SerializeField] private int _harvestCost = 10;
    [SerializeField] private string _notEnoughStaminaMessage = "Not enough stamina.";

    private void Awake()
    {
        if (_farmGridManager == null)
        {
            _farmGridManager = GetComponent<FarmGridManager>();
        }

        if (_inventorySystem == null)
        {
            _inventorySystem = UnityEngine.Object.FindAnyObjectByType<InventorySystem>();
        }

        if (_staminaSystem == null)
        {
            _staminaSystem = GetComponent<StaminaSystem>();
        }

        if (_playerController == null)
        {
            _playerController = UnityEngine.Object.FindAnyObjectByType<PlayerController>();
        }
    }

    private void Update()
    {
        if (_farmGridManager == null || _inventorySystem == null || _staminaSystem == null || _playerController == null)
        {
            Debug.Log($"FarmGridInputTester missing dependency. Grid: {_farmGridManager != null}, Inventory: {_inventorySystem != null}, Stamina: {_staminaSystem != null}, Player: {_playerController != null}");
            return;
        }

        if (Input.GetKeyDown(_advanceDayKey))
        {
            SleepUntilNextDay();
        }

        if (!Input.GetKeyDown(_farmActionKey))
        {
            return;
        }

        if (!TryGetFacingFarmCell(out Vector2Int coordinates))
        {
            Debug.Log("Farm action ignored because the highlighted player tile is outside farm grid bounds.");
            return;
        }

        FarmItemData selectedItem = _inventorySystem.SelectedHotbarItem;
        Debug.Log($"Farm action on cell {coordinates}. Selected item: {(selectedItem != null ? selectedItem.ItemId : "none")}");

        if (selectedItem == null)
        {
            return;
        }

        if (TryGetToolType(selectedItem, out FarmToolType toolType))
        {
            TryApplyToolAction(coordinates, toolType);
            return;
        }

        if (TryGetCropType(selectedItem, out FarmCropType cropType))
        {
            TryPlantCropAction(coordinates, cropType);
        }
    }

    private void TryApplyToolAction(Vector2Int coordinates, FarmToolType toolType)
    {
        int staminaCost = GetToolCost(toolType);

        if (!_staminaSystem.CanAfford(staminaCost))
        {
            ShowNotEnoughStaminaFeedback(staminaCost);
            return;
        }

        bool applied = _farmGridManager.TryApplyTool(coordinates, toolType);
        Debug.Log($"Farm tool action. Tool: {toolType}, Cell: {coordinates}, Applied: {applied}");

        if (applied)
        {
            _staminaSystem.TrySpend(staminaCost);
        }
    }

    private void TryPlantCropAction(Vector2Int coordinates, FarmCropType cropType)
    {
        if (!_staminaSystem.CanAfford(_plantingCost))
        {
            ShowNotEnoughStaminaFeedback(_plantingCost);
            return;
        }

        bool planted = _farmGridManager.TryPlantCrop(coordinates, cropType);
        Debug.Log($"Farm plant action. Crop: {cropType}, Cell: {coordinates}, Planted: {planted}");

        if (planted)
        {
            if (_inventorySystem.TryConsumeSelectedHotbarItem(1))
            {
                _staminaSystem.TrySpend(_plantingCost);
            }
        }
    }

    public void SleepUntilNextDay()
{
    _farmGridManager.AdvanceDay();
    _staminaSystem.RestoreToMax();
    TimeSystem timeSystem = UnityEngine.Object.FindAnyObjectByType<TimeSystem>();
    if (timeSystem != null)
    {
        timeSystem.SetTime(timeSystem.Day + 1, 6, 0);
    }
}

    private bool TryGetFacingFarmCell(out Vector2Int coordinates)
    {
        Vector3 targetWorldPosition = _playerController.GetFacingWorldCenterPosition();
        return _farmGridManager.TryWorldToCell(targetWorldPosition, out coordinates);
    }

    private static bool TryGetToolType(FarmItemData item, out FarmToolType toolType)
    {
        toolType = default;

        if (item == null)
        {
            return false;
        }

        switch (item.ItemId)
        {
            case "tool_shovel":
            case "tool_hoe":
                toolType = FarmToolType.Hoe;
                return true;
            case "tool_watering_can":
                toolType = FarmToolType.WateringCan;
                return true;
            case "tool_axe":
            case "tool_sickle":
                toolType = FarmToolType.Sickle;
                return true;
            default:
                return false;
        }
    }

    private int GetToolCost(FarmToolType toolType)
    {
        return toolType switch
        {
            FarmToolType.Hoe => _hoeCost,
            FarmToolType.WateringCan => _wateringCanCost,
            FarmToolType.Sickle => _harvestCost,
            _ => 0
        };
    }

    private void ShowNotEnoughStaminaFeedback(int requiredStamina)
    {
        Debug.Log($"{_notEnoughStaminaMessage} Required: {requiredStamina}, Current: {_staminaSystem.CurrentStamina}.");
    }

    private static bool TryGetCropType(FarmItemData item, out FarmCropType cropType)
    {
        cropType = FarmCropType.None;

        if (item == null)
        {
            return false;
        }

        switch (item.ItemId)
        {
            case "crop_beetroot":
            case "crop_tomato":
                cropType = FarmCropType.Beetroot;
                return true;
            case "crop_carrot":
                cropType = FarmCropType.Carrot;
                return true;
            case "crop_potato":
            case "crop_corn":
                cropType = FarmCropType.Potato;
                return true;
            case "crop_wheat":
                cropType = FarmCropType.Wheat;
                return true;
            default:
                return false;
        }
    }
}
