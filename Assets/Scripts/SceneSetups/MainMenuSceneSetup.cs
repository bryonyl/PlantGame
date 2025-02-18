using System;
using UnityEngine;

public class MainMenuSceneSetup : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_rainEffect;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        m_rainEffect.Play();
    }
}
