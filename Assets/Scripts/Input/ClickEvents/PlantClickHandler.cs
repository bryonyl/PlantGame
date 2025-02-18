using System;
using UnityEngine;

public class PlantClickHandler : MonoBehaviour
{
    public delegate void PlantWatered(PlantData data, IndividualPlantGrowthManager growthManager);
    public static event PlantWatered OnPlantWatered;
    
    public delegate void PlantHarvested(PlantData data);
    public static event PlantHarvested OnPlantHarvested;

    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;
    private EventClick m_eventClick;

    private void Start()
    {
        m_plantData = gameObject.GetComponent<PlantData>();
        m_changePlantSprite = gameObject.GetComponent<ChangePlantSprite>();
        m_eventClick = gameObject.GetComponent<EventClick>();
    }

    private void OnEnable()
    {
        EventClick.OnObjectClicked += HandleClick;
        EventClick.OnObjectEntered += HandleEnter;
        EventClick.OnObjectExited += HandleExit;
    }

    private void OnDisable()
    {
        EventClick.OnObjectClicked -= HandleClick;
        EventClick.OnObjectEntered -= HandleEnter;
        EventClick.OnObjectExited -= HandleExit;
    }

    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;
        
        IndividualPlantGrowthManager thisPlantGrowthManager = gameObject.GetComponent<IndividualPlantGrowthManager>();

        if (thisPlantGrowthManager == null)
        {
            Debug.LogError("This plant's growth manager is null!");
        }
        else
        {
            OnPlantWatered?.Invoke(m_plantData, thisPlantGrowthManager);
            OnPlantHarvested?.Invoke(m_plantData);
        }
    }
    
    private void HandleEnter(GameObject enteredObject)
    {
        if (m_plantData.m_isDead)
        {
            m_eventClick.m_normalColour = Color.black;
            m_eventClick.m_hoveredColour = Color.black;
            m_eventClick.m_spriteRenderer.color = m_eventClick.m_normalColour;
        }
    }

    private void HandleExit(GameObject exitedObject)
    {
        if (m_plantData.m_isDead)
        {
            m_eventClick.m_normalColour = Color.black;
            m_eventClick.m_hoveredColour = Color.black;
            m_eventClick.m_spriteRenderer.color = m_eventClick.m_normalColour;
        }
    }
}
