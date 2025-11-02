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
    SpriteRenderer m_spriteRenderer = null;

    [SerializeField]
    InputAction movementAction;
    [SerializeField]
    InputAction jumpAction;

    public float maxSpeed = 5.0f;
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

    public bool candleZoneToggle = false;
    public float candleZoneTimer = 10.0f;
    public float candleZoneDelay = 3.0f;
    public Vector3 initPosition;
    private Vector3 displaceLeft = new Vector3(-2.0f, 0.0f, 0.0f);
    private Vector3 displaceRight = new Vector3(2.0f, 0.0f, 0.0f);


    public Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
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
        bool is_grounded = collide_ground;

        if (movementInput.x != 0.0f) // Acelera
        {
            float sign = Mathf.Sign(movementInput.x);
            if (candleZoneToggle) sign = -sign;
            velocity.x = Mathf.Lerp(velocity.x, sign * maxSpeed, acceleration);
        }
        else // Desacelera
        {
            velocity.x = Mathf.Lerp(velocity.x, 0.0f, deceleration);
        }

        if (is_grounded)
        {
            m_rigidbody2D.gravityScale = gravity;
            if (jumpInput) // Aplica salto
            {
                velocity.y = jumpForce;
            }
        }
        else if (!levitatin)
        {
            velocity.y -= gravity;
            velocity.y = velocity.y < -maxFallSpeed ? -maxFallSpeed : velocity.y;
        }

        if (touch_powerup) // Forzar salto
        {

            StopAllCoroutines();
            StartCoroutine(levitate(1));
        }

        m_rigidbody2D.linearVelocity = velocity;

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

    private void ToggleCandleStatus()
    {
        if (candleZoneToggle)
        {
            // Llegas a vela destino, vuelves a rojo y paras corrutinas
            StopAllCoroutines();
            candleZoneToggle = false;
            m_spriteRenderer.color = Color.red;
        } else
        {
            // Sales de vela origen, guardas posicion y empiezas corrutina
            initPosition = gameObject.transform.position;
            m_spriteRenderer.color = Color.orange;
            StartCoroutine(delayedToggle(candleZoneDelay));
            StartCoroutine(candleManager(candleZoneTimer + candleZoneDelay));
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "orbe")
        {
            touch_powerup = true;
        }
        if (collider.gameObject.tag == "vela")
        {
            ToggleCandleStatus();
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "orbe")
        {
            touch_powerup = false;
        }
        // if (collider.gameObject.tag == "vela")
        // {
        //     ToggleCandleStatus();
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Ground")
        {
            collide_ground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Ground")
        {
            collide_ground = false;
        }
    }

    void returnToFirstCandle(Vector3 position, Vector3 offset)
    {
        // Salir artificialmente de la zona de velas
        // Anular el toggle, teleport a la zona position (+offset para no pisar la vela)
        candleZoneToggle = !candleZoneToggle;
        this.transform.position = position + offset;
        m_spriteRenderer.color = Color.red;
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

    IEnumerator candleManager(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        returnToFirstCandle(initPosition, displaceLeft);
        yield break;
    }

    IEnumerator delayedToggle(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_spriteRenderer.color = Color.blue;
        candleZoneToggle = !candleZoneToggle;
        yield break;
    }
}
