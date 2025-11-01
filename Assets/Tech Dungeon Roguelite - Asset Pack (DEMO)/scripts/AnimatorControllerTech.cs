using UnityEngine;
using UnityEngine.InputSystem;
public class AnimatorControllerTech : MonoBehaviour
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

    void Start()
    {
        animator = GetComponent<Animator>(); //gets the animator component
        spriteRenderer = GetComponent<SpriteRenderer>();  //gets the sprite renderer component
    }

    void Update()
    {
        // float horizontal = Input.GetAxisRaw("Horizontal"); //gets horizontal input
        // float vertical = Input.GetAxisRaw("Vertical");     //gets vertical input

        float horizontal = Keyboard.current.aKey.isPressed ? -1 :
                   Keyboard.current.dKey.isPressed ? 1 : 0;
        float vertical  = Keyboard.current.sKey.isPressed ? -1 :
                   Keyboard.current.wKey.isPressed ? 1 : 0;
        
        animator.SetFloat(animator_x_axis_name, horizontal != 0 ? 1f : 0f); //sets the animators x axis varible to horizontal input
        animator.SetFloat(animator_y_axis_name,vertical != 0 ? 1f : 0f);   //sets the animators y axis varible to vertical input

        bool isMoving = (horizontal != 0 || vertical != 0); 
        animator.SetBool(animator_ismoving_bool_name, isMoving); //sets the animators is moving varible when moving

        bool actionPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        if (actionPressed)
        {
            animator.SetTrigger(animator_action_trigger_name); //triggers action animation
        }
        // else
        // {
        //     animator.SetBool(animator_action_trigger_name, false);//triggers action animation
        // }

        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0; // flips sprite when going left
        }
    }
}
