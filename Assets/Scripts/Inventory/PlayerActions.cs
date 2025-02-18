using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Script References")]
    public PlantGrowthManager m_plantGrowthManager;
    public InventoryManager m_inventoryManager;
    public MoneyManager m_moneyManager;
    
    [Header("Items Able to be Picked Up")]
    public Item[] m_itemsToPickup;

    [Header("Actions Allowed")]
    [HideInInspector] public bool m_wateringCanUsageAllowed;
    [HideInInspector] public bool m_hoeUsageAllowed;
    [HideInInspector] public bool m_itemSellingAllowed;

    [Header("Tool Options")]
    [SerializeField] private int m_wateringCanCapacity;
    
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
    /// <param name="id">The ID of the item to pick up. All IDs are stored in m_itemsToPickup.</param>
    public void PickUpItem(int id)
    {
        m_inventoryManager.AddItem(m_itemsToPickup[id]);
    }
    
    /// <summary>
    /// The specified plant is watered. The player physically watering the plant is handled in the specified plant prefab's PlantEventClick script.
    /// </summary>
    /// <param name="data">The data for the specified plant.</param>
    private void PlantIsWatered(PlantData data, ChangePlantSprite sprite)
    {
        if (m_wateringCanUsageAllowed == true && data.m_isDead == false)
        {
            if (data.m_waterLevel > data.m_waterCapacityCap)
            {
                Debug.Log("Plant can't take anymore water for now!");
            }
            else
            {
                data.m_waterLevel = data.m_waterLevel + m_wateringCanCapacity;
                Debug.Log($"Plant watered! New water level: {data.m_waterLevel}");
            }
            
            // Resets plant growth points timer if it was deactivated due to insufficient water
            if (m_plantGrowthManager.m_plantGrowthPointsTimerActive == false && data.m_isDead == false)
            {
                m_plantGrowthManager.m_plantGrowthPointsTimerActive = true;
                m_plantGrowthManager.PlantHealthCheck(data, sprite);
                StartCoroutine(m_plantGrowthManager.AddPlantGrowthPointsTimer(data));
                Debug.Log("Restarted AddPlantGrowthPointsTimer coroutine");
            }
        }
        else
        {
            Debug.Log("Watering can usage is not allowed, or the plant is dead!");
        }
    }

    private void PlantIsHarvested(PlantData data)
    {
        if (m_hoeUsageAllowed == true && data.m_readyToHarvest == true)
        {
            Destroy(data.gameObject);
            PickUpItem(2); // Beetroot is added to player inventory
        }
        else if (m_hoeUsageAllowed == true && data.m_isDead == true) // Player is not given crop if the plant is dead
        {
            Destroy(data.gameObject);
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
        if (m_itemSellingAllowed == true)
        {
            // Take 1 plant away from the user's inventory
            m_inventoryManager.UseItem();
            
            // Adds the plant's monetary value to their balance
            m_moneyManager.AddMoney(item.sellingValue);
        }
    }
}
