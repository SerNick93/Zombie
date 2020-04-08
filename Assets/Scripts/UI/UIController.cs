using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController myInstance { get; set; }
    public static UIController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<UIController>();
            }
            return myInstance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI interactionText;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void WeaponPickup(Weapon weapon)
    {
        interactionText.text = "Press E to pickup the " + weapon.WeaponName + ".";
    }
    public void ItemPickup(Item item)
    {
        interactionText.text = "Press E to Pickup the " + item.ItemName + ".";
    }
    public void AmmoPickup(string ammoName)
    {
        interactionText.text = "Press E to pickup the " + ammoName + ".";
    }
    public void DoorInteraction()
    {
        interactionText.text = "Press E to go through the " + "Door" + ".";
    }

    public void turnOffInteractions()
    {
        if (interactionText.text != null)
        {
            interactionText.text = "";
        }
    }

    public void TurnCursorOff()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TurnCursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
