using System;
using UnityEngine;

public class PlantClickHandler : MonoBehaviour
{
    public delegate void PlantWatered(PlantData data);
    public static event PlantWatered OnPlantWatered;
    
    public delegate void PlantHarvested(PlantData data);
    public static event PlantHarvested OnPlantHarvested;

    private PlantData m_thisPlantData;

    private void Start()
    {
        m_thisPlantData = gameObject.GetComponent<PlantData>();
    }

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

        // Prints plant debug info
        m_thisPlantData.QueryPlant();
        
        OnPlantWatered?.Invoke(m_thisPlantData);
        OnPlantHarvested?.Invoke(m_thisPlantData);
    }
}
