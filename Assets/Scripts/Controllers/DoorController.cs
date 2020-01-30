using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    

    private void OnTriggerStay(Collider collision)
    {
        UIController.MyInstance.DoorInteraction();
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            //Do you want to go into the next room?
            //Has the player pressed the interaction button?
            gameObject.SetActive(false);
            UIController.MyInstance.turnOffInteractions();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        UIController.MyInstance.turnOffInteractions();
        gameObject.SetActive(true);

    }
}
