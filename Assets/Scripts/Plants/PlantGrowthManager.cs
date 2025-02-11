using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    // Events
    public static event Action OnPlantNeedsWater;
    public static event Action OnPlantDying;
    public static event Action OnPlantIsDead;
    public static event Action OnPlantHappy;
    public static event Action OnPlantCanBeHarvested;

    // Script references
    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;

    // An expandable list of all plants' plantData in the scene
    List<PlantData> m_plantDatasInSceneList = new();
    List<ChangePlantSprite> m_changePlantSpritesInSceneList = new();

    // Conditions
    public bool plantGrowthPointsTimerActive = true;

    private void OnEnable()
    {
        
    }
    
    void Start() 
    {
        SetUp();
    }
    
    private void SetUp()
    {
        // Identifies number of plants in scene
        GameObject[] plantsInSceneArr = GameObject.FindGameObjectsWithTag("Plant");

        // For all plants in scene, grab PlantData and ChangePlantSprite component from each of them
        for (int i = 0; i < plantsInSceneArr.Count(); i++)
        {
            // Grab PlantData and ChangePlantSprite component from each individual plant
            m_plantData = plantsInSceneArr[i].GetComponent<PlantData>();
            m_changePlantSprite = plantsInSceneArr[i].GetComponent<ChangePlantSprite>();

            // Add the individual components to two lists
            m_plantDatasInSceneList.Add(m_plantData);
            m_changePlantSpritesInSceneList.Add(m_changePlantSprite);

            // Properties of plant are printed to console
            Debug.Log($"Plant created with the values:\nPlant ID: {m_plantDatasInSceneList[i].UniquePlantId}\nWater Level: {m_plantDatasInSceneList[i].waterLevel}\nPlant Growth Stage: {m_plantDatasInSceneList[i].growthStage}\nNeeds Water?: {m_plantDatasInSceneList[i].needsWater}\nIs Dying?: {m_plantDatasInSceneList[i].isDying}\nCan Grow?: {m_plantDatasInSceneList[i].canGrow}");
        }
        
        // For all plants in scene, start each plants' coroutines
        for (int j = 0; j < m_plantDatasInSceneList.Count; j++)
        {
            Debug.Log($"Starting coroutines for {m_plantDatasInSceneList[j].UniquePlantId}");
            
            // Checking if lists are within bounds of index (j)
            if (j < m_plantDatasInSceneList.Count && j < m_changePlantSpritesInSceneList.Count)
            {
                // Checking if an individual plants' script components are not null, so that coroutines can run
                if (m_plantDatasInSceneList[j] != null && m_changePlantSpritesInSceneList[j] != null)
                {
                    // Water level of individual plant starts decaying
                    StartCoroutine(WaterLevelDecayTimer(m_plantDatasInSceneList[j]));

                    // Individual plant's growth starts to be monitored
                    StartCoroutine(PlantGrowthTimer(m_plantDatasInSceneList[j], m_changePlantSpritesInSceneList[j]));

                    // Growth points start being added to specific plant
                    StartCoroutine(AddPlantGrowthPointsTimer(m_plantDatasInSceneList[j]));
                }
                else
                {
                    Debug.LogError("One of the script components is null!");
                }
            }
            else
            {
                Debug.LogError("Index out of bounds!");
            }
        }
    }

    #region Plant Health Check Method

    // Checks if the plant needs water and whether it is dying
    public bool PlantHealthCheck(PlantData data)
    {
        if (data.waterLevel <= 0)
        {
            data.needsWater = true;

            // Event is invoked so that needs water status indicator can react and display itself
            OnPlantNeedsWater?.Invoke();

            // Plant is also set to dying if water level is too far into the negatives
            if (data.waterLevel < -10)
            {
                data.isDying = true;
                OnPlantDying?.Invoke();
            }

            return false; // Plant is dying and needs care
        }
        else
        {
            data.needsWater = false;
            data.isDying = false;

            // Event is invoked so that happy status indicator can react and display itself
            OnPlantHappy?.Invoke();

            return true; // Plant is healthy
        }
    }

    #endregion

    #region Plant Growth Methods

    // Plant grows, so its growthStage increments by 1 and its sprite changes

    // Checks if the conditions have been met for the plant to be able to grow
    private IEnumerator PlantGrowthTimer(PlantData data, ChangePlantSprite sprite)
    {
        while (true)
        {
            // Starts specific plant's growth timer
            yield return new WaitForSeconds(data.growthCheckTimer);

            // If plant health check is true AND the plant has the required growth points, so the plant meets the conditions for growing
            if (PlantHealthCheck(data) == true && data.growthPoints >= data.requiredGrowthPoints) 
            {
                data.canGrow = true;
                Debug.Log($"canGrow = {data.canGrow}");

                // Resets growth points
                data.growthPoints = 0;

                // This method actually allows the plant to grow
                PlantGrows(data, sprite);
            }
            // If plant health check is false, so the plant does not meet the conditions for growing
            else if (PlantHealthCheck(data) == false) 
            {
                data.canGrow = false;
                Debug.Log($"canGrow = {data.canGrow}");
            }
        }
    }

    public IEnumerator AddPlantGrowthPointsTimer(PlantData data)
    {
        while (plantGrowthPointsTimerActive == true)
        {
            // If plant does not need water
            if (data.needsWater != true)
            {
                // Allow growth points to be added to plant's growth points
                yield return new WaitForSeconds(data.growthPointTimer);
                AddPlantGrowthPoints(data);
            }
            else
            {
                // Timer stops
                plantGrowthPointsTimerActive = false;
                yield break;
            }
        }
    }

    private void PlantGrows(PlantData data, ChangePlantSprite sprite)
    {
        // Resets canGrow after plant has grown
        data.canGrow = false;

        // If plant isn't fully grown, then add a growth stage and change the sprite
        if (data.growthStage <= 2)
        {
            data.growthStage++;
            sprite.ChangeSprite(data.growthStage);
            data.growthPoints = 0;
        }
        else if (data.growthStage == 3)
        {
            Debug.Log("Plant has finished growing!");
            StopCoroutine(AddPlantGrowthPointsTimer(data));
            StopCoroutine(PlantGrowthTimer(data, sprite));
            StopCoroutine(WaterLevelDecayTimer(data));
            data.readyToHarvest = true;
        }
    }

    // Adds plant growth point to specific plant
    private void AddPlantGrowthPoints(PlantData data)
    {
        Debug.Log($"New growth points: {data.growthPoints}");
        data.growthPoints++;
    }

    #endregion

    #region Water Level Decay Methods

    // Decays specific plant's water level over time
    private IEnumerator WaterLevelDecayTimer(PlantData data)
    {
        while (true)
        {
            // Starts specific plant's water decay timer
            yield return new WaitForSeconds(data.wateringDecayTimer);

            // This method actually decays the water level
            WaterLevelDecay(data);
        }
    }

    // Actually decays plant's water level
    private int WaterLevelDecay(PlantData data)
    {
        data.waterLevel--;
        Debug.Log($"ID: {data.UniquePlantId} Water Level: {data.waterLevel}");
        return data.waterLevel;
    }

    #endregion
}
