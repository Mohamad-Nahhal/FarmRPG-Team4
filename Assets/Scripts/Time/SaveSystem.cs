using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int day;
    public int hour;
    public int minute;
    public string season;
    public int gold; // placeholder for friend's economy
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private TimeSystem _timeSystem;

    private string _savePath;

    private void Awake()
    {
        _savePath = Application.persistentDataPath + "/savefile.json";
    }

    public void Save()
    {
        GameData data = new GameData();
        data.day = _timeSystem.Day;
        data.hour = _timeSystem.Hour;
        data.minute = _timeSystem.Minute;
        data.season = _timeSystem.CurrentSeason.ToString();
        //data.gold = PlayerEconomy.Instance.currentGold; 
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_savePath, json);
        Debug.Log("Game saved to: " + _savePath);
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
        Debug.Log("Game loaded! Day: " + data.day + " | Season: " + data.season);
        //PlayerEconomy.Instance.currentGold = data.gold;
    }
}