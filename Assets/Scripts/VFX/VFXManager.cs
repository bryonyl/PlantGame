using UnityEngine;

public class VFXManager : MonoBehaviour
{
    // The instance that will be accessible to other scripts
    // Here, static means it's not a member of the class but exists across ALL instances of the class
    // We can access it from anywhere with VFXManager.s_instance
    public static VFXManager s_instance; // s_ prefix stands for singleton

    // Indicator prefab to be instantiated later
    [SerializeField] private GameObject m_indicatorPrefab;

    private void Awake()
    {
        // Sets the instance to this if it's null
        if (s_instance == null)
            s_instance = this;
    }

    // This is a static function, so we don't need an instance of the class to run the function
    public static GameObject CreateIndicator(Vector3 position, float destroyAfter = 0.5f)
    {
        // Check if instance is null
        if (s_instance == null)
        {
            Debug.LogError("Tried to spawn an indicator but the instance hasn't been set");
            return null;
        }

        // If not, spawn indicator
        GameObject indicator = Instantiate(s_instance.m_indicatorPrefab, position, Quaternion.identity);

        // Destroy indicator after certain amount of time
        Destroy(indicator, destroyAfter);

        // Return a reference to the indicator
        return indicator;
    }
}
