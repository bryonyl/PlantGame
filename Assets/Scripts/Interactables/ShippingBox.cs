using UnityEngine;

public class ShippingBox : MonoBehaviour
{
    [SerializeField] private MoneySystem m_moneySystem;

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

    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;

        m_moneySystem.AddMoney(15);
    }
}
