using System;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    // Identification variables
    private string m_plantName = "";
    public Guid m_uniquePlantId { get; } = Guid.NewGuid(); // Public property that holds a unique identifier for each instance of this class. Can only be read, not set anywhere else

    // Money related variables
    private float m_plantSellingValue = 0f;

    // Care related variables
    public int m_waterLevel = 0;
    public int m_growthStage = 0;

    // Timers
    public float m_growthTimer = 0;
    public float m_wateringDecayTimer = 0;
    public float m_timePassedSinceWatered = 0;

    // Conditions
    public bool m_needsWater = false;
    public bool m_canGrow = false;
    public bool m_isDying = false;
    public bool m_recentlyWatered = false;

    enum PlantDatabase
    {
        Wheat = 1,
        Beetroot = 2
    }

    public void QueryPlant()
    {
        Debug.Log($"PLANT DEBUG\n\nPlant ID: {m_uniquePlantId}\n\nWater Level: {m_waterLevel}\nGrowth Stage: {m_growthStage}\n\nGrowth Timer: {m_growthTimer}\nWatering Decay Timer: {m_wateringDecayTimer}" +
            $"\nTime Passed Since Last Watered: {m_timePassedSinceWatered}\n\nNeeds Water?: {m_needsWater}\nCan Grow?: {m_canGrow}\nIs Dying?: {m_isDying}\nRecently Watered?: {m_recentlyWatered}");
    }
}