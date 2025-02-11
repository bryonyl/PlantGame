using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantData : MonoBehaviour
{
    public Guid UniquePlantId { get; } = Guid.NewGuid(); // Public property that holds a unique identifier for each instance of this class. Can only be read, not set anywhere else

    [Header("Plant Health")]
    public int waterLevel;
    public int waterCapacityCap;
    public int growthPoints;
    public int growthStage; // Maximum is 3
    public bool readyToHarvest = false;

    [Header("Timers")]
    public float growthCheckTimer;
    public float growthPointTimer;
    public float wateringDecayTimer;

    [Header("Conditions")]
    public float requiredGrowthPoints;
    [HideInInspector] public bool needsWater = false;
    [HideInInspector] public bool canGrow = false;
    [HideInInspector] public bool isDying = false;

    public void QueryPlant()
    {
        Debug.Log($"PLANT DEBUG\n\nPlant ID: {UniquePlantId}\n\nWater Level: {waterLevel}\nGrowth Stage: {growthStage}\n\nGrowth Timer: {growthCheckTimer}\nWatering Decay Timer: {wateringDecayTimer}" +
            $"\n\nNeeds Water?: {needsWater}\nCan Grow?: {canGrow}\nIs Dying?: {isDying}");
    }
}