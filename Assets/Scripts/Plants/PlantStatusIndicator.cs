using System;
using System.Collections;
using UnityEngine;

public class PlantStatusIndicator : MonoBehaviour
{
    private Animator m_plantStatusIndicatorAnimator;

    private void Start()
    {
        m_plantStatusIndicatorAnimator = GetComponent<Animator>();
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
    private IEnumerator IndicatorSpawner()
    {
        // Waits for a moment
        yield return new WaitForSeconds(0.1f);

        // Finds the position to spawn the indicator at
        Vector3 indicatorPos = (transform.position + (new Vector3(0f, 1f, 0f)));

        // Spawns indicator via VFXManager.cs
        VFXManager.CreateIndicator(indicatorPos);
    }

    private void PlantNeedsWaterIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isDying", false);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);

        IndicatorSpawner();
    }
    private void PlantDyingIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isDying", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", true);
        m_plantStatusIndicatorAnimator.SetBool("isHappy", false);

        IndicatorSpawner();
    }
    private void PlantHappyIndicator()
    {
        m_plantStatusIndicatorAnimator.SetBool("isHappy", true);
        m_plantStatusIndicatorAnimator.SetBool("needsWater", false);
        m_plantStatusIndicatorAnimator.SetBool("isDying", false);

        IndicatorSpawner();
    }
}
