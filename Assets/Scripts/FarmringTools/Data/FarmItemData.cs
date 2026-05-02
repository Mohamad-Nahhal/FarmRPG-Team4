using UnityEngine;

[CreateAssetMenu(fileName = "FarmItemData", menuName = "Game/Item Data")]
public class FarmItemData : ScriptableObject
{
    public string ItemId;
    public string ItemName;
    public Sprite Icon;
    public int MaxStack = 99;
}