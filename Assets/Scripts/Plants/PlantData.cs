using System;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    [Header("Identification")]
    private string m_plantName = "";
    public Guid m_uniquePlantId { get; } = Guid.NewGuid(); // Public property that holds a unique identifier for each instance of this class. Can only be read, not set anywhere else

    [Header("Selling")]
    // Money related variables
    private float m_plantSellingValue = 0f;

    [Header("Plant Health")]
    public int m_waterLevel = 0;
    public int m_growthPoints = 0;
    public int m_growthStage = 0;

    [Header("Timers")]
    public float m_growthCheckTimer = 0;
    public float m_growthPointTimer = 0;
    public float m_wateringDecayTimer = 0;

    [Header("Conditions")]
    public float m_requiredGrowthPoints = 0;
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
        Debug.Log($"PLANT DEBUG\n\nPlant ID: {m_uniquePlantId}\n\nWater Level: {m_waterLevel}\nGrowth Stage: {m_growthStage}\n\nGrowth Timer: {m_growthCheckTimer}\nWatering Decay Timer: {m_wateringDecayTimer}" +
            $"\n\nNeeds Water?: {m_needsWater}\nCan Grow?: {m_canGrow}\nIs Dying?: {m_isDying}\nRecently Watered?: {m_recentlyWatered}");
    }
}