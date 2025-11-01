using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the player's movement mechanics in the game.
/// It uses the new Input System for keyboard controls and Rigidbody2D for physics-based movement.
/// </summary>
public class PlayerMovementTech : MonoBehaviour
{
    [Header("PLAYER_CONFIG")]
    [Space(5)]
    public float moveSpeed = 5f; //players movement speed

    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to this GameObject

    private Vector2 movement; // Stores the current movement vector

    /// <summary>
    /// Called before the first frame. Initializes components.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets rigidbody
       
    }

    /// <summary>
    /// Called once per frame. Handles input processing.
    /// </summary>
    void Update()
    {
        // Get input
        // Get keyboard input using the new Input System
        /*
        movement.x = Keyboard.current.aKey.isPressed ? -1 :
                   Keyboard.current.dKey.isPressed ? 1 : 0;
        movement.y = Keyboard.current.sKey.isPressed ? -1 :
                   Keyboard.current.wKey.isPressed ? 1 : 0;
                   */

        movement.x = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 1 (derecha)
        movement.y = Input.GetAxisRaw("Vertical");   // -1 (abajo), 1 (arriba)
        
        movement = movement.normalized; // Normalize movement so diagonal movement isn't faster
        
    }

    /// <summary>
    /// Called at a fixed interval. Handles physics-based movement.
    /// </summary>
    void FixedUpdate()
    {
        
        rb.linearVelocity = movement * moveSpeed; // Apply movement to Rigidbody2D
    }
}
