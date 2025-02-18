using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Script References")]
    public IndividualPlantGrowthManager m_individualPlantGrowthManager;
    public InventoryManager m_inventoryManager;
    public MoneyManager m_moneyManager;
    public Animator m_playerAnimator;
    
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

    private void Start()
    {
        PickUpItem(0); // Obtain watering can at start of game
        PickUpItem(1); // Obtain hoe at start of game
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
    private void PlantIsWatered(PlantData data, IndividualPlantGrowthManager growthManager)
    {
        if (m_wateringCanUsageAllowed == true && data.m_isDead == false)
        {
            m_playerAnimator.ResetTrigger("Watering");
            m_playerAnimator.SetTrigger("Watering");
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
            if (growthManager.m_plantGrowthPointsTimerActive == false && data.m_isDead == false)
            {
                growthManager.m_plantGrowthPointsTimerActive = true;
                growthManager.PlantHealthCheck();
                StartCoroutine(growthManager.AddPlantGrowthPointsTimer());
                Debug.Log("Restarted AddPlantGrowthPointsTimer coroutine");
            }
            m_playerAnimator.ResetTrigger("Watering");
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
            m_moneyManager.AddMoney(item.m_sellingValue);
        }
    }
}
