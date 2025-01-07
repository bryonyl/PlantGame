using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int m_playerCurrentMoney;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void AddMoney(int moneyToAdd)
    {
        m_playerCurrentMoney += moneyToAdd;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
