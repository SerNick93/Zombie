using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Texture2D normalCrosshair, crosshairTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2 (1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(normalCrosshair, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
