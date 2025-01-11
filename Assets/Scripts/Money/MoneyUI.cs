using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : MonoBehaviour
{
    private MoneySystem m_moneySystem; // Reference to the money system
    public TMPro.TextMeshProUGUI m_uiLabel; // Reference to the label
    public void UpdateUI()
    {
        m_uiLabel.text = "Money: $" + m_moneySystem.m_playerCurrentMoney;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            m_moneySystem = GetComponent<MoneySystem>();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        Debug.Log("UI updated");
    }

    
}
