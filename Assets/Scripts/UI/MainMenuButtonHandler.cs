using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private MenuManager m_menuManager;

    public void OpenLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void SettingsFunctionality()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ExitPanel()
    {
        try
        {
            m_menuManager.m_currentPanelOpen.SetActive(false);
        }
        catch (System.Exception exNotFound)
        {
            Debug.Log($"{m_menuManager.m_currentPanelOpen}");
            Debug.LogError("[ERROR] m_currentPanelOpen of m_menuManager not found!");
        }
    }
}
