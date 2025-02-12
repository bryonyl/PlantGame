using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputManager m_inputManager;
    
    public void PauseGame()
    {
        if (InputManager.m_gameIsPaused)
        {
            // Stops all time-based operations (movement, physics, animation, time system, etc.)
            Time.timeScale = 0f;
            AudioListener.pause = true;
            m_inputManager.DisablePlayerInput();
        }
        else
        {
            // Resumes all time-based operations
            Time.timeScale = 1;
            AudioListener.pause = false;
            m_inputManager.EnablePlayerInput();
        }
    }
}
