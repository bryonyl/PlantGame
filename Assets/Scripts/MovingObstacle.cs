using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform m_startWayPoint;
    [SerializeField] private Transform m_endWayPoint;
    [SerializeField] private float m_speed = 5;

    private Transform m_target; // Holds the start/end point, so the object knows where to go to

    public PlayerHealth PlayerHealth;

    void ChangeTarget()
    {
        if (m_target == m_startWayPoint)
        {
            m_target = m_endWayPoint;
        }
        else
        {
            m_target = m_startWayPoint;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();         
        m_target = m_startWayPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves an object towards a target destination, never overshooting
        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime); // Time.deltaTime is the time in seconds between the last frame and the current frame.
                                                                                                                   // When using * Time.deltaTime, you can carry out actions over time - this is useful
                                                                                                                   // when it comes to timers and fire rates, for example.

        // Makes fireball spin
        transform.Rotate(0,0,3.0f);
        
        //// Lerp attempt
        
        //bool switchDirection = false;
        //float timeElapsed = 1f;
        //Vector3 somewhereBetween;
        
        //if (switchDirection == false)
        //{
        //    somewhereBetween = Vector3.Lerp(m_startWayPoint.position, m_endWayPoint.position, timeElapsed);
        //    transform.position = somewhereBetween;
        //    switchDirection = true;
        //}
        //else if (switchDirection == true)
        //{
        //    somewhereBetween = Vector3.Lerp(m_endWayPoint.position, m_startWayPoint.position, timeElapsed);
        //    transform.position = somewhereBetween;
        //    switchDirection = true;
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision) // Trigger/overlap check to see if we touch anything
                                                        // The collision parameter can be used to get info about what we hit, e.g. whether it's a tag, a layer, components, a script...
    {
        if (collision.CompareTag("MovingObstacleWaypoint")) // Checking the tag of the thing we have overlapped with. If there is a tag, call ChangeTarget function
        {
            ChangeTarget();
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is attacked!");
            PlayerHealth.m_playerCurrentHealth = PlayerHealth.m_playerCurrentHealth - 10;
            Debug.Log($"Player health = {PlayerHealth.m_playerCurrentHealth}");
        }
    }

}