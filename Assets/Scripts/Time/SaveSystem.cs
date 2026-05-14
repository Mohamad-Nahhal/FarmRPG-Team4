using UnityEngine;
using System.IO;

[System.Serializable]
public class InventorySlotSaveData
{
    public string itemId;
    public int quantity;
}

[System.Serializable]
public class GameData
{
    public int day;
    public int hour;
    public int minute;
    public string season;

    public int gold;
    public int stamina;

    public float playerX;
    public float playerY;
    public float playerZ;

    public InventorySlotSaveData[] inventorySlots;
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;
    [SerializeField] private PlayerGold _playerGold;
    [SerializeField] private StaminaSystem _staminaSystem;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private Transform _player;

    private string _savePath;

    private void Awake()
{
    _savePath = Application.persistentDataPath + "/savefile.json";

    if (_timeSystem == null)
        _timeSystem = FindAnyObjectByType<TimeSystem>();

    if (_playerGold == null)
        _playerGold = FindAnyObjectByType<PlayerGold>();

    if (_staminaSystem == null)
        _staminaSystem = FindAnyObjectByType<StaminaSystem>();

    if (_inventorySystem == null)
        _inventorySystem = FindAnyObjectByType<InventorySystem>();

    if (_player == null)
        _player = FindAnyObjectByType<PlayerController>()?.transform;
}

    public void Save()
    {
        GameData data = new GameData();
        data.day = _timeSystem.Day;
        data.hour = _timeSystem.Hour;
        data.minute = _timeSystem.Minute;
        data.season = _timeSystem.CurrentSeason.ToString();
        //data.gold = PlayerEconomy.Instance.currentGold; 
        
        Debug.Log("Game saved to: " + _savePath);
        data.gold = _playerGold.Gold;
        data.stamina = _staminaSystem.CurrentStamina;

        data.playerX = _player.position.x;
        data.playerY = _player.position.y;
        data.playerZ = _player.position.z;

        data.inventorySlots = new InventorySlotSaveData[_inventorySystem.Slots.Count];

for (int i = 0; i < _inventorySystem.Slots.Count; i++)
{
    InventorySlotData slot = _inventorySystem.Slots[i];

    data.inventorySlots[i] = new InventorySlotSaveData();

    if (slot != null && slot.Item != null && slot.Quantity > 0)
    {
        data.inventorySlots[i].itemId = slot.Item.ItemId;
        data.inventorySlots[i].quantity = slot.Quantity;
    }
}
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_savePath, json);
    }

    public void Load()
    {
        if (!File.Exists(_savePath))
        {
            Debug.Log("No save file found!");
            return;
        }

        string json = File.ReadAllText(_savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        _timeSystem.SetTime(data.day, data.hour, data.minute);
        _playerGold.SetGold(data.gold);
        _staminaSystem.SetStamina(data.stamina);
        _player.position = new Vector3(data.playerX, data.playerY, data.playerZ);
        Debug.Log("Game loaded! Day: " + data.day + " | Season: " + data.season);
        //PlayerEconomy.Instance.currentGold = data.gold;
    }
}