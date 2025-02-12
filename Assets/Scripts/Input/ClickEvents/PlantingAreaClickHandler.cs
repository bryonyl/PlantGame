using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantingAreaClickHandler : MonoBehaviour
{
    public delegate void PlantPlanted(GameObject plant);
    public static event PlantPlanted OnPlantPlanted;
    
    private GameObject m_plantingArea;
    public GameObject m_plantToSpawn;
    private bool m_clickingAllowed = true;

    private void Start()
    {
        m_plantingArea = gameObject;
    }

    private void OnEnable()
    {
        EventClick.OnObjectClicked += HandleClick;
    }

    private void OnDisable()
    {
        EventClick.OnObjectClicked -= HandleClick;
    }

    // public void ObjectEntered(GameObject enteredObject)
    // {
    //
    // }
    //
    // public void ObjectExited(GameObject exitedObject);
    // {
    //
    // }
    
    private void HandleClick(GameObject clickedObject)
    {
        if (m_clickingAllowed == true)
        {
            if (clickedObject != gameObject) return;
            SpawnPlant();
            m_clickingAllowed = false;
        }
    }

    private void SpawnPlant()
    {
        GameObject spawnedPlant = Instantiate(m_plantToSpawn, m_plantingArea.transform);
        OnPlantPlanted?.Invoke(spawnedPlant);
    }
}