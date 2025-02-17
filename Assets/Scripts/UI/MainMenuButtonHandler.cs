using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_howToPlayPanel;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_howToPlayPanelExitButton;
    [SerializeField] private GameObject m_controlsPanelExitButton;

    private bool m_howToPlayPanelOpen = false;
    private bool m_controlsPanelOpen = false;

    public void OpenMainLevel()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ToggleHowToPlayPanel()
    {
        m_howToPlayPanelOpen = !m_howToPlayPanelOpen;
        
        if (m_howToPlayPanelOpen)
        {
            m_howToPlayPanel.SetActive(true);
            m_howToPlayPanelExitButton.SetActive(true);

            m_controlsPanel.SetActive(false);
            m_controlsPanelOpen = false;
        }
        else
        {
            m_howToPlayPanelExitButton.SetActive(false);
            m_howToPlayPanel.SetActive(false);
        }
    }

    public void ToggleControlsPanel()
    {
        m_controlsPanelOpen = !m_controlsPanelOpen;
        
        if (m_controlsPanelOpen)
        {
            m_controlsPanel.SetActive(true);
            m_controlsPanelExitButton.SetActive(true);
            
            m_howToPlayPanel.SetActive(false);
            m_howToPlayPanelOpen = false;
        }
        else
        {
            m_controlsPanelExitButton.SetActive(false);
            m_controlsPanel.SetActive(false);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsPanelExitButton()
    {
        m_controlsPanelExitButton.SetActive(false);
        m_controlsPanel.SetActive(false);
    }

    public void HowToPlayPanelExitButton()
    {
        m_howToPlayPanelExitButton.SetActive(false);
        m_howToPlayPanel.SetActive(false);
    }
}
