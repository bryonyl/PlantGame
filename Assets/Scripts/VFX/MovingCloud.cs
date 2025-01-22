using Unity.VisualScripting;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    [SerializeField] private Transform m_startWayPoint;
    [SerializeField] private Transform m_endWayPoint;
    [SerializeField] private float m_speed = 5;

    private Vector2 m_teleportPosition;
    private Transform m_target; // Holds the start/end point, so the cloud knows where to go

    void Start()
    {
        m_target = m_endWayPoint;
        m_teleportPosition = m_startWayPoint.position;
    }

    void Update()
    {
        // Moves an object towards a target destination, never overshooting
        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime); // Time.deltaTime is the time in seconds between the last frame and the current frame.
                                                                                                                   // When using * Time.deltaTime, you can carry out actions over time
    }

    void ResetPosition()
    {
        if (m_target == m_endWayPoint)
        {
            transform.position = m_teleportPosition;
            m_target = m_endWayPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Trigger/overlap check to see if cloud touches a collider
    {
        if (collision.CompareTag("MovingObstacleWaypoint")) // If collider is MovingCloudWaypoint, then reset position
        {
            ResetPosition();
        }
    }
}