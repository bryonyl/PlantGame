using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneySystem m_moneySystem; // Reference to the money system
    public TMPro.TextMeshProUGUI m_moneyText; // Reference to the label

    private void OnEnable()
    {
        MoneySystem.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        MoneySystem.OnMoneyChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        m_moneyText.text = $"Money: ${m_moneySystem.m_playerCurrentMoney}";
    }
}
