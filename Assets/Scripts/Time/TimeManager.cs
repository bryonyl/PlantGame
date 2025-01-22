using UnityEngine;
using System; // Need this namespace to access Action type

public class TimeManager : MonoBehaviour
{
    // Apparently Actions are slightly faster than Unity Events for this?
    public static event Action OnMinuteChanged;
    public static event Action OnHourChanged;

    // Keep a reference to what time it is. Uses properties so we can't edit the time outside of this script
    public static int m_minute { get; private set; } // Can only change this value within this class
    public static int m_hour { get; private set; }
    public static int m_day { get; private set; }

    private float m_minuteToRealTime = 0.5f; // 0.5 seconds in real time represents 1 minute in game
    private float m_timer; // Localised timer

    [SerializeField] public static int m_dayStartTime = 8; // 8AM
    [SerializeField] public static int m_dayEndTime = 22; // 10PM

    private void OnEnable()
    {
        DayProgression.OnDayChanged += ChangeDay;
    }

    private void OnDisable()
    {
        DayProgression.OnDayChanged -= ChangeDay;
    }

    private void ChangeDay()
    {
        m_day++;
        m_hour = m_dayStartTime;
        m_minute = 0;
    }

    void Start()
    {
        ChangeDay();
        m_timer = m_minuteToRealTime;
    }

    void Update()
    {
        // Creates timer
        m_timer -= Time.deltaTime;

        // Adds 1 to Minute every half a second
        if (m_timer <= 0)
        {
            m_minute++; // Minute is incremented by 1
            OnMinuteChanged?.Invoke(); // Invokes OnMinuteChanged event so that other scripts can respond to this event. ?. is the null check

            if(m_minute >= 60) // Minute is reset here as there are 60 mins in an hour
            {
                m_hour++; // Hour is incremented by 1
                m_minute = 0; // Minute is reset back to 0
                OnHourChanged?.Invoke(); // Invokes OnHourChanged event so that other scripts can respond to this event
            }

            m_timer = m_minuteToRealTime; // Timer is reset to minuteToRealTime
        }
    }
}
