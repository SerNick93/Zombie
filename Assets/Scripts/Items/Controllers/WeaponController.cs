using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Gun gunData;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIController.MyInstance.GunPickup(gunData);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (WeaponUIController.MyInstance.ActiveWeapon.sprite == null)
                {

                    WeaponUIController.MyInstance.SetWeaponActive(gunData);
                    UIController.MyInstance.turnOffInteractions();
                    Destroy(gameObject);
                }
                else
                {
                    WeaponUIController.MyInstance.AddWeaponToInventory(gunData);
                    UIController.MyInstance.turnOffInteractions();

                    Destroy(gameObject);
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        UIController.MyInstance.turnOffInteractions();
    }

}
