using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_retryButton;
    [SerializeField] private GameObject m_exitToMainMenuButton;
    [SerializeField] private GameObject m_quitButton;
    
    public void OpenMainLevel()
    {
        SceneManager.LoadScene(1);
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
