using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public ItemType itemType; // Define if an item is a plant or a tool
    public ActionType actionType; // What action this item does
    public float sellingValue;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image; // Sprite shown in inventory
}

public enum ItemType
{
    Plant,
    Tool
}

public enum ActionType
{
    None,
    Dig,
    Water,
}