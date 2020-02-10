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
    private float originalMoveSpeed = 6f;

    [SerializeField]
    private float runSpeed = 20f;
    Vector3 velocity;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    bool isGrounded;
    [SerializeField]
    float jumpHeight;
    bool isRunning;
     private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
            isRunning = true;
        }
        else
        {
            isRunning = false;
            moveSpeed = originalMoveSpeed;
        }
            
    }
    public void Run()
    {  
        while (isRunning)
        {
            moveSpeed = runSpeed;
            break;
        }
    }

}
