using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private MainMenuManager m_menuManager;

    public void OpenLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void SettingsFunctionality()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitPanel()
    {
        try
        {
            m_menuManager.currentPanelOpen.SetActive(false);
            m_menuManager.exitPanelButton.SetActive(false);
        }
        catch (System.Exception exNotFound)
        {
            Debug.LogError("currentPanelOpen of m_menuManager not found!");
        }
    }
}
