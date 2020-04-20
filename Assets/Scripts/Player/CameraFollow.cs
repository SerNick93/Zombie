using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow myInstance { get; set; }
    public static CameraFollow MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<CameraFollow>();
            }

            return myInstance;
        }
    }

    public float MouseSensitivity { get => mouseSensitivity; set => mouseSensitivity = value; }

    [SerializeField]
    private Transform Player, playerHead;

    [SerializeField]
    private float mouseSensitivity;
    private float xRotation = 0f;
    private float yRotation = 0;
    Quaternion rotation;
    private void Start()
    {
        rotation = transform.rotation;

    }

    private void LateUpdate()
    {
        if (!WeaponController.MyInstance.IsUIOn)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            Player.Rotate(Vector3.up * mouseX);
            transform.position = new Vector3(playerHead.position.x, playerHead.position.y, playerHead.position.z);


            if (transform.rotation.x != Player.rotation.x)
            {
                Player.rotation = Quaternion.Euler(0f, yRotation, 0f);
            }
            //implement headbob for the player's head - less than what the animation offers.
        }

    }


}
