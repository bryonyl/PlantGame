using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_howToPlayPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_settingsPanel;
    [SerializeField] private GameObject m_exitPanelButton;
    public GameObject m_currentPanelOpen;

    private bool m_howToPlayPanelOpen = true;
    private bool m_controlsPanelOpen = true;
    private bool m_settingsPanelOpen = true;
    private bool m_showExitPanelButton = true;

    public void ToggleHowToPlayPanel()
    {
        CheckForNewActivePanel(m_howToPlayPanel);

        if (m_howToPlayPanelOpen)
        {
            m_currentPanelOpen = m_howToPlayPanel;
            m_howToPlayPanel.SetActive(true);
            ToggleExitPanelButton();

            m_controlsPanel.SetActive(false);
            m_settingsPanel.SetActive(false);
        }
        else
        {
            ToggleExitPanelButton();
            m_howToPlayPanel.SetActive(false);
        }

        m_howToPlayPanelOpen = !m_howToPlayPanelOpen;
    }

    public void ToggleControlsPanel()
    {
        CheckForNewActivePanel(m_controlsPanel);

        if (m_controlsPanelOpen)
        {
            m_currentPanelOpen = m_controlsPanel;
            m_controlsPanel.SetActive(true);
            ToggleExitPanelButton();

            m_settingsPanel.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
        else
        {
            ToggleExitPanelButton();
            m_controlsPanel.SetActive(false);
        }

        m_controlsPanelOpen = !m_controlsPanelOpen;
    }

    public void ToggleSettingsPanel()
    {
        CheckForNewActivePanel(m_settingsPanel);
        if (m_settingsPanelOpen)
        {
            m_currentPanelOpen = m_settingsPanel;
            m_settingsPanel.SetActive(true);
            ToggleExitPanelButton();

            m_controlsPanel.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
        else
        {
            ToggleExitPanelButton();
            m_settingsPanel.SetActive(false);
        }

        m_settingsPanelOpen = !m_settingsPanelOpen;
    }

    public void CheckForNewActivePanel(GameObject currentPanel)
    {
        if (m_currentPanelOpen != currentPanel)
        {
            m_exitPanelButton.SetActive(false);
        }
    }

    public void ToggleExitPanelButton()
    {
        if (m_showExitPanelButton)
        {
            m_exitPanelButton.SetActive(true);
            
        }
        else
        {
            m_exitPanelButton.SetActive(false);
        }
    }
}
