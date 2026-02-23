using UnityEngine;
using UnityEngine.InputSystem;

public class NewMovement : MonoBehaviour
{
    Animator anim;
    PlayerInput playerInput;
    public CharacterController controller;
    public Transform cam;
    
    float turnSmoothVelocity;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;

    public float turnSmoothTime = 0.5f;
    float speed;
    public float walkSpeed = 6f;
    public float sprintSpeed = 9f;

    //[SerializeField] float jumpHeight = 6;

    
    public float gravity = -9.81f;
    float verticalVelocity;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();

        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
        sprintAction = playerInput.actions.FindAction("Sprint");

        verticalVelocity = 0;

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalk ();
        PlayerSprint();
        PlayerJump();
        PlayerLanded();
    }

    void PlayerWalk()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        

        if (direction.magnitude >= 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            // play the walk anim
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }


    void PlayerSprint()
    {
        if (sprintAction.IsPressed() && moveAction.ReadValue<Vector2>().magnitude > 0.1f)
        {
            speed = sprintSpeed;
            anim.SetFloat("Speed", 2); 
        }
        else
        {
            speed = walkSpeed;
        }
    }


    void PlayerJump()
    {
        if (jumpAction.WasPressedThisFrame() )
        {

            if (isGrounded && verticalVelocity <= 0)
            {
                verticalVelocity = 12f;
                Debug.Log("Jump pressed");

                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }
        }

        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        isGrounded = controller.isGrounded;
        print("grounded=" + isGrounded + "  vv=" + verticalVelocity);

    }


    void PlayerLanded()
    {
        if( isGrounded && verticalVelocity < -2 )
        {
            verticalVelocity = -2;
            anim.SetBool("isJumping", false);


        }
    }
}
