using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_menuPanel;

    [SerializeField] private GameObject m_howToPlayPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_settingsPanel;
    [SerializeField] private GameObject m_exitButton;

    private bool m_howToPlayPanelOpen = false;
    private bool m_controlsPanelOpen = false;
    private bool m_settingsPanelOpen = false;
    private bool m_exitButtonDisplayed = false;

    public void ToggleControlsPanel()
    {
        if (m_controlsPanelOpen)
        {
            m_controlsPanel.SetActive(true);
            

            m_controlsPanel.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
        else
        {
            m_menuPanel.SetActive(false);
            m_controlsPanel.SetActive(true);
        }

        m_controlsPanelOpen = !m_controlsPanelOpen;
    }

    public void ToggleSettingsPanel()
    {
        if (m_settingsPanelOpen)
        {

        }
    }

}
