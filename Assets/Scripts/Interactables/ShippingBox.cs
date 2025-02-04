using UnityEngine;
using UnityEngine.Serialization;

public class ShippingBox : MonoBehaviour
{
    [SerializeField] private MoneySystem moneySystem;

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
        
        // TODO - access currently selected slot here. access item within slot. add money depending on how much that item is worth via its plant data.
        // moneySystem.AddMoney(15);
    }
}
