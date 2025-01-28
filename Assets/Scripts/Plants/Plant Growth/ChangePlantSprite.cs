using UnityEngine;

public class ChangePlantSprite : MonoBehaviour
{
    public SpriteRenderer m_plantSpriteRenderer;
    public Sprite m_plantGrowthStage1;
    public Sprite m_plantGrowthStage2;
    public Sprite m_plantGrowthStage3;
    public Sprite m_plantGrowthStage4;
    Sprite[] m_allPlantGrowthSprites;

    private void Start()
    {
        m_allPlantGrowthSprites = new Sprite[] { m_plantGrowthStage1, m_plantGrowthStage2, m_plantGrowthStage3, m_plantGrowthStage4 };
    }
    public void ChangeSprite(int plantGrowthStage)
    {
        m_plantSpriteRenderer.sprite = m_allPlantGrowthSprites[plantGrowthStage];
    }
}
