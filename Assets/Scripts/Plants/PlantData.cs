using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantData : MonoBehaviour
{
    public Guid UniquePlantId { get; } = Guid.NewGuid(); // Public property that holds a unique identifier for each instance of this class. Can only be read, not set anywhere else

    [Header("Plant Health")]
    public int m_waterLevel;
    public int m_waterCapacityCap;
    public int m_growthPoints;
    public int m_growthStage; // Maximum is 3
    [HideInInspector] public bool m_readyToHarvest = false;

    [Header("Timers")]
    public float m_growthCheckTimer;
    public float m_growthPointTimer;
    public float m_wateringDecayTimer;

    [Header("Conditions")]
    public float m_requiredGrowthPoints;
    [HideInInspector] public bool m_needsWater = false;
    [HideInInspector] public bool m_canGrow = false;
    [HideInInspector] public bool m_isDead = false;
}