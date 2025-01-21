using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    PlantData plantData;
    GameObject[] m_plantsInSceneArr;
    //List<GameObject> plantsInSceneList = new List<GameObject>();
    List<PlantData> plantsInScenePlantDataList = new List<PlantData>();

    void Start()
    {
        plantsInSceneList.Add(GameObject.FindGameObjectWithTag("Plant"));

        for (int i = 0; i < plantsInSceneList.Count; i++)
        {
            m_plantsInSceneArr = GameObject.FindGameObjectWithTag("Plant");
            plantData = plantsInScene[i].GetComponent<PlantData>();
            plantsInScenePlantData[i] = plantsInScene[i].GetComponent<PlantData>();

            Debug.Log($"Plant created with the values:\nPlant ID: {id}\nWater Level: {plantData.m_waterLevel}\nPlant Growth Stage: {plantData.m_growthStage}\nNeeds Water?: {plantData.m_needsWater}\nIs Dying?: {plantData.m_isDying}\nCan Grow?: {plantData.m_canGrow}");

            StartCoroutine(WaterLevelDecayTimer(plantsInScenePlantData[id]));
            StartCoroutine(PlantGrowthTimer(plantsInScenePlantData[id]));
        } 
    }

    // Plant Health Check
    private bool PlantHealthCheck(PlantData data)
    {
        if (plantData.m_waterLevel <= 0)
        {
            plantData.m_needsWater = true;
            // Change plant status indicator animation to needs watering
            if (plantData.m_waterLevel < -10)
            {
                plantData.m_isDying = true;
            }
            Debug.Log($"PLANT STATUS\nWater Level = {plantData.m_waterLevel}\nNeeds Water = {plantData.m_needsWater}\nIs Dying = {plantData.m_isDying}");
            return false; // Plant is dying and needs care
        }
        else
        {
            plantData.m_needsWater = false;
            plantData.m_isDying = false;
            Debug.Log($"PLANT STATUS\nWater Level = {plantData.m_waterLevel}\nNeeds Water = {plantData.m_needsWater}\nIs Dying = {plantData.m_isDying}");
            return true; // Plant is healthy
        }
    }

    // Plant Growth
    private IEnumerator PlantGrowthTimer(PlantData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
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
        }
    }

    private void PlantGrows(PlantData data)
    {
        plantData.m_canGrow = false; // Resets canGrow after plant as grown
        Debug.Log("Plant can grow!");
        plantData.m_growthStage++;
    }

    // Water Level Decay
    private int WaterLevelDecay(PlantData data)
    {
        plantData.m_waterLevel--;
        Debug.Log($"ID: {plantData.m_plantId} Water Level: {plantData.m_waterLevel}");
        return plantData.m_waterLevel;
    }

    private IEnumerator WaterLevelDecayTimer(PlantData data)
    {
        while (true)
        {
            //maybe decrease time between loops
            yield return new WaitForSeconds(plantData.m_wateringTimer);
            WaterLevelDecay(data);
        }
    }
}
