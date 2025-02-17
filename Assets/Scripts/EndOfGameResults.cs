using System;
using UnityEngine;

public class EndOfGameResults : MonoBehaviour
{
    [SerializeField] private MoneyManager m_moneyManager;

    private float m_finalMoney; // Field
    public float FinalMoney // Property - so that m_finalMoney can be accessed outside of this script independent of the money manager
    {
        get { return m_finalMoney; }
        set { m_finalMoney = m_moneyManager.m_playerCurrentMoney; }
    }
    
    private float m_finalQuota;
    public float FinalQuota
    {
        get { return m_finalQuota; }
        set { m_finalQuota = m_moneyManager.m_playerCurrentQuotaGoal; }
    }
    
    private int m_finalDay;
    public float FinalDay
    {
        get { return m_finalDay; }
        set { m_finalDay = TimeManager.m_day; }
    }
    
    private void OnEnable()
    {
        DayProgression.OnGameOver += SaveResults;
    }

    private void OnDisable()
    {
        DayProgression.OnGameOver -= SaveResults;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void SaveResults()
    {
        m_finalMoney = m_moneyManager.m_playerCurrentMoney;
        m_finalQuota = m_moneyManager.m_playerCurrentQuotaGoal;
        m_finalDay = TimeManager.m_day;
    }

    
}
