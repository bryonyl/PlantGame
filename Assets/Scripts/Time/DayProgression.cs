using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayProgression : MonoBehaviour
{
    public static event Action OnDayChanged;
    public static event Action OnGameOver;

    [SerializeField] private GameObject m_sleepingPanel;
    
    [SerializeField] private MoneyManager m_moneyManager;

    private void OnEnable()
    {
        TimeManager.OnHourChanged += EndOfDayCheck;
        TimeManager.OnMinuteChanged += EndOfDayCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged -= EndOfDayCheck;
        TimeManager.OnMinuteChanged -= EndOfDayCheck;
    }

    // Checks the time. If it has it is the end of the day, then progress to the next day
    private void EndOfDayCheck()
    {
        if (TimeManager.m_hour == TimeManager.m_dayEndTime && TimeManager.m_minute == 00)
        {
            PlayerSleeps();
        }
    }

    public void PlayerSleeps()
    {
        m_sleepingPanel.SetActive(true);
        if (m_moneyManager.CheckQuota() == true) // Player has fulfilled quota for the day
        {
            Invoke(nameof(ChangeDay), 3); // Invokes ChangeDay after 3 seconds have passed
        }
        else // Player has NOT fulfilled quota for the day
        {
            OnGameOver?.Invoke();
            SceneManager.LoadScene(2); // Loads game over scene
        }
    }

    public void ChangeDay()
    {
        OnDayChanged?.Invoke();
        m_sleepingPanel.SetActive(false);
    }
}
