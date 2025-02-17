using UnityEngine;
using UnityEngine.Serialization;

public class QuotaUI : MonoBehaviour
{
    [SerializeField] private MoneyManager m_moneyManager;
    public TMPro.TextMeshProUGUI m_quotaText;

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
        m_quotaText.text = $"Quota: ${m_moneyManager.m_playerCurrentMoney}/${m_moneyManager.m_playerCurrentQuotaGoal}";
    }
}
