using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody2D = null;
    BoxCollider2D m_boxCollider2D = null;
    Animator m_animator = null;

    [SerializeField]
    InputAction movementAction;
    [SerializeField]
    InputAction jumpAction;

    public float maxSpeed = 3.0f;
    [Range(0.0f, 1.0f)]
    public float acceleration = 1.8f;
    [Range(0.0f, 1.0f)]
    public float deceleration = 0.8f;

    public float jumpForce = 100.0f;
    public float maxFallSpeed = 10.0f;
    public float gravity = 1.0f;
    public float levitate_gravity = -0.15f;

    public LayerMask groundLayer;
    [Range(0.0f, 1.0f)]
    public float groundCheckOffset = 0.1f;

    private Vector2 movementInput = Vector2.zero;
    private bool jumpInput = false;
    private bool touch_powerup = false;
    public bool levitatin = false;
    public bool collide_ground = false;
    private Vector2 direction = new Vector2(1.0f, 1.0f);


    public Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();  
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //transform.position = new Vector3(1.0f, 1.0f, 0.0f);
    }

    void OnEnable()
    {
        movementAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        //bool is_grounded = GroundCheck();
        bool is_grounded = collide_ground;

        if (movementInput.x != 0.0f) // Acelera
        {
            velocity.x = Mathf.Lerp(velocity.x, Mathf.Sign(movementInput.x) * maxSpeed, acceleration);
        }
        else // Desacelera
        {
            velocity.x = Mathf.Lerp(velocity.x, 0.0f, deceleration);
        }
        
        if (is_grounded)
        {
            m_rigidbody2D.gravityScale = gravity;
            //Debug.Log("jumpable");
            if (jumpInput) // Aplica salto
            {
                velocity.y = jumpForce;
                //Debug.Log("salto");
            }
        } 
        else if(!levitatin)
        {
            velocity.y -= gravity;
            velocity.y = velocity.y < -maxFallSpeed ? -maxFallSpeed : velocity.y;
        }

        if (touch_powerup) // Forzar salto
        {
            //velocity.y = velocity.y + 5.0f;
            
            StopAllCoroutines();
            StartCoroutine(levitate(1));
            //m_rigidbody2D.AddForce(direction * jumpForce, ForceMode2D.Impulse);
        }

        m_rigidbody2D.linearVelocity = velocity;
        //m_animator.speed = velocity.x;

        ConsumeInput();
    }

    private void CheckInput() 
    {
        movementInput = movementAction.ReadValue<Vector2>();
        jumpInput = jumpAction.triggered | jumpInput;
    }

    private void ConsumeInput() 
    {
        jumpInput = false;
    }

    // private bool GroundCheck() 
    // {
    //     Vector3 center = m_boxCollider2D.bounds.center;
    //     Vector3 extents = m_boxCollider2D.bounds.extents;

    //     Debug.DrawRay(m_boxCollider2D.bounds.min + Vector3.down * groundCheckOffset + Vector3.right * 0.02f, Vector2.right * m_boxCollider2D.bounds.size.x * 0.96f, Color.red);
    //     return Physics2D.Raycast(m_boxCollider2D.bounds.min + Vector3.down * groundCheckOffset + Vector3.right * 0.02f,
    //                              Vector2.right, m_boxCollider2D.bounds.size.x * 0.96f, groundLayer);
    // }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "orbe")
        {
            touch_powerup = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "orbe")
        {
            touch_powerup = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.name == "Ground")
        {
            collide_ground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.gameObject.name == "Ground")
        {
            collide_ground = false;
        }
    }

    IEnumerator levitate(int seconds)
    {
        
        velocity.y = 4;
        m_rigidbody2D.gravityScale = levitate_gravity;
        levitatin = true;
        yield return new WaitForSeconds(seconds);
        m_rigidbody2D.gravityScale = gravity;
        levitatin = false;
        yield break;
    }
}
