using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    PlantData plantData;
    List<PlantData> plantsInScenePlantDataList = new List<PlantData>();

    void Start()
    {
        GameObject[] plantsInSceneArr = GameObject.FindGameObjectsWithTag("Plant");

        for (int i = 0; i < plantsInSceneArr.Count(); i++)
        {
            plantData = plantsInSceneArr[i].GetComponent<PlantData>();
            plantsInScenePlantDataList.Add(plantData);           

            Debug.Log($"Plant created with the values:\nPlant ID: {plantsInScenePlantDataList[i].m_uniquePlantId}\nWater Level: {plantsInScenePlantDataList[i].m_waterLevel}\nPlant Growth Stage: {plantsInScenePlantDataList[i].m_growthStage}\nNeeds Water?: {plantsInScenePlantDataList[i].m_needsWater}\nIs Dying?: {plantsInScenePlantDataList[i].m_isDying}\nCan Grow?: {plantsInScenePlantDataList[i].m_canGrow}");
        }

        for (int j = 0; j < plantsInScenePlantDataList.Count; j++)
        {
            Debug.Log($"Starting coroutines for {plantsInScenePlantDataList[j].m_uniquePlantId}");
            StartCoroutine(WaterLevelDecayTimer(plantsInScenePlantDataList[j]));
            StartCoroutine(PlantGrowthTimer(plantsInScenePlantDataList[j]));
        }
    }

    // Plant Health Check
    private bool PlantHealthCheck(PlantData data)
    {
        if (data.m_waterLevel <= 0)
        {
            data.m_needsWater = true;
            // Change plant status indicator animation to needs watering
            if (data.m_waterLevel < -10)
            {
                data.m_isDying = true;
            }
            Debug.Log($"PLANT STATUS\nWater Level = {data.m_waterLevel}\nNeeds Water = {data.m_needsWater}\nIs Dying = {data.m_isDying}");
            return false; // Plant is dying and needs care
        }
        else
        {
            data.m_needsWater = false;
            data.m_isDying = false;
            Debug.Log($"PLANT STATUS\nWater Level = {data.m_waterLevel}\nNeeds Water = {data.m_needsWater}\nIs Dying = {data.m_isDying}");
            return true; // Plant is healthy
        }
    }

    // Plant Growth
    private IEnumerator PlantGrowthTimer(PlantData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.m_growthTimer);
            if (PlantHealthCheck(data) == true) // If plant health check is true, so the plant meets the conditions for growing
            {
                //remove can grow?
                data.m_canGrow = true;
                Debug.Log($"canGrow = {data.m_canGrow}");
                PlantGrows(data);
            }
            else if (PlantHealthCheck(data) == false) // If plant health check is false, so the plant does not meet the conditions for growing
            {
                data.m_canGrow = false;
                Debug.Log($"canGrow = {data.m_canGrow}");
            }
            yield break;
        }
    }

    private void PlantGrows(PlantData data)
    {
        data.m_canGrow = false; // Resets canGrow after plant as grown
        Debug.Log("Plant can grow!");
        data.m_growthStage++;
    }

    // Water Level Decay
    private int WaterLevelDecay(PlantData data)
    {
        data.m_waterLevel--;
        Debug.Log($"ID: {plantData.m_uniquePlantId} Water Level: {data.m_waterLevel}");
        return data.m_waterLevel;
    }

    private IEnumerator WaterLevelDecayTimer(PlantData data)
    {
        while (true)
        {
            //maybe decrease time between loops
            yield return new WaitForSeconds(data.m_wateringTimer);
            WaterLevelDecay(data);
            yield break;
        }
    }
}
