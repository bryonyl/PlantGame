using UnityEngine;
using UnityEngine.Serialization;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager; // Reference to the money system
    public TMPro.TextMeshProUGUI moneyText; // Reference to the label

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
        moneyText.text = $"Money: ${moneyManager.playerCurrentMoney}";
    }
}