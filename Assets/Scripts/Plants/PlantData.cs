using UnityEngine;

public class PlantData : MonoBehaviour
{
    // Identification variables
    private string m_plantName = "";
    [SerializeField] public int m_plantId = 0;

    // Money related variables
    private float m_plantSellingValue = 0f;

    // Care related variables
    [SerializeField] public int m_waterLevel = 0;
    [SerializeField] public float m_growthTimer = 0;
    [SerializeField] public int m_growthStage = 0;
    public bool m_needsWater = false;
    public bool m_canGrow = false;
    public bool m_isDying = false;

    enum PlantDatabase
    {
        Wheat = 1,
        Beetroot = 2
    }
}