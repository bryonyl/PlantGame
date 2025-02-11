using UnityEngine;
using UnityEngine.Serialization;

public class ShippingBoxClickHandler : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private InventoryManager inventoryManager;

    private void OnEnable()
    {
        EventClick.OnObjectClicked += HandleClick;
    }

    private void OnDisable()
    {
        EventClick.OnObjectClicked -= HandleClick;
    }

    private void HandleClick(GameObject clickedObject)
    {
        if (clickedObject != gameObject) return;

        playerActions.ItemIsSold(inventoryManager.QuerySelectedItem());
    }
}
