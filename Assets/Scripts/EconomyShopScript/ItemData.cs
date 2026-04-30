using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Farm RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int buyPrice;
    public int sellPrice;
}