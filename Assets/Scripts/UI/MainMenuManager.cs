using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject controlsPanel;
    public GameObject exitPanelButton;
    public GameObject currentPanelOpen;

    private bool m_howToPlayPanelOpen = true;
    private bool m_controlsPanelOpen = true;

    public void ToggleHowToPlayPanel()
    {
        if (m_howToPlayPanelOpen)
        {
            currentPanelOpen = howToPlayPanel;
            howToPlayPanel.SetActive(true);
            exitPanelButton.SetActive(true);

            controlsPanel.SetActive(false);
        }
        else
        {
            exitPanelButton.SetActive(false);
            howToPlayPanel.SetActive(false);
        }

        m_howToPlayPanelOpen = !m_howToPlayPanelOpen;
    }

    public void ToggleControlsPanel()
    {
        if (m_controlsPanelOpen)
        {
            currentPanelOpen = controlsPanel;
            controlsPanel.SetActive(true);
            exitPanelButton.SetActive(true);
            
            howToPlayPanel.SetActive(false);
        }
        else
        {
            exitPanelButton.SetActive(false);
            controlsPanel.SetActive(false);
        }

        m_controlsPanelOpen = !m_controlsPanelOpen;
    }
}
