using UnityEngine;

public class ShowPlantInfoOnHover : MonoBehaviour
{
    private void OnEnable()
    {
        EventClick.OnObjectEntered += HandleMouseEnter;
        EventClick.OnObjectExited += HandleMouseExit;
    }

    private void OnDisable()
    {
        EventClick.OnObjectEntered -= HandleMouseEnter;
        EventClick.OnObjectExited -= HandleMouseExit;
    }

    private void HandleMouseEnter(GameObject enteredObject)
    {
        // Set hover UI to active
    }

    private void HandleMouseExit(GameObject enteredObject)
    {
        // Set hover UI to inactive
    }
}
