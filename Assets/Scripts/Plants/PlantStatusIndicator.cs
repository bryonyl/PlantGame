using System;
using System.Collections;
using UnityEngine;

public class PlantStatusIndicator : MonoBehaviour
{
    private Animator m_plantStatusIndicatorAnimator;
    private IndividualPlantGrowthManager m_individualPlantGrowthManager;

    private void Awake()
    {
        m_plantStatusIndicatorAnimator = gameObject.GetComponent<Animator>();
        m_individualPlantGrowthManager = GetComponentInParent<IndividualPlantGrowthManager>();

        if (m_individualPlantGrowthManager == null)
        {
            Debug.LogError(m_individualPlantGrowthManager + " is null!");
        }
    }
    
    private void OnEnable()
    {
        m_individualPlantGrowthManager.OnPlantNeedsWater += PlantNeedsWaterIndicator;
        m_individualPlantGrowthManager.OnPlantDead += PlantDeadIndicator;
        m_individualPlantGrowthManager.OnPlantHappy += PlantHappyIndicator;
    }
    private void OnDisable()
    {
        m_individualPlantGrowthManager.OnPlantNeedsWater -= PlantNeedsWaterIndicator;
        m_individualPlantGrowthManager.OnPlantDead -= PlantDeadIndicator;
        m_individualPlantGrowthManager.OnPlantHappy -= PlantHappyIndicator;
    }

    private void PlantNeedsWaterIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isDead", false);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);
    }
    private void PlantDeadIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isDead", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);
    }
    private void PlantHappyIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isHappy", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", false);
        m_plantStatusIndicatorAnimator.SetBool("isDead", false);
    }
}
