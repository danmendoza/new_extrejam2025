using UnityEngine;
using UnityEngine.InputSystem;
public class AnimatorControllerGJ : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("ANIMATION_CONFIG")]
    [Space(2)]
    public string animator_x_axis_name;
    [Space(5)]
    public string animator_y_axis_name;
    [Space(5)]
    public string animator_ismoving_bool_name;

    public string animator_action_trigger_name;

    [Space(5)]
    public string animator_triggerv1_name = "isV1";
    [Space(5)]
    public string animator_triggerv2_name = "isV2";
    [Space(5)]
    public string animator_triggerDead_name = "isDead";

    public bool isDead;

    public bool hasKey;
    public bool isOnStairs;
    public bool hasPants;

    public bool hasGloves;

    void Start()
    {
        animator = GetComponent<Animator>(); //gets the animator component
        spriteRenderer = GetComponent<SpriteRenderer>();  //gets the sprite renderer component
        hasKey = false;
        isOnStairs = false;
        hasPants = false;
        hasGloves = false;
        isDead = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //gets horizontal input
        float vertical = Input.GetAxisRaw("Vertical");     //gets vertical input

        animator.SetFloat(animator_x_axis_name, horizontal != 0 ? 1f : 0f); //sets the animators x axis varible to horizontal input
        animator.SetFloat(animator_y_axis_name, vertical != 0 ? 1f : 0f);   //sets the animators y axis varible to vertical input

        bool isMoving = (horizontal != 0 || vertical != 0);
        animator.SetBool(animator_ismoving_bool_name, isMoving); //sets the animators is moving varible when moving

        bool actionPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        if (actionPressed)
        {
            animator.SetTrigger(animator_action_trigger_name); //triggers action animation
        }


        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0; // flips sprite when going left
        }
    }

    public void transitionAnimation(AnimationState newState)
    {
        switch (newState)
        {

            case AnimationState.StateV1:
                Debug.Log("Transición a StateV1: animación alternativa 1.");
                animator.SetTrigger(animator_triggerv1_name);
                break;

            case AnimationState.StateV2:
                Debug.Log("Transición a StateV2: animación alternativa 2.");
                animator.SetTrigger(animator_triggerv2_name);
                break;

            case AnimationState.StateDead:
                Debug.Log("Transición a Dead: animación alternativa 3.");
                animator.SetTrigger(animator_triggerDead_name);
                break;

            default:
                Debug.LogWarning("Estado de animación no reconocido.");
                break;
        }
    }

    public void isDeadTransitionEnded()
    {
        Debug.Log("Dead Transition Ended called");
        GameManager.Instance.SpawnPlayer();

    }
}
