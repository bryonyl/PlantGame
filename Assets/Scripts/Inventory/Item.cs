using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public TileBase tile;
    public ItemType type; // Define if an item is placeable or a tool
    public ActionType actionType; // What action this item does
    public Vector2Int range = new Vector2Int(5, 4);

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
    Dig,
    Water,
    Harvest
}