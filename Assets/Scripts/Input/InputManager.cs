using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Hotbar Controller
    
    [SerializeField] private InventoryManager inventoryManager;
    
    /// <summary>
    /// The controls the player uses to navigate the hot bar. Checks if any key between 1-6 is pressed, as this is how many hot bar slots there are.
    /// Changes the selected slot via InventoryManager.cs if these criteria is fulfilled.
    /// </summary>
    private void HotBarControls() // Called in Update
    {
        if (Input.inputString != null) // Checks if any key is pressed
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); // Checks if pressed key is a number
            if (isNumber && number > 0 && number < 7) // If number is between range for hot bar selection
            {
                inventoryManager.ChangeSelectedSlot(number - 1); // Change selected slot
            }
        }
    }
    
    #endregion
    
    #region Character Controller (from framework)

    // The inputs that we need to retrieve from the input system.
    private InputAction m_moveAction;

    // The components that we need to edit to make the player move smoothly.
    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    
    // The direction that the player is moving in.s
    private Vector2 m_playerDirection;

    // The last direction the player was pointing in.
    public Vector2 m_lastDirection;   

    [Header("Movement parameters")]
    //The speed at which the player moves
    [SerializeField] private float m_playerSpeed = 200f;
    //The maximum speed the player can move
    [SerializeField] private float m_playerMaxSpeed = 1000f;
    
    private void Awake()
    {
        //bind movement inputs to variables
        m_moveAction = InputSystem.actions.FindAction("Move");
        
        //get components from Character game object so that we can use them later.
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        //clamp the speed to the maximum speed for if the speed has been changed in code.
        float speed = m_playerSpeed > m_playerMaxSpeed ? m_playerMaxSpeed : m_playerSpeed;
        
        //apply the movement to the character using the clamped speed value.
        m_rigidbody.linearVelocity = m_playerDirection * (speed * Time.fixedDeltaTime);
    }
    
    private void Update()
    {
        // Constantly monitors for the player pressing a hot bar key
        HotBarControls();
        
        // Stores any movement inputs into m_playerDirection - this will be used in FixedUpdate to move the player.
        m_playerDirection = m_moveAction.ReadValue<Vector2>();
        
        // Updates the animator speed to ensure that we revert to idle if the player doesn't move.
        m_animator.SetFloat("Speed", m_playerDirection.magnitude);
        
        // If there is movement, set the directional values to ensure the character is facing the way they are moving.
        if (m_playerDirection.magnitude > 0)
        {
            m_animator.SetFloat("Horizontal", m_playerDirection.x);
            m_animator.SetFloat("Vertical", m_playerDirection.y);
            
            // Makes sure m_lastDirection will always be a Vector2D value greater than (0, 0) and therefore an actual direction
            m_lastDirection = m_playerDirection;
        }
    }
    #endregion
}
