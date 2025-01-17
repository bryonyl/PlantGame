using System.Collections;
using UnityEngine;

public class IndicatorTest : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(IndicatorSpawner());
    }

    private IEnumerator IndicatorSpawner()
    {
        while (true)
        {
            // Waits for a moment
            yield return new WaitForSeconds(0.1f);

            // Finds the position to spawn the indicator at
            Vector3 indicatorPos = (transform.position + (new Vector3 (0f, 1f, 0f)));

            // Spawns indicator
            VFXManager.CreateIndicator(indicatorPos);
        }
    }
}
