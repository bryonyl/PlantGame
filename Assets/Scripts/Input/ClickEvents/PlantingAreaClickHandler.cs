using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantingAreaClickHandler : MonoBehaviour
{
    public delegate void PlantPlanted(GameObject plant);
    public static event PlantPlanted OnPlantPlanted;

    [SerializeField] private MoneyManager m_moneyManager;
    
    private GameObject m_plantingArea;
    public GameObject m_plantToSpawn;

    private void Start()
    {
        m_plantingArea = gameObject;
    }

    private void OnEnable()
    {
        EventClick.OnObjectClicked += HandleClick;
        PlantClickHandler.OnPlantHarvested += ReactivatePlantingArea;
    }

    private void OnDisable()
    {
        EventClick.OnObjectClicked -= HandleClick;
        PlantClickHandler.OnPlantHarvested -= ReactivatePlantingArea;
    }
    
    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;

        if (m_moneyManager.m_playerCurrentMoney >= 10) // 10 = price of a seed
        {
            m_moneyManager.RemoveMoney(10); // 10 is deducted from player's money
            SpawnPlant();
            
            m_plantingArea.GetComponent<EventClick>().m_clickingAllowed = false;
        }
        else
        {
            Debug.Log("You do not have enough money to plant this plant!");
        }
    }

    private void SpawnPlant()
    {
        GameObject spawnedPlant = Instantiate(m_plantToSpawn, m_plantingArea.transform);
        OnPlantPlanted?.Invoke(spawnedPlant);
    }

    public void ReactivatePlantingArea(PlantData data)
    {
        if (data.m_readyToHarvest)
        {
            m_plantingArea.GetComponent<EventClick>().m_clickingAllowed = true;
        }
    }
}