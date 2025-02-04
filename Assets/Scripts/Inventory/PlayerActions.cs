using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    /// <summary>
    /// Picks up an item via its ID via the AddItem method in InventoryManager.cs.
    /// </summary>
    /// <param name="id">The ID of the item to pick up.</param>
    public void PickUpItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    /// <summary>
    /// Accesses the player's selected item via the GetSelectedItem method in InventoryManager.cs.
    /// </summary>
    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log($"Received item: {receivedItem}");
        }
        else
        {
            Debug.Log("No item received");
        }
    }

    /// <summary>
    /// Expends the player's selected item via the GetSelectedItem method in InventoryManager.cs.
    /// </summary>
    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log($"Used item: {receivedItem}");
        }
        else
        {
            Debug.Log("No item used");
        }
    }
}
