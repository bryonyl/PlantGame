using System;
using UnityEngine;

public class ChangeSpriteColour : MonoBehaviour
{
    public Color hoveredColour;
    public Color normalColour;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        EventClick.OnObjectEntered += ChangeObjectToHoveredColour;
        EventClick.OnObjectExited += ChangeObjectToNormalColour;
    }

    private void OnDisable()
    {
        EventClick.OnObjectEntered -= ChangeObjectToHoveredColour;
        EventClick.OnObjectExited -= ChangeObjectToNormalColour;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeObjectToHoveredColour(GameObject object)
    {
        spriteRenderer.color = hoveredColour;
    }

    private void ChangeObjectToNormalColour(GameObject object)
    {
        spriteRenderer.color = normalColour;
    }
}
