using System;
using UnityEngine;

public class PlantClickHandler : MonoBehaviour
{
    public delegate void PlantWatered(PlantData data, ChangePlantSprite sprite);
    public static event PlantWatered OnPlantWatered;
    
    public delegate void PlantHarvested(PlantData data);
    public static event PlantHarvested OnPlantHarvested;

    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;

    private void Start()
    {
        m_plantData = gameObject.GetComponent<PlantData>();
        m_changePlantSprite = gameObject.GetComponent<ChangePlantSprite>();
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
        
        OnPlantWatered?.Invoke(m_plantData, m_changePlantSprite);
        OnPlantHarvested?.Invoke(m_plantData);
    }
}
