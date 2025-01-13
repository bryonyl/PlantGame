using System.Collections;
using UnityEngine;

public class DayChanger : MonoBehaviour
{
    private int m_currentDay = 1; // Default, starting day is 1

    //private void OnEnable()
    //{
    //    TimeManager.OnMinuteChanged += TimeCheck; // Subscribes TimeCheck to OnMinuteChanged
    //}

    //private void OnDisable()
    //{
    //    TimeManager.OnMinuteChanged -= TimeCheck; // Unsubscribes TimeCheck from OnMinuteChanged
    //}

    private void TimeCheck()
    {
        if (TimeManager.m_hour == 22 && TimeManager.m_minute == 00) // Checks if the time has hit 10PM (military time)
        {
            StartCoroutine(DayProgresses(m_currentDay));
        }
    }

    private IEnumerator DayProgresses(int day) // Coroutine. Apparently IEnumerator iterates through a list, so not sure if it's the best thing to use here?
    {
        day++; // Increments the current day by 1
        yield return day;
    }
}
