using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputManager m_inputManager;
    [SerializeField] private GameObject m_pauseMenu;

    public void PauseGame()
    {
        if (InputManager.m_gameIsPaused)
        {
            // Stops all time-based operations (movement, physics, animation, time system, etc.)
            Time.timeScale = 0f;
            AudioListener.pause = true;
            m_inputManager.DisablePlayerInput();
            m_pauseMenu.SetActive(true);
            
            EventSystem.current.SetSelectedGameObject(m_pauseMenu);
        }
        else
        {
            // Resumes all time-based operations
            Time.timeScale = 1;
            InputManager.m_gameIsPaused = false;
            AudioListener.pause = false;
            m_inputManager.EnablePlayerInput();
            m_pauseMenu.SetActive(false);
        }
    }
}
