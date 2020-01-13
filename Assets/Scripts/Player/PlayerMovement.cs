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

    public int FacingDirection { get => facingDirection; set => facingDirection = value; }
    public Quaternion BulletOrientation { get => bulletOrientation; set => bulletOrientation = value; }

    Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed = 25;
    private Animator animator;
    private Vector2 direction;
    [SerializeField]
    private TextMeshProUGUI pickupText;
    [SerializeField]
    private int facingDirection = 0;
    private Quaternion bulletOrientation; //the orientation of the bullet prefab
    [SerializeField]
    Transform bullet;

    // Start is called before the first frame update
    void Start()
    {
       Physics2D.IgnoreLayerCollision(9, 8);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        //This will not work on a touch device.
        //the player's facing direction will need to be found another way. 
        if (Input.GetKeyDown(KeyCode.A))
        {
            facingDirection = 1;
            FireGun.MyInstance.SetDirectionGunImage(facingDirection);
            //bulletOrientation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            facingDirection = 0;
            FireGun.MyInstance.SetDirectionGunImage(facingDirection);

            //bulletOrientation = Quaternion.Euler(180, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            facingDirection = 3;
            FireGun.MyInstance.SetDirectionGunImage(facingDirection);

            //bulletOrientation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            facingDirection = 2;
            FireGun.MyInstance.SetDirectionGunImage(facingDirection);

            //bulletOrientation = Quaternion.Euler(0, 0, 0);
        }
    }


    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 tempVector = new Vector3(h, v, 0);

        tempVector = tempVector * movementSpeed * Time.deltaTime;

        rb.MovePosition(rb.transform.position + tempVector * movementSpeed * Time.fixedDeltaTime);
        Animate(tempVector);

    }

    public void Animate(Vector3 Direction)
    {
        animator.SetFloat("x", Direction.x);
        animator.SetFloat("y", Direction.y);

    }

}
