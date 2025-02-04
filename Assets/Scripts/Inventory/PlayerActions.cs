using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Script References")]
    public PlantGrowthManager plantGrowthManager;
    public InventoryManager inventoryManager;
    
    [Header("Items Able to be Picked Up")]
    public Item[] itemsToPickup;

    [Header("Actions Allowed")]
    [HideInInspector] public bool wateringCanUsageAllowed;
    [HideInInspector] public bool hoeUsageAllowed;
    [HideInInspector] public bool plantSellingAllowed;
    
    private void OnEnable()
    {
        PlantHandleClick.OnPlantWatered += PlantIsWatered;
    }
    
    private void OnDisable()
    {
        PlantHandleClick.OnPlantWatered -= PlantIsWatered;
    }

    /// <summary>
    /// Picks up a specified item via the AddItem method in InventoryManager.cs.
    /// </summary>
    /// <param name="id">The ID of the item to pick up. All IDs are stored in itemsToPickup.</param>
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
    /// The specified plant is watered. The player physically watering the plant is handled in the specified plant prefab's EventClick script.
    /// </summary>
    /// <param name="data">The data for the specified plant.</param>
    private void PlantIsWatered(PlantData data)
    {
        if (wateringCanUsageAllowed == true)
        {
            data.m_waterLevel = data.m_waterLevel + 50;
            Debug.Log($"Plant watered! New water level: {data.m_waterLevel}");

            if (plantGrowthManager.plantGrowthPointsTimerActive == false)
            {
                plantGrowthManager.plantGrowthPointsTimerActive = true;
                plantGrowthManager.PlantHealthCheck(data);
                StartCoroutine(plantGrowthManager.AddPlantGrowthPointsTimer(data));
                Debug.Log("Restarted AddPlantGrowthPointsTimer coroutine");
            }
        }
    }

    private void PlantIsSold(PlantData data, Item item)
    {
        if (plantSellingAllowed == true)
        {
            
        }
    }
}
