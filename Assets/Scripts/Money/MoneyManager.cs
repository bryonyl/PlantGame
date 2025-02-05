using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneyManager : MonoBehaviour
{
    public static event Action OnMoneyChanged;

    public float playerCurrentMoney;
    public float playerCurrentQuotaGoal;

    private void Awake()
    {
        playerCurrentMoney = 0;
        playerCurrentQuotaGoal = 250;
    }

    public void AddMoney(float moneyToAdd)
    {
        playerCurrentMoney += moneyToAdd;
        OnMoneyChanged?.Invoke();
        Debug.Log($"Money added (${moneyToAdd})");
        Debug.Log($"New money = {playerCurrentMoney}");
    }
}
