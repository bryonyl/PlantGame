using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantingAreaHandleClick : MonoBehaviour
{
    private GameObject plantingArea;
    [SerializeField] private GameObject plantToSpawn;

    private void Start()
    {
        plantingArea = gameObject;
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
        Debug.Log("Plant is trying to spawn");
        Instantiate(plantToSpawn, plantingArea.transform);
    }
}
