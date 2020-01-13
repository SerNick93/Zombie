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

    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private PlayerMovement playerController;



    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerController.transform.position + offset;
    }
}
