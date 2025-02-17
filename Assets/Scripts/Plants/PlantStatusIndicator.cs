using System;
using System.Collections;
using UnityEngine;

public class PlantStatusIndicator : MonoBehaviour
{
    private Animator m_plantStatusIndicatorAnimator;

    private void Start()
    {
        m_plantStatusIndicatorAnimator = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlantGrowthManager.OnPlantNeedsWater += PlantNeedsWaterIndicator;
        PlantGrowthManager.OnPlantDying += PlantDyingIndicator;
        PlantGrowthManager.OnPlantHappy += PlantHappyIndicator;
    }
    private void OnDisable()
    {
        PlantGrowthManager.OnPlantNeedsWater -= PlantNeedsWaterIndicator;
        PlantGrowthManager.OnPlantDying -= PlantDyingIndicator;
        PlantGrowthManager.OnPlantHappy -= PlantHappyIndicator;
    }

    private void Update()
    {
        Debug.Log("Needs water = " + m_plantStatusIndicatorAnimator.GetBool("needsWater"));
        Debug.Log("Is dying = " + m_plantStatusIndicatorAnimator.GetBool("isDying"));
        Debug.Log("Is happy = " + m_plantStatusIndicatorAnimator.GetBool("isHappy"));
    }

    private void PlantNeedsWaterIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isDying", false);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);
    }
    private void PlantDyingIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isDying", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);
    }
    private void PlantHappyIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isHappy", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", false);
        m_plantStatusIndicatorAnimator.SetBool("isDying", false);
    }
}
