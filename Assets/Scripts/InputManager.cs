using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    private void Update()
    {
        HotBarControls();
    }

    /// <summary>
    /// The controls the player uses to navigate the hot bar. Checks if any key between 1-6 is pressed, as this is how many hot bar slots there are.
    /// Changes the selected slot via InventoryManager.cs if these criteria is fulfilled.
    /// </summary>
    private void HotBarControls()
    {
        if (Input.inputString != null) // Checks if any key is pressed
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); // Checks if pressed key is a number
            if (isNumber && number > 0 && number < 7) // If number is between range for hot bar selection
            {
                inventoryManager.ChangeSelectedSlot(number - 1); // Change selected slot
            }
        }
    }
}
