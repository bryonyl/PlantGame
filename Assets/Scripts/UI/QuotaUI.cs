using UnityEngine;
using UnityEngine.Serialization;

public class QuotaUI : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager; // Reference to the money manager
    public TMPro.TextMeshProUGUI quotaText;

    private void Start()
    {
        UpdateUI();
    }

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
        quotaText.text = $"Quota: ${moneyManager.m_playerCurrentMoney}/${moneyManager.m_playerCurrentQuotaGoal}";
    }
}
