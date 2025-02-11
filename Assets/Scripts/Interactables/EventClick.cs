using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // The game object that this script is attached to
    private GameObject thisGameObject;
    
    // Object clicked event
    public delegate void ObjectClicked(GameObject clickedObject);
    public static event ObjectClicked OnObjectClicked;

    // Object hovered over (entered/exited) events
    public delegate void ObjectEntered(GameObject enteredObject);
    public static event ObjectEntered OnObjectEntered;

    public delegate void ObjectExited(GameObject exitedObject);
    public static event ObjectExited OnObjectExited;
    
    // Hover colour related variables
    private SpriteRenderer m_spriteRenderer;
    private Color m_hoveredColour = Color.grey;
    private Color m_normalColour;
    
    private void Start()
    {
        thisGameObject = gameObject;
        m_spriteRenderer = thisGameObject.GetComponent<SpriteRenderer>();
        m_normalColour = m_spriteRenderer.color;
    }

    // These functions are overwritten in individual game objects' own EventClick scripts
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked " + gameObject.name);
        OnObjectClicked?.Invoke(thisGameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered " + gameObject.name);
        m_spriteRenderer.color = m_hoveredColour;
        OnObjectEntered?.Invoke(thisGameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exited " + gameObject.name);
        m_spriteRenderer.color = m_normalColour;
        OnObjectExited?.Invoke(thisGameObject);
    }
}