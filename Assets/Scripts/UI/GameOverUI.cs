using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_quotaProgressText;
    [SerializeField] private TMPro.TextMeshProUGUI m_dayReachedText;
    
    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_quotaProgressText.text = $"Quota Progress: ${PlayerPrefs.GetFloat("FinalMoney", 0)}/${PlayerPrefs.GetFloat("FinalQuotaGoal", 0)}";
        m_dayReachedText.text = $"Day Reached: {PlayerPrefs.GetInt("FinalDay", 0)}";
    }
}
