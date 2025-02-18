using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_playButton;
    [SerializeField] private GameObject m_controlsPanel;
    [SerializeField] private GameObject m_controlsPanelBackButton;
    [SerializeField] private GameObject m_exitToMainMenuButton;
    [SerializeField] private GameObject m_quitButton;
    
    [SerializeField] private PauseMenuManager m_pauseMenuManager;
    
    private bool m_controlsPanelOpen = true;

    public void ResumeGame()
    {
        InputManager.m_gameIsPaused = false;
        m_pauseMenuManager.PauseGame();
    }

    public void ToggleControlsPanel()
    {
        if (m_controlsPanelOpen)
        {
            m_controlsPanel.SetActive(true);
            m_controlsPanelBackButton.SetActive(true);
        }
        else
        {
            m_controlsPanelBackButton.SetActive(false);
            m_controlsPanel.SetActive(false);
        }
    }
    
    public void ControlsPanelBackButton()
    {
        m_controlsPanelBackButton.SetActive(false);
        m_controlsPanel.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
