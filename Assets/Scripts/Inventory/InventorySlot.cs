using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image inventorySlotImage;
    public Color selectedColour;
    public Color notSelectedColour;

    public void Awake()
    {
        Deselect();
    }

    /// <summary>
    /// Visually selects the inventory slot by changing its image's colour to be the selectedColour.
    /// </summary>
    public void Select()
    {
        inventorySlotImage.color = selectedColour;
    }

    /// <summary>
    /// Visually deselects the inventory slot by resetting its image's colour to be the notSelectedColour.
    /// </summary>
    public void Deselect()
    {
        inventorySlotImage.color = notSelectedColour;
    }
}
