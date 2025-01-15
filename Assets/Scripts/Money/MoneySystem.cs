using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int m_playerCurrentMoney = 0;
    public int m_playerCurrentQuotaGoal = 0;

    public void AddMoney(int moneyToAdd)
    {
        m_playerCurrentMoney += moneyToAdd;
    }
}
