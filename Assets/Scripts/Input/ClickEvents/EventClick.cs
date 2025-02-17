using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
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
    
    // Conditions
    public bool m_clickingAllowed = true;
    
    private void Start()
    {
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_normalColour = m_spriteRenderer.color;
    }

    // These functions are overwritten in individual game objects' own EventClick scripts
    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_clickingAllowed)
        {
            OnObjectClicked?.Invoke(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_clickingAllowed)
        {
            m_spriteRenderer.color = m_hoveredColour;
            OnObjectEntered?.Invoke(gameObject);
        }
        else
        {
            m_spriteRenderer.color = m_normalColour;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_clickingAllowed)
        {
            m_spriteRenderer.color = m_normalColour;
            OnObjectExited?.Invoke(gameObject);
        }
        else
        {
            m_spriteRenderer.color = m_normalColour;
        }
        
    }
}