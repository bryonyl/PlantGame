using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using static EventClick;

public class DayProgression : MonoBehaviour
{
    public static event Action OnDayChanged;

    private void OnEnable()
    {
        TimeManager.OnHourChanged += TimeCheck;
        TimeManager.OnMinuteChanged += TimeCheck; // Subscribes TimeCheck to OnMinuteChanged, so TimeCheck is listening for the OnMinuteChanged signal
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged -= TimeCheck;
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    // Checks the time. If it has it is the end of the day, then progress to the next day
    private void TimeCheck()
    {
        if (TimeManager.m_hour == TimeManager.m_dayEndTime && TimeManager.m_minute == 00)
        {
            OnDayChanged?.Invoke();
        }
    }
}
