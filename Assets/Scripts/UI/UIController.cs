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

    public void GunPickup(Gun gun)
    {
        interactionText.text = "Press E to pickup the " + gun.GunName + ".";
    }
    public void AmmoPickup(string ammoName)
    {
        interactionText.text = "Press E to pickup the " + ammoName + ".";
    }
    public void DoorInteraction()
    {
        interactionText.text = "Press E to continue " + "Door" + ".";
    }


    public void turnOffInteractions()
    {
        if (interactionText.text != null)
        {
            interactionText.text = "";
        }
    }
}
