using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_howToPlayPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_settingsPanel;
    public GameObject m_exitPanelButton;
    public GameObject m_currentPanelOpen;

    private bool m_howToPlayPanelOpen = true;
    private bool m_controlsPanelOpen = true;
    private bool m_settingsPanelOpen = true;

    public void ToggleHowToPlayPanel()
    {
        if (m_howToPlayPanelOpen)
        {
            m_currentPanelOpen = m_howToPlayPanel;
            m_howToPlayPanel.SetActive(true);
            m_exitPanelButton.SetActive(true);

            m_controlsPanel.SetActive(false);
            m_settingsPanel.SetActive(false);
        }
        else
        {
            m_exitPanelButton.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }

        m_howToPlayPanelOpen = !m_howToPlayPanelOpen;
    }

    public void ToggleControlsPanel()
    {
        if (m_controlsPanelOpen)
        {
            m_currentPanelOpen = m_controlsPanel;
            m_controlsPanel.SetActive(true);
            m_exitPanelButton.SetActive(true);

            m_settingsPanel.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
        else
        {
            m_exitPanelButton.SetActive(false);
            m_controlsPanel.SetActive(false);
        }

        m_controlsPanelOpen = !m_controlsPanelOpen;
    }

    public void ToggleSettingsPanel()
    {
        if (m_settingsPanelOpen)
        {
            m_currentPanelOpen = m_settingsPanel;
            m_settingsPanel.SetActive(true);
            m_exitPanelButton.SetActive(true);

            m_controlsPanel.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
        else
        {
            m_exitPanelButton.SetActive(false);
            m_settingsPanel.SetActive(false);
        }

        m_settingsPanelOpen = !m_settingsPanelOpen;
    }
}
