using System;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public static event Action OnMoneyChanged;

    public float m_playerCurrentMoney;
    public float m_playerCurrentQuotaGoal;

    private void Awake()
    {
        m_playerCurrentMoney = 0;
        m_playerCurrentQuotaGoal = 250;
    }

    public void AddMoney(int moneyToAdd)
    {
        m_playerCurrentMoney += moneyToAdd;
        OnMoneyChanged?.Invoke();
        Debug.Log($"Money added (${moneyToAdd})");
        Debug.Log($"New money = {m_playerCurrentMoney}");
    }
}
