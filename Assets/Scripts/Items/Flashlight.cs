using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private GameObject flashLight;

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLight.activeInHierarchy == true)
            {
                flashLight.SetActive(false);
            }
            else
                flashLight.SetActive(true);
            
        }
    }
}
