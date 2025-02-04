using UnityEngine;
/// <summary>
/// Handles finding sufficient inventory slots, adding items to them and creating and accessing items.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// The maximum size of a stack of stackable items.
    /// </summary>
    public int maxStackedItems = 10;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private int m_selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    /// <summary>
    /// Changes the player's selected inventory slot.
    /// </summary>
    /// <param name="newSlotValue">The new slot to be selected</param>
    public void ChangeSelectedSlot(int newSlotValue)
    {
        if (m_selectedSlot >= 0)
        {
            inventorySlots[m_selectedSlot].Deselect();
        }

        inventorySlots[newSlotValue].Select();
        m_selectedSlot = newSlotValue;
    }
    
    /// <summary>
    /// Finds a sufficient (e.g. of the same item and not already full) or unoccupied inventory slot and adds an item to it.
    /// Will only add an item if both of these conditions return true. Else, the item will not be added.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    /// <returns>Returns true if item can be added. Returns false if item cannot be added.</returns>
    public bool AddItem(Item item)
    {
        // Checks if any slot has the same item with count lower than maximum
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.ResetItemQuantityText();
                return true;
            }
        }

        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Spawns a new item in the inventory slot using info from AddItem method.
    /// </summary>
    /// <param name="item">Item to be spawned.</param>
    /// <param name="slot">Inventory slot for item to be placed in.</param>
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    /// <summary>
    /// Accesses the player's currently selected item in their hot bar.
    /// </summary>
    /// <param name="use">Boolean that determines whether the item should be expended or not.</param>
    /// <returns>If the player is trying to access nothing, then null is returned.</returns>
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[m_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.ResetItemQuantityText();
                }
            }
        }

        return null;
    }

    // public Item QueryItemInSlot(InventorySlot slot, InventoryItem item)
    // {
    //     InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    //     if (itemInSlot == item)
    //     {
    //         Debug.Log($"{itemInSlot} is equal to {item}");
    //
    //         return 
    //     }
    //     else
    //     {
    //         Debug.Log($"{itemInSlot} is not equal to {item}");
    //     }
    // }
}
