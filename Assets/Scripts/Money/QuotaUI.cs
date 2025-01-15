using TMPro;
using UnityEngine;

public class QuotaUI : MonoBehaviour
{
    [SerializeField] private MoneySystem m_moneySystem; // Reference to the money system
    public TextMeshProUGUI m_quotaText;

    private void UpdateUI()
    {
        m_quotaText.text = $"Quota: {MoneySystem.m_playerCurrentMoney}/{MoneySystem.m_playerCurrentQuotaGoal}";
    }

}
