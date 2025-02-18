using UnityEngine;

public class EndOfGameResults : MonoBehaviour
{
    [SerializeField] private MoneyManager m_moneyManager;
    
    private void OnEnable()
    {
        DayProgression.OnGameOver += SavePlayerStats;
    }

    private void OnDisable()
    {
        DayProgression.OnGameOver -= SavePlayerStats;
    }

    /// <summary>
    /// Saves player's final stats (their current money, their quota goal, the day they're at) to PlayerPrefs
    /// </summary>
    private void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("FinalMoney", m_moneyManager.m_playerCurrentMoney);
        PlayerPrefs.SetFloat("FinalQuotaGoal", m_moneyManager.m_playerCurrentQuotaGoal);
        PlayerPrefs.SetInt("FinalDay", TimeManager.m_day);
        PlayerPrefs.Save();
    }
}
