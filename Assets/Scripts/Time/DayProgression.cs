using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static EventClick;

public class DayProgression : MonoBehaviour
{
    public static event Action OnDayChanged;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck; // Subscribes TimeCheck to OnMinuteChanged, so TimeCheck is listening for the OnMinuteChanged signal
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    // Checks the time. If it has it 10PM, then progress to the next day
    private void TimeCheck()
    {
        if (TimeManager.m_hour == 11 && TimeManager.m_minute == 00)
        {
            OnDayChanged?.Invoke();
        }
    }
}
