using System;
using UnityEngine;
/// <summary>
/// Allows all managers to persist in DontDestroyOnLoad.
/// </summary>
public class SetUpManagers : MonoBehaviour
{
    private GameObject[] m_allManagers;
    private void Awake()
    {
        m_allManagers = GameObject.FindGameObjectsWithTag("Manager");
        foreach (GameObject manager in m_allManagers) DontDestroyOnLoad(manager);
    }
}
