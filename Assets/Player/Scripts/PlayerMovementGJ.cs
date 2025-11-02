using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the player's movement mechanics in the game.
/// It uses the new Input System for keyboard controls and Rigidbody2D for physics-based movement.
/// </summary>
public class PlayerMovementGJ : MonoBehaviour
{
    [Header("PLAYER_CONFIG")]
    [Space(5)]
    public float moveSpeed = 5f; //players movement speed

    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to this GameObject

    private Vector2 movement; // Stores the current movement vector

    private AnimatorControllerGJ animController;

    private float originalGravity;
    
    public float bounceForce = 10.0f;

    /// <summary>
    /// Called before the first frame. Initializes components.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets rigidbody
        animController = GetComponent<AnimatorControllerGJ>();
        originalGravity = rb.gravityScale;
    }

    /// <summary>
    /// Called once per frame. Handles input processing.
    /// </summary>
    void Update()
    {
        // Get input
        // Get keyboard input using the new Input System
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ApplyBounce();
        }

        //movement.x = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 1 (derecha)
        if(animController.isOnStairs){
            movement.y = Input.GetAxisRaw("Vertical");   // -1 (abajo), 1 (arriba)
            rb.gravityScale = 0f;
        }
        else{
            movement.y = 0;
            rb.gravityScale = originalGravity;
        }
        
        movement.x = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 1 (derecha)
   
        movement = movement.normalized; // Normalize movement so diagonal movement isn't faster

        
        
    }

    /// <summary>
    /// Called at a fixed interval. Handles physics-based movement.
    /// </summary>
    void FixedUpdate()
    {
        
        rb.linearVelocity += movement * moveSpeed; // Apply movement to Rigidbody2D
    }

    public void ApplyBounce()
    {
        Debug.Log("APPLYING BOUNCE");
        // Calcula la dirección a 45 grados (derecha arriba por defecto)
        // Si quieres que vaya hacia la izquierda, usa Vector2(-1, 1)
        Vector2 bounceDirection = new Vector2(1f, 1f).normalized;
        
        // Aplica la velocidad directamente
        rb.linearVelocity = bounceDirection * bounceForce;
        
        // Alternativa: Usar AddForce para un impulso instantáneo
        rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
    }
}
