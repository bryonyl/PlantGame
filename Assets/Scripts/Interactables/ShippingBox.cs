using UnityEngine;

public class ShippingBox : MonoBehaviour
{
    public GameObject moneySystemGameObject; // Reference to money system's game object
    private MoneySystem moneySystem; // Reference to money system's script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneySystem = moneySystemGameObject.GetComponent<MoneySystem>(); // Getting the money system script from the actual money system game object to avoid object not found error, as the shipping box does not have a money system script attached itself.
    }

    private void OnEnable() // Special method that is automatically called when the script instance is enabled
    {
        // Subscribes to the OnObjectClicked event located in EventClick.cs
        EventClick.OnObjectClicked += HandleClick;
    }

    private void OnDisable() // Special method that is automatically called when the script instance is disabled
    {
        // Unsubscribes from the OnObjectClicked event when it is not needed to avoid memory leaks
        // (memory leak = program releasing memory it no longer needs. If memory is not released, all available memory can be consumed eventually, causing program or computer to crash)
        EventClick.OnObjectClicked -= HandleClick;
    }
    private void AddMoney(int amount)
    {
        moneySystem.m_playerCurrentMoney += amount; // Adds and sets the amount of money
        Debug.Log("Money added: " + amount);
        Debug.Log("New money = " + moneySystem.m_playerCurrentMoney);
    }

    private void HandleClick(GameObject clickedObject)
    {
        Debug.Log("Shipping box clicked. Adding money.");
        AddMoney(15);
    }
}
