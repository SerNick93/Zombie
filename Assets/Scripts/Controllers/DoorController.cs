using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            //Do you want to go into the next room?
            //Has the player pressed the interaction button?
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
