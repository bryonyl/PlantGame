using UnityEngine;

public class BedClickHandler : MonoBehaviour
{
    [SerializeField] private DayProgression m_dayProgression;
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

        m_dayProgression.PlayerSleeps();
    }
}
