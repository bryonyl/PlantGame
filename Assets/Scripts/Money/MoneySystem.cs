using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int m_playerCurrentMoney;

    public void AddMoney(int moneyToAdd)
    {
        m_playerCurrentMoney += moneyToAdd;
    }
}
