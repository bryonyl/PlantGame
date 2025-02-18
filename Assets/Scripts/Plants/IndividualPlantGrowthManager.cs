using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script manages plants on an individual scale, not globally, so this script should only be attached to individual plants. It is responsible for the plant's needs, like watering, and its growth and death. It also triggers the appropriate plant status indicator
/// </summary>
public class IndividualPlantGrowthManager : MonoBehaviour
{
    // Events
    public event Action OnPlantHappy;
    public event Action OnPlantNeedsWater;
    public delegate void PlantDead(PlantData data);
    public static event PlantDead OnPlantDead;
    public event Action OnPlantDead_SpecificToIndicator; // Needed to make a new event specifically for indicators to react to because the event above is static, but changing it to not static breaks a lot of things. This is not the best approach I think but unfortunately I don't have a lot of time left to figure out a better way
    
    // Script references
    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;
    public PlantingAreaClickHandler m_plantingAreaClickHandler;
    
    // Game object references
    private GameObject m_plantStatusIndicator;
    
    // Conditions
    public bool m_plantGrowthPointsTimerActive = true;
    private bool m_waterLevelDecayTimerActive = true;
    private bool m_plantGrowthTimerActive = true;

    private void OnEnable()
    {
        // Setting up planting areas to be able to spawn plants
        m_plantingAreaClickHandler = GameObject.Find("PlantingArea").GetComponent<PlantingAreaClickHandler>();
        
        GameObject plant = m_plantingAreaClickHandler.m_plantToSpawn;
        PlantingAreaClickHandler.OnPlantPlanted += SetUpPlant;
    }

    private void OnDisable()
    {
        PlantingAreaClickHandler.OnPlantPlanted -= SetUpPlant;
    }

    private void SetUpPlant(GameObject plant)
    {
        m_plantData = GetComponent<PlantData>();
        m_changePlantSprite = GetComponent<ChangePlantSprite>();
        m_plantStatusIndicator = plant.transform.Find("PlantStatusIndicator").gameObject;

        Debug.Log($"Plant created with the ID {m_plantData.UniquePlantId}. Starting coroutines");
        
        // Water level of plant starts decaying
        StartCoroutine(WaterLevelDecayTimer());

        // Plant's growth starts to be monitored
        StartCoroutine(PlantGrowthTimer());

        // Growth points start being added to plant
        StartCoroutine(AddPlantGrowthPointsTimer());
    }

    /// <summary>
    /// Checks if the plant needs water and/or if it is dying
    /// </summary>
    /// <returns>Returns false if plant is dying and needs care. Returns true if plant is healthy</returns>
    public bool PlantHealthCheck()
    {
        if (m_plantData.m_waterLevel <= 0 && m_plantData.m_waterLevel >= -15)
        {
            m_plantData.m_needsWater = true;

            // Event is invoked so that needs water status indicator can react and display itself
            OnPlantNeedsWater?.Invoke();

            return false; // Plant needs care, so false is returned (health check came back unsuccessful)
        }
        if (m_plantData.m_waterLevel < -15) // Plant dies if water level falls below -15
        {
            PlantDies();

            return false;
        }
        
        m_plantData.m_needsWater = false;
        m_plantData.m_isDead = false;
        OnPlantHappy?.Invoke();

        return true; // Plant is healthy, so true is returned (health check came back successful)
    }
    
    /// <summary>
    /// Checks if conditions have been met for the plant to be able to grow, and makes plant grow
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlantGrowthTimer()
    {
        while (m_plantGrowthTimerActive)
        {
            // Starts specific plant's growth timer
            yield return new WaitForSeconds(m_plantData.m_growthCheckTimer);

            // If plant health check is true AND the plant has the required growth points, so the plant meets the conditions for growing
            if (PlantHealthCheck() == true && m_plantData.m_growthPoints >= m_plantData.m_requiredGrowthPoints) 
            {
                m_plantData.m_canGrow = true;
                Debug.Log($"canGrow = {m_plantData.m_canGrow}");

                // Resets growth points
                m_plantData.m_growthPoints = 0;

                // This method actually allows the plant to grow
                PlantGrows();
            }
            // If plant health check is false, so the plant does not meet the conditions for growing
            else if (PlantHealthCheck() == false) 
            {
                m_plantData.m_canGrow = false;
                Debug.Log($"canGrow = {m_plantData.m_canGrow}");
            }
        }
    }

    /// <summary>
    /// Timer that allows growth points to be added each time it is called
    /// </summary>
    /// <returns></returns>
    public IEnumerator AddPlantGrowthPointsTimer()
    {
        while (m_plantGrowthPointsTimerActive == true)
        {
            // If plant does not need water
            if (m_plantData.m_needsWater == false)
            {
                // Allow growth points to be added to plant's growth points
                yield return new WaitForSeconds(m_plantData.m_growthPointTimer);
                AddPlantGrowthPoints();
            }
            else
            {
                // Timer stops
                m_plantGrowthPointsTimerActive = false;
                yield break;
            }
        }
    }

    /// <summary>
    /// Grows plant by 1 stage each time, up until the 3rd stage, when it is fully grown and all growth related timers are stopped
    /// </summary>
    private void PlantGrows()
    {
        // Resets m_canGrow after plant has grown a stage
        m_plantData.m_canGrow = false;

        // If plant isn't fully grown, then add a growth stage and change the sprite
        if (m_plantData.m_growthStage <= 1)
        {
            m_plantData.m_growthStage++;
            m_changePlantSprite.ChangeSprite(m_plantData.m_growthStage);
            m_plantData.m_growthPoints = 0;
        }
        // Plant has finished growing. All timers are stopped
        else if (m_plantData.m_growthStage == 2)
        {
            m_plantData.m_growthStage++;
            m_changePlantSprite.ChangeSprite(m_plantData.m_growthStage);
            m_plantData.m_growthPoints = 0;
            StopAllCoroutines();
            Destroy(m_plantStatusIndicator);
            Debug.Log("Plant has finished growing!");
            m_plantData.m_readyToHarvest = true;
        }
    }

    /// <summary>
    /// Adds plant growth points to a plant
    /// </summary>
    private void AddPlantGrowthPoints()
    {
        Debug.Log($"New growth points: {m_plantData.m_growthPoints}");
        m_plantData.m_growthPoints++;
    }

    /// <summary>
    /// Plant dies and all timers are stopped
    /// </summary>
    private void PlantDies()
    {
        m_plantData.m_isDead = true;
        m_changePlantSprite.ChangeSpriteToDead();
        StopAllCoroutines();
        OnPlantDead?.Invoke(m_plantData);
        OnPlantDead_SpecificToIndicator?.Invoke();
    }
    
    /// <summary>
    /// Timer determining how frequently the water level of a plant should be decayed
    /// </summary>
    private IEnumerator WaterLevelDecayTimer()
    {
        while (m_waterLevelDecayTimerActive)
        {
            // Starts specific plant's water decay timer
            yield return new WaitForSeconds(m_plantData.m_wateringDecayTimer);

            // This method actually decays the water level
            WaterLevelDecay();
        }
    }

    /// <summary>
    /// Decays the water level of a plant
    /// </summary>
    /// <returns></returns>
    private int WaterLevelDecay()
    {
        m_plantData.m_waterLevel--;
        Debug.Log($"ID: {m_plantData.UniquePlantId} Water Level: {m_plantData.m_waterLevel}");
        return m_plantData.m_waterLevel;
    }
}
