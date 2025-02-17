using System;
using System.Collections;
using UnityEngine;

public class PlantStatusIndicator : MonoBehaviour
{
    private Animator m_plantStatusIndicatorAnimator;
    [SerializeField] GameObject m_plantStatusIndicatorPrefab;

    private void Start()
    {
        GameObject spawnedPlantStatusIndicator = Instantiate(m_plantStatusIndicatorPrefab);
        m_plantStatusIndicatorAnimator = spawnedPlantStatusIndicator.GetComponent<Animator>();
        if (m_plantStatusIndicatorAnimator == null)
        {
            Debug.LogError($"{m_plantStatusIndicatorAnimator} is null!");
        }
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
