using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    void Update()
    {
        // Defines the origin of the ray - from the mouse position
        Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Defines the direction of the ray - straight down
        Vector3 rayDirection = Vector3.down; // Changes to preferred direction?

        // Defines the maximum distance that the ray can travel
        float maxDistance = 10f;

        // Perform the raycast
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, maxDistance))
        {
            // Something was hit by the raycast
            Debug.Log("Raycast hit object: " + hitInfo.collider.gameObject.name);
            Debug.Log("Collision point: " + hitInfo.point);

            // Draws a debug line to show the raycast in Scene view
            Debug.DrawRay(rayOrigin, rayDirection * hitInfo.distance, Color.red);
        }
        else
        {
            // Nothing was hit by the raycast
            Debug.Log("Raycast did not hit anything");
        }
    }
}
