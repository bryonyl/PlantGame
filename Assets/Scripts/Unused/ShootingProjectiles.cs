using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingProjectiles : MonoBehaviour
{
    // Reference to TopDownCharacterController
    public TopDownCharacterController TopDownCharacterController;

    // Shooting variables
    [Header("Projectile Parameters")]
    [SerializeField] GameObject m_projectilePrefab;
    [SerializeField] Transform m_firePoint;
    [SerializeField] float m_projectileSpeed;
    [SerializeField] float m_fireRate;
    private float m_fireTimeout = 0; // Projectiles firing timeout

    private void Fire()
    {
        // New Vector2D variable to be the last known direction of travel or at the very least, the default Vector2.down direction
        Vector2 fireDirection = TopDownCharacterController.m_lastDirection;

        if (fireDirection == Vector2.zero)
        {
            fireDirection = Vector2.down;
        }

        // Instantiate creates objects at runtime - perfect for spawning projectiles
        // m_projectilePrefab is the reference to the object we want to spawn
        // m_firePoint.position is the location where the object will be spawned
        // Quaternion.identity is the rotation at which the object will be spawned. As "identity" is used, there is no rotation to consider.
        GameObject projectileToSpawn = Instantiate(m_projectilePrefab, m_firePoint.position, Quaternion.identity);

        if (projectileToSpawn.GetComponent<Rigidbody2D>() != null) // Defensive programming example - checks the component is actually present before undertaking any actions on it
        {
            // Makes projectile move
            // m_playerDirection is normalized so that the projectile's magnitude remains as 1 and doesn't change to 0.71 when the player starts moving diagonally (which would make the projectile go slower) So the direction no longer matters when it comes to speed. 
            projectileToSpawn.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * m_projectileSpeed, ForceMode2D.Impulse); // ForceMode2D.Impulse is a single instant application of force, compared to a continuous force
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mousePointOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 8)); // Finds mouse position on the screen
        //Debug.Log(mousePointOnScreen);

        // check if an attack has been triggered.
        if (TopDownCharacterController.m_attackAction.IsPressed() && Time.time > m_fireTimeout)
        {
            Debug.Log("Attack action pressed");
            // Firing is possible if enough time has passed from the previous firing of the weapon
            m_fireTimeout = Time.time + m_fireRate; // Time.time returns the amount of time that has passed in seconds. When the amount of time that has passed exceeds m_fireRate, we can call our Fire() function
            Fire();
        }
    }

    private void Awake()
    {
        TopDownCharacterController = GetComponent<TopDownCharacterController>();
        // Binds movement input to variable
        TopDownCharacterController.m_attackAction = InputSystem.actions.FindAction("Attack");
    }
}
