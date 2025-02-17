using UnityEngine;

public class DayUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_dayText;

    private void OnEnable()
    {
        DayProgression.OnDayChanged += UpdateTime;
    }

    private void OnDisable()
    {
        DayProgression.OnDayChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        m_dayText.text = $"Day: {TimeManager.m_day}";
    }
}
