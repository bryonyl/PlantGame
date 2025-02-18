using System;
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

   public PlayerActions playerActions;
    
    [Header("Items")]
    public Item wateringCanItem;
    public Item hoeItem;
    public Item beetrootItem;

    private void Start()
    {
        ChangeSelectedSlot(0);
        //GameObject.DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // Checks if player can use the currently selected item
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
    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    /// <summary>
    /// Accesses the player's currently selected item in their hot bar.
    /// </summary>
    public Item QuerySelectedItem()
    {
        InventorySlot slot = inventorySlots[m_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;

            if (itemInSlot.item == wateringCanItem)
            {
                playerActions.m_hoeUsageAllowed = false;
                playerActions.m_itemSellingAllowed = false;
                playerActions.m_wateringCanUsageAllowed = true;
                Debug.Log($"Watering can usage allowed = {playerActions.m_wateringCanUsageAllowed}");
                
                return itemInSlot.item;
            }
            else if (itemInSlot.item == hoeItem)
            {
                playerActions.m_wateringCanUsageAllowed = false;
                playerActions.m_itemSellingAllowed = false;
                playerActions.m_hoeUsageAllowed = true;
                Debug.Log($"Hoe usage allowed = {playerActions.m_hoeUsageAllowed}");
                
                return itemInSlot.item;
            }
            else if (itemInSlot.item == beetrootItem)
            {
                playerActions.m_wateringCanUsageAllowed = false;
                playerActions.m_hoeUsageAllowed = false;
                playerActions.m_itemSellingAllowed = true;
                Debug.Log($"Plant selling allowed = {playerActions.m_itemSellingAllowed}");
                
                return itemInSlot.item;
            }
            else
            {
                playerActions.m_wateringCanUsageAllowed = false;
                playerActions.m_hoeUsageAllowed = false;
                playerActions.m_itemSellingAllowed = false;
                
                Debug.Log("No item in slot that can be used");
            }
        }
        return null;
    }

    public Item UseItem()
    {
        InventorySlot slot = inventorySlots[m_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        
        if (playerActions.m_itemSellingAllowed)
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
