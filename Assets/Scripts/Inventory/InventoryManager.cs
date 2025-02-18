using System;
using UnityEngine;
/// <summary>
/// Handles finding sufficient inventory slots, adding items to them and creating and accessing items.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public int m_maxStackedItems = 10;
    public InventorySlot[] m_inventorySlots;
    public GameObject m_inventoryItemPrefab;
    private int m_selectedSlot = -1;

    public PlayerActions m_playerActions;
    
    [Header("Items")]
    public Item m_wateringCanItem;
    public Item m_hoeItem;
    public Item m_beetrootItem;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        // Checks if player can use the currently selected item constantly
        QuerySelectedItem();
    }

    /// <summary>
    /// Changes the player's selected inventory slot.
    /// </summary>
    /// <param name="newSlotValue">The new slot to be selected</param>
    public void ChangeSelectedSlot(int newSlotValue)
    {
        if (m_selectedSlot >= 0)
        {
            m_inventorySlots[m_selectedSlot].Deselect();
        }

        m_inventorySlots[newSlotValue].Select();
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
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            InventorySlot slot = m_inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < m_maxStackedItems && itemInSlot.item.m_stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.ResetItemQuantityText();
                return true;
            }
        }

        // Find any empty slot
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            InventorySlot slot = m_inventorySlots[i];
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
    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(m_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    /// <summary>
    /// Accesses the player's currently selected item in their hot bar.
    /// </summary>
    public Item QuerySelectedItem()
    {
        InventorySlot slot = m_inventorySlots[m_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;

            if (itemInSlot.item == m_wateringCanItem)
            {
                m_playerActions.m_hoeUsageAllowed = false;
                m_playerActions.m_itemSellingAllowed = false;
                m_playerActions.m_wateringCanUsageAllowed = true;
                Debug.Log($"Watering can usage allowed = {m_playerActions.m_wateringCanUsageAllowed}");
                
                return itemInSlot.item;
            }
            else if (itemInSlot.item == m_hoeItem)
            {
                m_playerActions.m_wateringCanUsageAllowed = false;
                m_playerActions.m_itemSellingAllowed = false;
                m_playerActions.m_hoeUsageAllowed = true;
                Debug.Log($"Hoe usage allowed = {m_playerActions.m_hoeUsageAllowed}");
                
                return itemInSlot.item;
            }
            else if (itemInSlot.item == m_beetrootItem)
            {
                m_playerActions.m_wateringCanUsageAllowed = false;
                m_playerActions.m_hoeUsageAllowed = false;
                m_playerActions.m_itemSellingAllowed = true;
                Debug.Log($"Plant selling allowed = {m_playerActions.m_itemSellingAllowed}");
                
                return itemInSlot.item;
            }
            else
            {
                m_playerActions.m_wateringCanUsageAllowed = false;
                m_playerActions.m_hoeUsageAllowed = false;
                m_playerActions.m_itemSellingAllowed = false;
                
                Debug.Log("No item in slot that can be used");
            }
        }
        return null;
    }

    /// <summary>
    /// Expends the "used" item (so it is destroyed and removed from the inventory).
    /// </summary>
    /// <returns></returns>
    public Item UseItem()
    {
        InventorySlot slot = m_inventorySlots[m_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        
        if (m_playerActions.m_itemSellingAllowed)
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

        return null;
    }
}
