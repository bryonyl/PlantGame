using UnityEngine;

public class AutomaticallyDestroyProjectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
