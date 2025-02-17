using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MoneyManager : MonoBehaviour
{
    public static event Action OnMoneyChanged;

    public float m_playerCurrentMoney;
    public float m_playerCurrentQuotaGoal;

    private void Awake()
    {
        m_playerCurrentMoney = 100;
        m_playerCurrentQuotaGoal = 250;
    }

    public void AddMoney(float moneyToAdd)
    {
        m_playerCurrentMoney += moneyToAdd;
        OnMoneyChanged?.Invoke();
        Debug.Log($"Money added (${moneyToAdd})");
        Debug.Log($"New money = {m_playerCurrentMoney}");
    }

    public void RemoveMoney(float moneyToRemove)
    {
        m_playerCurrentMoney -= moneyToRemove;
        OnMoneyChanged?.Invoke();
        Debug.Log($"Money removed (${moneyToRemove})");
        Debug.Log($"New money = {m_playerCurrentMoney}");
    }

    public bool CheckQuota()
    {
        if (m_playerCurrentMoney < m_playerCurrentQuotaGoal)
        {
            Debug.Log("Quota has not been met! Game over!");
            SceneManager.LoadScene(2);
            return false;
        }
        else
        {
            Debug.Log("Quota has been met! Continuing game!");
            return true;
        }
    }
}
