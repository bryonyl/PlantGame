using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Input system
    public InputSystem_Actions m_playerControls;
    private InputAction m_movementControls;
    private InputAction m_pauseGameControls;
    private InputAction m_hotbarSlot1Controls;
    private InputAction m_hotbarSlot2Controls;
    private InputAction m_hotbarSlot3Controls;
    private InputAction m_hotbarSlot4Controls;
    private InputAction m_hotbarSlot5Controls;
    private InputAction m_hotbarSlot6Controls;
    
    // Components
    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private PauseMenuManager m_pauseMenuManager;

    [Header("Movement Parameters")]
    [SerializeField] private float m_playerSpeed = 200f;
    [SerializeField] private float m_playerMaxSpeed = 1000f;
    private Vector2 m_playerDirection;
    private Vector2 m_lastDirection;
    
    // Pause menu variables
    public static bool m_gameIsPaused = false;

    private void OnEnable()
    {
        EnablePlayerInput();
    }

    private void OnDisable()
    {
        DisablePlayerInput();
    }

    private void Awake()
    {
        // Initialises InputSystem_Actions script
        m_playerControls = new InputSystem_Actions();

        // Binds inputs to variables
        m_movementControls = InputSystem.actions.FindAction("Move");
        m_pauseGameControls = InputSystem.actions.FindAction("PauseGame");
        m_hotbarSlot1Controls = InputSystem.actions.FindAction("HotbarSlot1");
        m_hotbarSlot2Controls = InputSystem.actions.FindAction("HotbarSlot2");
        m_hotbarSlot3Controls = InputSystem.actions.FindAction("HotbarSlot3");
        m_hotbarSlot4Controls = InputSystem.actions.FindAction("HotbarSlot4");
        m_hotbarSlot5Controls = InputSystem.actions.FindAction("HotbarSlot5");
        m_hotbarSlot6Controls = InputSystem.actions.FindAction("HotbarSlot6");

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Stores any movement inputs into m_playerDirection - this will be used in FixedUpdate to move the player.
        m_playerDirection = m_movementControls.ReadValue<Vector2>();

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

    private void FixedUpdate()
    {
        // Clamps the speed to the maximum speed for if the speed has been changed in code.
        float speed = m_playerSpeed > m_playerMaxSpeed ? m_playerMaxSpeed : m_playerSpeed;

        // Applies the movement to the character using the clamped speed value.
        m_rigidbody.linearVelocity = m_playerDirection * (speed * Time.fixedDeltaTime);
    }

    public void DisablePlayerInput()
    {
        m_movementControls.Disable();

        m_pauseGameControls.Disable();
        
        m_hotbarSlot1Controls.Disable();
        m_hotbarSlot2Controls.Disable();
        m_hotbarSlot3Controls.Disable();
        m_hotbarSlot4Controls.Disable();
        m_hotbarSlot5Controls.Disable();
        m_hotbarSlot6Controls.Disable();
    }

    public void EnablePlayerInput()
    {
        m_movementControls = m_playerControls.Player.Move;
        m_movementControls.Enable();

        m_pauseGameControls = m_playerControls.Player.PauseGame;
        m_pauseGameControls.Enable();
        m_pauseGameControls.performed += PauseGame;

        m_hotbarSlot1Controls = m_playerControls.Player.HotbarSlot1;
        m_hotbarSlot1Controls.Enable();
        m_hotbarSlot1Controls.performed += SelectHotbarSlot1;
        
        m_hotbarSlot2Controls = m_playerControls.Player.HotbarSlot2;
        m_hotbarSlot2Controls.Enable();
        m_hotbarSlot2Controls.performed += SelectHotbarSlot2;
        
        m_hotbarSlot3Controls = m_playerControls.Player.HotbarSlot3;
        m_hotbarSlot3Controls.Enable();
        m_hotbarSlot3Controls.performed += SelectHotbarSlot3;
        
        m_hotbarSlot4Controls = m_playerControls.Player.HotbarSlot4;
        m_hotbarSlot4Controls.Enable();
        m_hotbarSlot4Controls.performed += SelectHotbarSlot4;
        
        m_hotbarSlot5Controls = m_playerControls.Player.HotbarSlot5;
        m_hotbarSlot5Controls.Enable();
        m_hotbarSlot5Controls.performed += SelectHotbarSlot5;
        
        m_hotbarSlot6Controls = m_playerControls.Player.HotbarSlot6;
        m_hotbarSlot6Controls.Enable();
        m_hotbarSlot6Controls.performed += SelectHotbarSlot6;
    }
    
    private void PauseGame(InputAction.CallbackContext context)
    {
        m_gameIsPaused = !m_gameIsPaused;
        m_pauseMenuManager.PauseGame();
    }
    
    private void SelectHotbarSlot1(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(0);
    }

    private void SelectHotbarSlot2(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(1);
    }
    
    private void SelectHotbarSlot3(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(2);
    }
    
    private void SelectHotbarSlot4(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(3);
    }
    
    private void SelectHotbarSlot5(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(4);
    }
    
    private void SelectHotbarSlot6(InputAction.CallbackContext context)
    {
        m_inventoryManager.ChangeSelectedSlot(5);
    }
}
