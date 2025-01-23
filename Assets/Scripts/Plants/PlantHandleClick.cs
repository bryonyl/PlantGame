using System;
using UnityEngine;

public class PlantHandleClick : MonoBehaviour
{
    public delegate void PlantWatered(PlantData data);
    public static event PlantWatered OnPlantWatered;

    private PlantData m_thisPlantData;

    private void Start()
    {
        m_thisPlantData = this.gameObject.GetComponent<PlantData>();
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

        // Plant is watered when clicked
        OnPlantWatered?.Invoke(m_thisPlantData);
        Debug.Log("OnPlantWatered invoked");
    }
}
