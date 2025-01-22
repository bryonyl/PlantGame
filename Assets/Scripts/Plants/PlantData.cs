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
    [SerializeField] public int m_waterLevel = 0;
    [SerializeField] public int m_growthStage = 0;

    // Timers
    [SerializeField] public float m_growthTimer = 0;
    [SerializeField] public float m_wateringTimer = 0;

    // Conditions
    public bool m_needsWater = false;
    public bool m_canGrow = false;
    public bool m_isDying = false;

    enum PlantDatabase
    {
        Wheat = 1,
        Beetroot = 2
    }
}