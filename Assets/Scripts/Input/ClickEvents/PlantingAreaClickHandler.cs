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

    private void OnEnable()
    {
        EventClick.OnObjectClicked += HandleClick;
    }

    private void OnDisable()
    {
        EventClick.OnObjectClicked -= HandleClick;
    }
    
    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;

        if (m_moneyManager.m_playerCurrentMoney >= 25) // 25 = price of a seed
        {
            m_moneyManager.RemoveMoney(25); // 25 is deducted from player's money
            SpawnPlant();
            //m_plantingArea.GetComponent<EventClick>().m_clickingAllowed = false;
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
}