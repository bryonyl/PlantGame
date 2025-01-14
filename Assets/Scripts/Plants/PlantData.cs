using UnityEngine;

public class PlantData : MonoBehaviour
{
    // Identification variables
    private string m_plantName = "";
    [SerializeField] private int m_plantId = 0;

    // Money related variables
    private float m_plantSellingValue = 5f;

    // Care related variables
    private int m_plantWateringRequirement = 1;
    private float m_growthProgressionTimer = 10;
    private bool m_plantGrowing = true;
    private bool m_plantDying = false;

    enum PlantDatabase
    {
        Wheat = 1,
        Beetroot = 2
    }
    private int m_plantGrowthStage = 1;

}