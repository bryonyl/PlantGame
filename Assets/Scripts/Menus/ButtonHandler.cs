using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void OpenLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
