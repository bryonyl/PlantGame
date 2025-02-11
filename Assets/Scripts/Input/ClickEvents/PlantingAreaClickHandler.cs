using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantingAreaClickHandler : MonoBehaviour
{
    public static event Action OnPlantPlanted;
    
    private GameObject m_plantingArea;
    [SerializeField] private GameObject plantToSpawn;

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

    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;
        Debug.Log("Clicking the planting area");

        SpawnPlant();
    }

    private void SpawnPlant()
    {
        Instantiate(plantToSpawn, m_plantingArea.transform);
        Debug.Log("Plant spawned");
        OnPlantPlanted?.Invoke();
    }
}
