using System;
using UnityEngine;

public class ChangePlantSprite : MonoBehaviour
{
    public SpriteRenderer m_plantSpriteRenderer;
    public Sprite m_plantGrowthStage1;
    public Sprite m_plantGrowthStage2;
    public Sprite m_plantGrowthStage3;
    public Sprite m_plantGrowthStage4;
    Sprite[] m_allPlantGrowthSprites;
    
    private PlantData m_plantData;
    private ChangePlantSprite m_changePlantSprite;

    private void OnEnable()
    {
        m_plantData = gameObject.GetComponent<PlantData>();
        m_changePlantSprite = gameObject.GetComponent<ChangePlantSprite>();
    }

    private void Start()
    {
        m_allPlantGrowthSprites = new Sprite[] { m_plantGrowthStage1, m_plantGrowthStage2, m_plantGrowthStage3, m_plantGrowthStage4 };
    }
    /// <summary>
    /// Changes plant sprite into the specified plant growth stage
    /// </summary>
    /// <param name="plantGrowthStage">The specified plant growth stage (max = 3)</param>
    public void ChangeSprite(int plantGrowthStage)
    {
        if (plantGrowthStage <= 3)
        {
            m_plantSpriteRenderer.sprite = m_allPlantGrowthSprites[plantGrowthStage];
        }
    }

    /// <summary>
    /// Changes the plant's sprite renderer to black
    /// </summary>
    public void ChangeSpriteToDead()
    {
        m_plantSpriteRenderer.color = Color.black;
    }
}
