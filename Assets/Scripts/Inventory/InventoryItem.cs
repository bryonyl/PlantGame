using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    public Image itemImage;
    public TMPro.TextMeshProUGUI countText;
    
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    /// <summary>
    /// Initialises an item by allocating it default values, so item is set to newItem and the image of the item is set to the newItem's image. The item's quantity text is also reset.
    /// </summary>
    /// <param name="newItem">The item to be set up.</param>
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.m_image;
        ResetItemQuantityText();
    }

    /// <summary>
    /// Visually resets an item's quantity text if the item's quantity is over 1.
    /// </summary>
    public void ResetItemQuantityText()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
}
