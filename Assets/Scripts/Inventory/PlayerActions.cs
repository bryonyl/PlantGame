using UnityEngine;
using UnityEngine.Serialization;

public class PlayerActions : MonoBehaviour
{
    [Header("Script References")]
    public PlantGrowthManager plantGrowthManager;
    public InventoryManager inventoryManager;
    public MoneyManager moneyManager;
    
    [Header("Items Able to be Picked Up")]
    public Item[] itemsToPickup;

    [Header("Actions Allowed")]
    [HideInInspector] public bool wateringCanUsageAllowed;
    [HideInInspector] public bool hoeUsageAllowed;
    [HideInInspector] public bool itemSellingAllowed;

    [Header("Tool Options")]
    [SerializeField] private int wateringCanCapacity;
    
    private void OnEnable()
    {
        PlantClickHandler.OnPlantWatered += PlantIsWatered;
        PlantClickHandler.OnPlantHarvested += PlantIsHarvested;
    }
    
    private void OnDisable()
    {
        PlantClickHandler.OnPlantWatered -= PlantIsWatered;
        PlantClickHandler.OnPlantHarvested -= PlantIsHarvested;
    }

    /// <summary>
    /// Picks up a specified item via the AddItem method in InventoryManager.cs.
    /// </summary>
    /// <param name="id">The ID of the item to pick up. All IDs are stored in itemsToPickup.</param>
    public void PickUpItem(int id)
    {
        inventoryManager.AddItem(itemsToPickup[id]);
    }
    
    /// <summary>
    /// The specified plant is watered. The player physically watering the plant is handled in the specified plant prefab's PlantEventClick script.
    /// </summary>
    /// <param name="data">The data for the specified plant.</param>
    private void PlantIsWatered(PlantData data)
    {
        if (wateringCanUsageAllowed == true)
        {
            if (data.waterLevel > data.waterCapacityCap)
            {
                Debug.Log("Water Capacity Reached");
            }
            else
            {
                data.waterLevel = data.waterLevel + wateringCanCapacity;
                Debug.Log($"Plant watered! New water level: {data.waterLevel}");
            }
            
            // Resets plant growth points timer if it was deactivated due to insufficient water
            if (plantGrowthManager.plantGrowthPointsTimerActive == false)
            {
                plantGrowthManager.plantGrowthPointsTimerActive = true;
                plantGrowthManager.PlantHealthCheck(data);
                StartCoroutine(plantGrowthManager.AddPlantGrowthPointsTimer(data));
                Debug.Log("Restarted AddPlantGrowthPointsTimer coroutine");
            }
        }
    }

    private void PlantIsHarvested(PlantData data)
    {
        if (hoeUsageAllowed == true && data.readyToHarvest == true)
        {
            Debug.Log("Player can harvest plant!");
        }
        else
        {
            Debug.Log("Plant does not meet the criteria to be harvested.");
        }
    }

    /// <summary>
    /// The specified item is sold. The player physically selling the item is handled in ShippingBox.cs.
    /// </summary>
    /// <param name="item">The item to be sold.</param>
    public void ItemIsSold(Item item)
    {
        if (itemSellingAllowed == true)
        {
            // Take 1 plant away from the user's inventory
            inventoryManager.UseItem();
            
            // Adds the plant's monetary value to their balance
            moneyManager.AddMoney(item.sellingValue);
        }
    }
}
