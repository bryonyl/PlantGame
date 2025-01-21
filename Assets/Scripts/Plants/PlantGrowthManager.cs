using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlantGrowthManager : MonoBehaviour
{
    public static PlantGrowthManager s_plantGrowthManagerInstance;

    GameObject[] plants;
    PlantData plantData;

    int waterLevel = 30;
    int plantGrowthStage = 1;

    bool needsWater = false;
    bool isDying = false;
    bool canGrow = true;

    void Start()
    {
        plants = GameObject.FindGameObjectsWithTag("Plant");

        for (int i = 0; i < plants.Length; i++)
        {
            plantData = plants[i].GetComponent<PlantData>();

            plantData.m_plantId = i;

            waterLevel = plantData.m_waterLevel;
            plantGrowthStage = plantData.m_growthStage;

            needsWater = plantData.m_needsWater;
            isDying = plantData.m_isDying;
            canGrow = plantData.m_canGrow;

            Debug.Log($"Plant created with the values:\nPlant ID: {plantData.m_plantId}\nWater Level: {waterLevel}\nPlant Growth Stage: {plantGrowthStage}\nNeeds Water?: {needsWater}\nIs Dying?: {isDying}\nCan Grow?: {canGrow}");

            StartCoroutine(nameof(WaterLevelDecayTimer)); // Uses nameof to be type safe
            StartCoroutine(nameof(PlantGrowthTimer));
        } 
    }

    // Plant Health Check
    private bool PlantHealthCheck()
    {
        if (waterLevel <= 0)
        {
            needsWater = true;
            // Change plant status indicator animation to needs watering
            if (waterLevel < -10)
            {
                isDying = true;
            }
            Debug.Log($"PLANT STATUS\nWater Level = {waterLevel}\nNeeds Water = {needsWater}\nIs Dying = {isDying}");
            return false; // Plant is dying and needs care
        }
        else// if (waterLevel > 0)
        {
            needsWater = false;
            isDying = false;
            Debug.Log($"PLANT STATUS\nWater Level = {waterLevel}\nNeeds Water = {needsWater}\nIs Dying = {isDying}");
            return true; // Plant is healthy
        }
    }

    // Plant Growth
    private IEnumerator PlantGrowthTimer(PlantData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            if (PlantHealthCheck() == true) // If plant health check is true, so the plant meets the conditions for growing
            {
                //remove can grow?
                data.m_canGrow = true;
                canGrow = true;
                Debug.Log($"canGrow = {canGrow}");
                PlantGrows();
            }
            else if (PlantHealthCheck() == false) // If plant health check is false, so the plant does not meet the conditions for growing
            {
                canGrow = false;
                Debug.Log($"canGrow = {canGrow}");
            }
        }
    }

    private void PlantGrows()
    {
        canGrow = false; // Resets canGrow after plant as grown
        Debug.Log("Plant can grow!");
        plantGrowthStage++;
    }

    // Water Level Decay
    private int WaterLevelDecay()
    {
        waterLevel--;
        return waterLevel;
    }

    private IEnumerator WaterLevelDecayTimer()
    {
        while (true)
        {
            //maybe decrease time between loops
            yield return new WaitForSeconds(5);
            Debug.Log("Water level decaying...");
            WaterLevelDecay();
        }
    }
}
