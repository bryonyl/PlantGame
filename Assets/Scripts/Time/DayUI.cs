using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI m_dayText;

    private void OnEnable() // Subscribe UpdateTime to OnDayChanged event
    {
        // Time is updated when the day changes
        DayProgression.OnDayChanged += UpdateTime;
    }

    private void OnDisable() // Unsubscribe UpdateTime from OnDayChanged event
    {
        DayProgression.OnDayChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        m_dayText.text = $"Day: {TimeManager.m_day}";
    }
}
