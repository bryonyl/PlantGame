using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    public static event Action OnPlantNeedsWater;
    public static event Action OnPlantDying;
    public static event Action OnPlantIsDead;
    public static event Action OnPlantHappy;

    PlantData plantData;
    List<PlantData> plantsInScenePlantDataList = new List<PlantData>();

    private void OnEnable()
    {
        PlantHandleClick.OnPlantWatered += WaterPlant;
    }

    private void OnDisable()
    {
        PlantHandleClick.OnPlantWatered -= WaterPlant;
    }

    void Start() // Identifies number of plants in scene and grabs PlantData component from each of them
    {
        GameObject[] plantsInSceneArr = GameObject.FindGameObjectsWithTag("Plant");

        for (int i = 0; i < plantsInSceneArr.Count(); i++)
        {
            plantData = plantsInSceneArr[i].GetComponent<PlantData>();
            plantsInScenePlantDataList.Add(plantData);           

            Debug.Log($"Plant created with the values:\nPlant ID: {plantsInScenePlantDataList[i].m_uniquePlantId}\nWater Level: {plantsInScenePlantDataList[i].m_waterLevel}\nPlant Growth Stage: {plantsInScenePlantDataList[i].m_growthStage}\nNeeds Water?: {plantsInScenePlantDataList[i].m_needsWater}\nIs Dying?: {plantsInScenePlantDataList[i].m_isDying}\nCan Grow?: {plantsInScenePlantDataList[i].m_canGrow}");
        }

        for (int j = 0; j < plantsInScenePlantDataList.Count; j++) // Coroutines for individual plants are started
        {
            Debug.Log($"Starting coroutines for {plantsInScenePlantDataList[j].m_uniquePlantId}");
            StartCoroutine(WaterLevelDecayTimer(plantsInScenePlantDataList[j]));
            StartCoroutine(PlantGrowthTimer(plantsInScenePlantDataList[j]));
        }
    }

    // Coroutines
    private IEnumerator PlantGrowthTimer(PlantData data) // Checks if the conditions have been met for the plant to be able to grow
    {
        while (true)
        {
            yield return new WaitForSeconds(data.m_growthTimer);
            if (PlantHealthCheck(data) == true) // If plant health check is true, so the plant meets the conditions for growing
            {
                data.m_canGrow = true;
                Debug.Log($"canGrow = {data.m_canGrow}");
                PlantGrows(data);
            }
            else if (PlantHealthCheck(data) == false) // If plant health check is false, so the plant does not meet the conditions for growing
            {
                data.m_canGrow = false;
                Debug.Log($"canGrow = {data.m_canGrow}");
            }
        }
    }

    private IEnumerator WaterLevelDecayTimer(PlantData data)
    {
        while (true)
        {
            //maybe decrease time between loops
            yield return new WaitForSeconds(data.m_wateringDecayTimer);
            WaterLevelDecay(data);
        }
    }

    private IEnumerator TimePassedSinceWatered(PlantData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.m_timePassedSinceWatered++);
            if (data.m_recentlyWatered == true)
            {
                data.m_recentlyWatered = false;
                yield break;
            }
        }
    }

    // Checks
    private bool PlantHealthCheck(PlantData data)
    {
        if (data.m_waterLevel <= 0)
        {
            data.m_needsWater = true;
            OnPlantNeedsWater?.Invoke();
            if (data.m_waterLevel < -10)
            {
                data.m_isDying = true;
                OnPlantDying?.Invoke();
            }
            Debug.Log($"PLANT STATUS\nWater Level = {data.m_waterLevel}\nNeeds Water = {data.m_needsWater}\nIs Dying = {data.m_isDying}");
            return false; // Plant is dying and needs care
        }
        else
        {
            data.m_needsWater = false;
            data.m_isDying = false;
            OnPlantHappy?.Invoke();
            Debug.Log($"PLANT STATUS\nWater Level = {data.m_waterLevel}\nNeeds Water = {data.m_needsWater}\nIs Dying = {data.m_isDying}");
            return true; // Plant is healthy
        }
    }

    // Consequences
    private void PlantGrows(PlantData data)
    {
        data.m_canGrow = false; // Resets canGrow after plant has grown
        data.m_growthStage++;
    }

    private int WaterLevelDecay(PlantData data)
    {
        data.m_waterLevel--;
        Debug.Log($"ID: {data.m_uniquePlantId} Water Level: {data.m_waterLevel}");
        return data.m_waterLevel;
    }

    private void WaterPlant(PlantData data)
    {
        data.m_recentlyWatered = true;
        data.m_waterLevel = data.m_waterLevel + 50;
        Debug.Log($"Plant watered! New water level: {data.m_waterLevel}");
    }
}
