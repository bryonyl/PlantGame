using UnityEngine;
using UnityEngine.Serialization;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneyManager m_moneyManager;
    public TMPro.TextMeshProUGUI m_moneyText;
    
    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= UpdateUI;
    }
    
    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_moneyText.text = $"Money: ${m_moneyManager.m_playerCurrentMoney}";
    }
}