using UnityEngine;
using UnityEngine.Serialization;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;
    public TMPro.TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        moneyText.text = $"Money: ${moneyManager.m_playerCurrentMoney}";
    }
}