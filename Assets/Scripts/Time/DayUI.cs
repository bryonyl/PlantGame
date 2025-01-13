using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable() // Where we subscribe to the actions found in TimeManager.cs
    {
        // Time is updated when the minute or hour changes
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable() // Where we unsubscribe to the actions to avoid memory leaks. This happens automatically if the time game object is disabled
    {
        // Unsubscribes the UpdateTime method from the actions OnMinuteChanged and OnHourChanged
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.m_hour}:{TimeManager.m_minute:00}"; // :00 masks the hours and the minutes with "00:" so that there are 0s present when nothing occupies either the tens or units of either the minutes or hours
    }
}
