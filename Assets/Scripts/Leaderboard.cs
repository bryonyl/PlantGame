using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Leaderboard : MonoBehaviour
{
    public int m_maximumLeaderboardEntries = 6;
    public int m_currentHighScore;
    
    // struct LeaderboardEntry
    // {
    //     public int m_daysReachedScore = PlayerPrefs.GetFloat("FinalMoney", m_moneyManager.m_playerCurrentMoney);
    //     public float m_moneyObtainedScore;
    //     public float m_quotaReachedScore;
    // }
    
    /*private List<LeaderboardEntry> m_leaderboardEntries = new List<LeaderboardEntry>();

    private void Start()
    {
        for (int i = 0; i < m_maximumLeaderboardEntries; i++)
        {
            bool hasKey = PlayerPrefs.HasKey(m_leaderboardEntries[i].m_daysReachedScore.ToString());
            
            m_leaderboardEntries.Add(new LeaderboardEntry());
        }
    }*/

    /*[SerializeField] private MoneyManager m_moneyManager;
    
    
    

    
    private void Start()
    {
        m_leaderboardEntries.Add
    }*/
}
