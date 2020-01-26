using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Gun gunData;

    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            UIController.MyInstance.GunPickup(gunData);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (WeaponUIController.MyInstance.ActiveWeapon.sprite == null)
                {
                    WeaponUIController.MyInstance.SetWeaponActive(gunData);
                    exitInteraction();
                    return;
                }

                else
                {
                    for (int i = 0; i < WeaponUIController.MyInstance.GunInventoryImages.Length; i++)
                    {
                        if (WeaponUIController.MyInstance.GunInventoryImages[i].sprite == gunData.GunImage)
                        {
                            WeaponUIController.MyInstance.HasWeapon(gunData);
                            exitInteraction();
                            return;

                        }
                        else if (WeaponUIController.MyInstance.GunInventoryImages[i].sprite == null)
                        {
                            WeaponUIController.MyInstance.AddWeaponToInventory(gunData);
                            exitInteraction();
                            return;
                        }
                        
                        if (WeaponUIController.MyInstance.GunInventoryImages[i].sprite != null)
                        {
                            WeaponUIController.MyInstance.GunInventFull(gunData);
                        }
                    }
                }
            }
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        UIController.MyInstance.turnOffInteractions();
    }
    public void exitInteraction()
    {
        UIController.MyInstance.turnOffInteractions();
        Destroy(gameObject);
    }
}
