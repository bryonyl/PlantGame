using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    // Events
    public static event Action OnPlantHappy;
    public static event Action OnPlantFinishedGrowing;
    public static event Action OnPlantNeedsWater;
    public static event Action OnPlantDying;
    public static event Action OnPlantIsDead;
    
    // Script references
    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;
    private PlantingAreaClickHandler m_plantingAreaClickHandler;

    // Lists
    private List<GameObject> m_plantsInSceneList = new();

    // Conditions
    public bool m_plantGrowthPointsTimerActive = true;

    private void OnEnable()
    {
        // Setting up planting areas to be able to spawn plants
        m_plantingAreaClickHandler = GameObject.Find("PlantingArea").GetComponent<PlantingAreaClickHandler>();
        GameObject plant = m_plantingAreaClickHandler.m_plantToSpawn;
        PlantingAreaClickHandler.OnPlantPlanted += SetUpIndividualPlant;
    }

    private void OnDisable()
    {
        PlantingAreaClickHandler.OnPlantPlanted -= SetUpIndividualPlant;
    }

    void Start() 
    {
        SetUpManager();
    }
    
    /// <summary>
    /// Identifies number of plants in scene and sets up each plant with its own components and coroutines
    /// </summary>
    private void SetUpManager()
    {
        // Identifies number of plants in scene
        IdentifyPlantsInScene();

        // Sets up each individual plant with their own coroutines
        for (int i = 0; i < m_plantsInSceneList.Count(); i++)
        {
            SetUpIndividualPlant(m_plantsInSceneList[i]);
        }
    }

    /// <summary>
    /// Sets up an individual plant with its own components and coroutines
    /// </summary>
    /// <param name="newPlant">Plant to be set up</param>
    private void SetUpIndividualPlant(GameObject newPlant)
    {
        if (newPlant == null)
        {
            return;
        }
        // Grab PlantData and ChangePlantSprite component from individual plant
        m_plantData = newPlant.GetComponent<PlantData>();

        if (m_plantData == null)
        {
            Debug.LogError($"{newPlant}'s Plant Data component is null!");
        }
        
        m_changePlantSprite = newPlant.GetComponent<ChangePlantSprite>();

        if (m_changePlantSprite == null)
        {
            Debug.LogError($"{newPlant}'s Change Plant Sprite component is null!");
        }
        
        Debug.Log($"Plant created with the ID {m_plantData.UniquePlantId}");
        Debug.Log($"Starting coroutines for {m_plantData.UniquePlantId}");

        if (m_plantData != null && m_changePlantSprite != null)
        {
            // Water level of individual plant starts decaying
            StartCoroutine(WaterLevelDecayTimer(m_plantData));

            // Individual plant's growth starts to be monitored
            StartCoroutine(PlantGrowthTimer(m_plantData, m_changePlantSprite));

            // Growth points start being added to specific plant
            StartCoroutine(AddPlantGrowthPointsTimer(m_plantData));
        }
        else
        {
            Debug.LogError("Plant Data and/or Change Plant Sprite components are null!");
        }
    }

    /// <summary>
    /// Checks what game objects in the scene are plants (have the tag "Plant")
    /// </summary>
    private void IdentifyPlantsInScene()
    {
        // Identifies number of plants in scene via array
        GameObject[] plantsInSceneArr = GameObject.FindGameObjectsWithTag("Plant");
        
        // Adds plants found in scene to plants in scene list (a list so that more plants can be added later)
        foreach (GameObject plant in plantsInSceneArr)
        {
            Debug.Log($"Plant found: {plant}");
            m_plantsInSceneList.Add(plant);
        }
    }

    #region Plant Health Check Method

    /// <summary>
    /// Checks if the plant needs water and/or if it is dying
    /// </summary>
    /// <param name="data">The specific plant's PlantData component</param>
    /// <param name="sprite">The specific plant's ChangePlantSprite component</param>
    /// <returns>Returns false if plant is dying and needs care. Returns true if plant is healthy</returns>
    public bool PlantHealthCheck(PlantData data, ChangePlantSprite sprite)
    {
        if (data.m_waterLevel <= 0)
        {
            data.m_needsWater = true;

            // Event is invoked so that needs water status indicator can react and display itself
            OnPlantNeedsWater?.Invoke();

            // Plant is also set to dying if water level is too far into the negatives
            if (data.m_waterLevel <= -10)
            {
                data.m_isDying = true;
                OnPlantDying?.Invoke();
            }
            // Plant dies at a water level of -20
            else if (data.m_waterLevel <= -20)
            {
                data.m_isDead = true;
                data.m_isDying = false;
                OnPlantIsDead?.Invoke();
                StopCoroutine(AddPlantGrowthPointsTimer(data));
                StopCoroutine(PlantGrowthTimer(data, sprite));
                StopCoroutine(WaterLevelDecayTimer(data));
            }

            return false; // Plant is dying and needs care
        }
        else
        {
            data.m_needsWater = false;
            data.m_isDying = false;

            // Event is invoked so that happy status indicator can react and display itself
            OnPlantHappy?.Invoke();

            return true; // Plant is healthy
        }
    }

    #endregion

    #region Plant Growth Methods

    // Plant grows, so its m_growthStage increments by 1 and its sprite changes

    // Checks if the conditions have been met for the plant to be able to grow
    
    /// <summary>
    /// Checks if conditions have been met for the plant to be able to grow, and makes plant grow
    /// </summary>
    /// <param name="data">Specific plant's PlantData component</param>
    /// <param name="sprite">Specific plant's ChangePlantSprite component</param>
    /// <returns></returns>
    private IEnumerator PlantGrowthTimer(PlantData data, ChangePlantSprite sprite)
    {
        while (true)
        {
            // Starts specific plant's growth timer
            yield return new WaitForSeconds(data.m_growthCheckTimer);

            // If plant health check is true AND the plant has the required growth points, so the plant meets the conditions for growing
            if (PlantHealthCheck(data, sprite) == true && data.m_growthPoints >= data.m_requiredGrowthPoints) 
            {
                data.m_canGrow = true;
                Debug.Log($"canGrow = {data.m_canGrow}");

                // Resets growth points
                data.m_growthPoints = 0;

                // This method actually allows the plant to grow
                PlantGrows(data, sprite);
            }
            // If plant health check is false, so the plant does not meet the conditions for growing
            else if (PlantHealthCheck(data, sprite) == false) 
            {
                data.m_canGrow = false;
                Debug.Log($"canGrow = {data.m_canGrow}");
            }
        }
    }

    /// <summary>
    /// Timer that allows growth points to be added each time it is called
    /// </summary>
    /// <param name="data">Specific plant's PlantData component</param>
    /// <returns></returns>
    public IEnumerator AddPlantGrowthPointsTimer(PlantData data)
    {
        while (m_plantGrowthPointsTimerActive == true)
        {
            // If plant does not need water
            if (data.m_needsWater != true)
            {
                // Allow growth points to be added to plant's growth points
                yield return new WaitForSeconds(data.m_growthPointTimer);
                AddPlantGrowthPoints(data);
            }
            else
            {
                // Timer stops
                m_plantGrowthPointsTimerActive = false;
                yield break;
            }
        }
    }

    private void PlantGrows(PlantData data, ChangePlantSprite sprite)
    {
        // Resets m_canGrow after plant has grown
        data.m_canGrow = false;

        // If plant isn't fully grown, then add a growth stage and change the sprite
        if (data.m_growthStage <= 2)
        {
            data.m_growthStage++;
            sprite.ChangeSprite(data.m_growthStage);
            data.m_growthPoints = 0;
        }
        else if (data.m_growthStage == 3)
        {
            Debug.Log("Plant has finished growing!");
            OnPlantFinishedGrowing?.Invoke();
            StopCoroutine(AddPlantGrowthPointsTimer(data));
            StopCoroutine(PlantGrowthTimer(data, sprite));
            StopCoroutine(WaterLevelDecayTimer(data));
            data.m_readyToHarvest = true;
        }
    }

    // Adds plant growth point to specific plant
    private void AddPlantGrowthPoints(PlantData data)
    {
        Debug.Log($"New growth points: {data.m_growthPoints}");
        data.m_growthPoints++;
    }

    #endregion

    #region Water Level Decay Methods

    // Decays specific plant's water level over time
    private IEnumerator WaterLevelDecayTimer(PlantData data)
    {
        while (true)
        {
            // Starts specific plant's water decay timer
            yield return new WaitForSeconds(data.m_wateringDecayTimer);

            // This method actually decays the water level
            WaterLevelDecay(data);
        }
    }

    // Actually decays plant's water level
    private int WaterLevelDecay(PlantData data)
    {
        data.m_waterLevel--;
        Debug.Log($"ID: {data.UniquePlantId} Water Level: {data.m_waterLevel}");
        return data.m_waterLevel;
    }

    #endregion
}
