using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public ItemType m_itemType; // Define if an item is a plant or a tool
    public ActionType m_actionType; // What action this item does
    public float m_sellingValue;

    [Header("Only UI")]
    public bool m_stackable = true;

    [Header("Both")]
    public Sprite m_image; // Sprite shown in inventory
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