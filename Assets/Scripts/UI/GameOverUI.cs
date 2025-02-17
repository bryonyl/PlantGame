using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_quotaProgressText;
    [SerializeField] private TMPro.TextMeshProUGUI m_dayReachedText;

    [SerializeField] private EndOfGameResults m_endOfGameResults;
    
    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_quotaProgressText.text = $"Quota Progress: ${m_endOfGameResults.FinalMoney}/${m_endOfGameResults.FinalQuota}";
        m_dayReachedText.text = $"Day Reached: ${m_endOfGameResults.FinalDay}";
    }
}
