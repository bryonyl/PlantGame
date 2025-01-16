using UnityEngine;

public class QuotaUI : MonoBehaviour
{
    [SerializeField] private MoneySystem m_moneySystem; // Reference to the money system
    public TMPro.TextMeshProUGUI m_quotaText;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        MoneySystem.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        MoneySystem.OnMoneyChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        m_quotaText.text = $"Quota: ${m_moneySystem.m_playerCurrentMoney}/${m_moneySystem.m_playerCurrentQuotaGoal}";
    }
}
