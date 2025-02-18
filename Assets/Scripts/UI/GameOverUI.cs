using System;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_quotaProgressText;
    [SerializeField] private TMPro.TextMeshProUGUI m_dayReachedText;

    private void OnEnable()
    {
        DayProgression.OnGameOver += UpdateUI;
    }

    private void OnDisable()
    {
        DayProgression.OnGameOver -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_quotaProgressText.text = $"Quota Progress: ${PlayerPrefs.GetFloat("FinalMoney")}/${PlayerPrefs.GetFloat("FinalQuotaGoal")}";
        m_dayReachedText.text = $"Day Reached: {PlayerPrefs.GetInt("FinalDay")}";
    }
}
