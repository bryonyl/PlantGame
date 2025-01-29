 using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    // Called from the outside, gets information about what item should be added and will search for any unoccupied slot in the inventory
    public void AddItem(Item item)
    {
        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            //Inventory slot = inventorySlots[i]; -- 13:15 in tut video
        }
    }

    // When item is found from AddItem, then this method, SpawnNewItem, will spawn a new item in the inventory slot using info from AddItem
    void SpawnNewItem(Item item, InventorySlot slot)
    {

    }
}
