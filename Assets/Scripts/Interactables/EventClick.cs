using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void ObjectClicked(GameObject clickedObject);
    public static event ObjectClicked OnObjectClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down " + gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up " + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked " + gameObject.name);
        Debug.Log("This game object's tag is " + gameObject.tag);

        OnObjectClicked?.Invoke(this.gameObject);
        Debug.Log("OnObjectClicked invoked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exited " + gameObject.name);
    }
}
