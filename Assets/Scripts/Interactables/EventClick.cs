using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject thisGameObject;
    // Object clicked event
    public delegate void ObjectClicked(GameObject clickedObject);
    public static event ObjectClicked OnObjectClicked;

    // Object hovered over (entered/exited) events
    public delegate void ObjectEntered(GameObject enteredObject);
    public static event ObjectEntered OnObjectEntered;

    public delegate void ObjectExited(GameObject exitedObject);
    public static event ObjectExited OnObjectExited;

    // These functions are overwritten in individual game objects' own EventClick scripts
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked " + gameObject.name);
        OnObjectClicked?.Invoke(thisGameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered " + gameObject.name);
        OnObjectEntered?.Invoke(thisGameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exited " + gameObject.name);
        OnObjectExited?.Invoke(thisGameObject);
    }
}