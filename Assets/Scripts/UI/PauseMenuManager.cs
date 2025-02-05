using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private void PauseGame()
    {
        // Stops all time-based operations (movement, physics, animation, time system, etc.)
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        // Resumes all time-based operations
        Time.timeScale = 1;
    }
}
