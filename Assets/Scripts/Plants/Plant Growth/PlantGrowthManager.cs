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

    // Script references
    PlantData m_plantData;
    ChangePlantSprite m_changePlantSprite;

    // An expandable list of all plants' plantData in the scene
    List<PlantData> plantDatasInSceneList = new List<PlantData>();
    List<ChangePlantSprite> changePlantSpritesInSceneList = new List<ChangePlantSprite>();

    // Conditions
    public bool m_plantGrowthPointsTimerActive = true;

    void Start() 
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
            plantDatasInSceneList.Add(m_plantData);
            changePlantSpritesInSceneList.Add(m_changePlantSprite);

            // Properties of plant are printed to console
            Debug.Log($"Plant created with the values:\nPlant ID: {plantDatasInSceneList[i].m_uniquePlantId}\nWater Level: {plantDatasInSceneList[i].m_waterLevel}\nPlant Growth Stage: {plantDatasInSceneList[i].m_growthStage}\nNeeds Water?: {plantDatasInSceneList[i].m_needsWater}\nIs Dying?: {plantDatasInSceneList[i].m_isDying}\nCan Grow?: {plantDatasInSceneList[i].m_canGrow}");
        }
        
        // For all plants in scene, start each plants' coroutines
        for (int j = 0; j < plantDatasInSceneList.Count; j++)
        {
            Debug.Log($"Starting coroutines for {plantDatasInSceneList[j].m_uniquePlantId}");
            
            // Checking if lists are within bounds of index (j)
            if (j < plantDatasInSceneList.Count && j < changePlantSpritesInSceneList.Count)
            {
                // Checking if an individual plants' script components are not null, so that coroutines can run
                if (plantDatasInSceneList[j] != null && changePlantSpritesInSceneList[j] != null)
                {
                    // Water level of individual plant starts decaying
                    StartCoroutine(WaterLevelDecayTimer(plantDatasInSceneList[j]));

                    // Individual plant's growth starts to be monitored
                    StartCoroutine(PlantGrowthTimer(plantDatasInSceneList[j], changePlantSpritesInSceneList[j]));

                    // Growth points start being added to specific plant
                    StartCoroutine(AddPlantGrowthPointsTimer(plantDatasInSceneList[j]));
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
        if (data.m_waterLevel <= 0)
        {
            data.m_needsWater = true;

            // Event is invoked so that needs water status indicator can react and display itself
            OnPlantNeedsWater?.Invoke();

            // Plant is also set to dying if water level is too far into the negatives
            if (data.m_waterLevel < -10)
            {
                data.m_isDying = true;
                OnPlantDying?.Invoke();
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
    private IEnumerator PlantGrowthTimer(PlantData data, ChangePlantSprite sprite)
    {
        while (true)
        {
            // Starts specific plant's growth timer
            yield return new WaitForSeconds(data.m_growthCheckTimer);

            // If plant health check is true AND the plant has the required growth points, so the plant meets the conditions for growing
            if (PlantHealthCheck(data) == true && data.m_growthPoints >= data.m_requiredGrowthPoints) 
            {
                data.m_canGrow = true;
                Debug.Log($"canGrow = {data.m_canGrow}");

                // Resets growth points
                data.m_growthPoints = 0;

                // This method actually allows the plant to grow
                PlantGrows(data, sprite);
            }
            // If plant health check is false, so the plant does not meet the conditions for growing
            else if (PlantHealthCheck(data) == false) 
            {
                data.m_canGrow = false;
                Debug.Log($"canGrow = {data.m_canGrow}");
            }
        }
    }

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
        // Resets canGrow after plant has grown
        data.m_canGrow = false;

        // If plant isn't fully grown, then add a growth stage and change the sprite
        if (data.m_growthStage <= 3)
        {
            data.m_growthStage++;
            sprite.ChangeSprite(data.m_growthStage);
            data.m_growthPoints = 0;
        }
        // If this criteria isn't met, do nothing
        else
        {

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
        Debug.Log($"ID: {data.m_uniquePlantId} Water Level: {data.m_waterLevel}");
        return data.m_waterLevel;
    }

    #endregion
}
