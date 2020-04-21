using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
 *      JUMP BACKWARDS.
 *
 *      
 * 
 * 
 */

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
    private float walkSpeed = 4f;
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
    bool isIdle, isWalking, isRunning, isCrouching;
    [SerializeField]
    Animator anim;
    AnimatorClipInfo[] animClipInfo;
     private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void LateUpdate()
    {
        //GroundingControlls
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        //Movement Controlls
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (isGrounded)
        {
            if (z > 0 || x > 0 && !isRunning)
            {   //Walk Forwards
                isWalking = true;
                isIdle = false;
                anim.SetBool("isWalking", true);
                anim.SetBool("isWalkingBackwards", false);
                anim.SetBool("isIdle", false);
            }
            else if (z < 0 || x < 0 && !isRunning)
            {   //Walk backwards
                anim.SetBool("isWalkingBackwards", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);

                controller.Move(move * crouchSpeed * Time.deltaTime);
            }
            else if (z == 0 && x == 0)
            {   //Stop Walking
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBackwards", false);
                anim.SetBool("isIdle", true);
            }

            if (isWalking)
            {
                if (x > 0)
                {   //Walk Strafe Right
                    anim.SetBool("isStrafingRight", true);
                    anim.SetBool("isStrafingLeft", false);
                    anim.SetBool("isIdle", false);
                }
                else if (x < 0)
                {   //walkSpeed Strafe Left
                    anim.SetBool("isStrafingLeft", true);
                    anim.SetBool("isStrafingRight", false);
                    anim.SetBool("isIdle", false);
                }
                controller.Move(move * walkSpeed * Time.deltaTime);


            }

            if (isRunning)
            {
                if (x > 0)
                {   //Run Strafe Right
                    anim.SetBool("isStrafingRight", true);
                    anim.SetBool("isStrafingLeft", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else if (x < 0)
                {   //Run Strafe Left
                    anim.SetBool("isStrafingLeft", true);
                    anim.SetBool("isStrafingRight", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else
                {   //Stop Strafing.
                    anim.SetBool("isStrafingRight", false);
                    anim.SetBool("isStrafingLeft", false);
                }
            }

            if (isCrouching)
            {
                if (z > 0)
                {   //Crouch Walk Forwards
                    anim.SetBool("isCrouching", true);
                    anim.SetBool("isWalking", true);
                }
                else if (z < 0)
                {   //Crouch Walk Backwards
                    anim.SetBool("isWalkingBackwards", true);
                    anim.SetBool("isWalking", false);
                }
                else if (x > 0)
                {   //Crouch Walk Right
                    anim.SetBool("isCrouching", true);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isStrafingRight", true);
                    anim.SetBool("isStrafingLeft", false);

                }
                else if (x < 0)
                {   //Crouch Walk Left
                    anim.SetBool("isCrouching", true);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isStrafingLeft", true);
                    anim.SetBool("isStrafingRight", false);

                }
                else
                {
                    //anim.SetBool("isWalking", false);
                }
            }

        }


        if (Input.GetButtonDown("Jump"))
        {           
            if (isIdle)
            {
                anim.SetTrigger("isJumping");
            }
            else if(isWalking)
            {
                anim.SetTrigger("isJumping");
            }
            else if (isRunning)
            {
                anim.SetTrigger("isJumping");
            }
            else if(isCrouching)
            {
                //Jumping from crouching position?
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            if (PlayerStats.MyInstance.CurrentStamina > 0)
            {
                isRunning = true;
                if (isWalking)
                {
                    Run(move);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            isRunning = false;
            isWalking = true;
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                StartCoroutine(Crouch(move));
                isCrouching = true;
            }
            else
            {
                StartCoroutine(StopCrouch());
                isCrouching = false;
            }
        }
        
    }

    public void Run(Vector3 move)
    {  
        while (isRunning)
        {
            PlayerStats.MyInstance.CurrentStamina -= 0.2f;
            isWalking = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
            controller.Move(move * runSpeed * Time.deltaTime);

            break;
        }
    }

    public IEnumerator Crouch(Vector3 move)
    {
        anim.SetBool("isCrouching", true);
        yield return new WaitForSeconds(.6f);
    }
    public IEnumerator StopCrouch()
    {
        anim.SetBool("isCrouching", false);
        yield return new WaitForSeconds(1.5f);

    }
}
