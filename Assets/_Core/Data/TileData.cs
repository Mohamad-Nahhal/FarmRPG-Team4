using UnityEngine;

public enum TileType
{
    Grass,
    Soil,
    Water,
    Path,
    Building,
    Door,
    Stamina,
    Shop
}
[System.Serializable]
public class TileData
{
    public string DoorID;
    public string TargetScene;
    public Vector2 SpawnPosition;
    
    public TileType Type;
    public bool IsWalkable;
    public bool IsInteractable; // currently door exclusive

    //UNADDED yet
    public bool IsTilled;
    public bool IsWatered;
    public bool HasCrop;
}