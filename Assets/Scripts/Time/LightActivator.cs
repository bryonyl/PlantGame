using System;
using UnityEngine;
using UnityEngine.AI;

public class LightActivator : MonoBehaviour
{
    private GameObject[] m_lightsInSceneArr;
    
    private void OnEnable()
    {
        TimeManager.OnHourChanged += LightActivationCheck;
        DayProgression.OnDayChanged += LightActivationCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged -= LightActivationCheck;
        DayProgression.OnDayChanged -= LightActivationCheck;
    }

    private void Start()
    {
        m_lightsInSceneArr = GameObject.FindGameObjectsWithTag("Light");

        SwitchAllLights(false);
    }

    private void LightActivationCheck()
    {
        if (TimeManager.m_hour == 20)
        {
            SwitchAllLights(true);
        }
        else
        {
            SwitchAllLights(false);
        }
    }
    
    private bool SwitchAllLights(bool state)
    {
        if (state == true)
        {
            foreach (GameObject light in m_lightsInSceneArr)
            {
                light.SetActive(true);
            }

            return true;
        }
        else
        {
            foreach (GameObject light in m_lightsInSceneArr)
            {
                light.SetActive(false);
            }

            return false;
        }
    }
}
