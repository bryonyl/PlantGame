using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image m_inventorySlotImage;
    public Color m_selectedColour;
    public Color m_notSelectedColour;

    public void Awake()
    {
        Deselect();
    }

    /// <summary>
    /// Visually selects the inventory slot by changing its image's colour to be the m_selectedColour.
    /// </summary>
    public void Select()
    {
        m_inventorySlotImage.color = m_selectedColour;
    }

    /// <summary>
    /// Visually deselects the inventory slot by resetting its image's colour to be the m_notSelectedColour.
    /// </summary>
    public void Deselect()
    {
        m_inventorySlotImage.color = m_notSelectedColour;
    }
}
