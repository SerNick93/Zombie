using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement myInstance { get; set; }
    public static PlayerMovement MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<PlayerMovement>();
            }
            return myInstance;
        }
    }

    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }

    private CharacterController controller;
    [SerializeField]
    private float moveSpeed = 10f;
    private float originalMoveSpeed = 4f;
    

    [SerializeField]
    private float runSpeed = 6f;
    [SerializeField]
    private float crouchSpeed = 3f;
    Vector3 velocity;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    bool isWalking, isRunning, isCrouching;
    [SerializeField]
    Animator anim;
    AnimatorClipInfo[] animClipInfo;
     private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void LateUpdate()
    {
        float walkDirection = 0;
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if (!isGrounded)
        {

            if (velocity.y < 0)
            {
                //velocity.y = -2f;
            }
        }
        /*WALK
         *      WALK STRAIGHT - Z > 0
         *      WALK BACKWARDS - Z < 0
         *      STRAFE LEFT X < 0
         *      STRAFE RIGHT X > 0
         *RUN
         *      RUN STRAIGHT
         *      RUN BACKWARDS
         *      RUN STRAFE LEFT
         *      RUN STRAFE RIGHT
         *      
         * 
         * JUMP
         *      CHANGE THE BOOL TO A TRIGGER, SO IT DOESN'T HAVE ANY CONDITIONS TO MEET
         *      USE INTERUPTION SOURCE TO GET RID OF SOME OF THE INCONSISTNCIES OF
         *      THE ANIMATION JOINS
         * 
         * 
         */




        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Set strafing speed.
        if (x > 0)
        {
            anim.SetBool("isStrafingRight", true);
            //Strafe Right
        }
        else if(x < 0)
        { 
            anim.SetBool("isStrafingLeft", true);
            //Straftr Left
        }
        else if (x == 0)
        {
            anim.SetBool("isStrafingLeft", false);
            anim.SetBool("isStrafingRight", false);
        }
        if (x > 0 && isRunning)
        {
            anim.SetBool("isRunning", true); 
            //Straft Right + Run
        }
        if (x < 0 && isRunning)
        {
            Debug.Log("Runstrafe left");
            anim.SetBool("isRunning", true);

            //Straft Left + Run
        }

        if (z > 0 && isGrounded)
        {
            isWalking = true;
            anim.SetBool("isWalking", true);
        }
        else
        {
            isWalking = false;
            anim.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            /*If Collider in front finds an object with tag 
             * climb, use the climb animation instad.
             * Get the height of the wall to detemine which climb is needed. 
             *
             * 
             */
             StartCoroutine(Jump());
        }

        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;

            if (PlayerStats.MyInstance.CurrentStamina > 0 && isWalking)
            {
                Run();
                PlayerStats.MyInstance.CurrentStamina -= 0.2f;
            }
            else
            {
                //anim.SetBool("isRunning", false);
                //isRunning = false;
                moveSpeed = originalMoveSpeed;
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
            moveSpeed = originalMoveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                StartCoroutine(Crouch());
                isCrouching = true;
            }
            else
            {
                StartCoroutine(StopCrouch());
                isCrouching = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Climb")
        {
        }
    }

    public void Run()
    {  
        while (isRunning)
        {
            Debug.Log(moveSpeed);
            moveSpeed = runSpeed;
            anim.SetBool("isRunning", true);
            break;
        }
    }

    public IEnumerator Crouch()
    {
        moveSpeed = crouchSpeed;
        anim.SetBool("isCrouching", true);
        yield return new WaitForSeconds(.6f);
        controller.center = new Vector3(0, Mathf.Lerp(.73f,1.2f,1f),0f);
    }
    public IEnumerator StopCrouch()
    {
        anim.SetBool("isCrouching", false);
        moveSpeed = originalMoveSpeed;
        yield return new WaitForSeconds(1.5f);
        controller.center = new Vector3(0, Mathf.Lerp(1.2f, .73f,1f),0f);

    }
    IEnumerator Jump()
    {

        //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        anim.SetBool("isJumping", true);
        if (isCrouching)
        {
            controller.center = new Vector3(0, Mathf.Lerp(1.2f, .73f, 1f), 0f);
        }
        yield return new WaitForSeconds(2.15f);
        if (isCrouching)
        {
            anim.SetBool("isCrouching", false);
        }

        anim.SetBool("isJumping", false);
    }

}
