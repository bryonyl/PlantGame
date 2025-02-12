using System;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class WaterBobbing : MonoBehaviour
{
    [SerializeField] private Transform m_startWayPoint;
    [SerializeField] private Transform m_endWayPoint;
    [SerializeField] private float m_speed = 5;
    private Transform m_target;

    void Start()
    {
        m_target = m_endWayPoint;
    }

    private void Update()
    {
        // Moves object towards target destination
        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (m_target == m_endWayPoint)
        {
            m_target = m_startWayPoint;
        }
        else
            m_target = m_endWayPoint;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingSeaWaypoint"))
        {
            ChangeTarget();
        }
    }
}
