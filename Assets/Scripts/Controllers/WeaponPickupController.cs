using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickupController : MonoBehaviour
{
    [SerializeField]
    private Gun gunData;

    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //UI Element
            UIController.MyInstance.GunPickup(gunData);

            if (Input.GetKey(KeyCode.E))
            {
                //Will only run when picking up a weapon for the first time.
                if (WeaponController.MyInstance.ActiveWeapon.sprite == null && WeaponController.MyInstance.GunInventoryImages[0].sprite == null)
                {
                    WeaponController.MyInstance.SetGunImageZero(gunData);
                    exitInteraction();
                    return;
                }
                else
                {
                    for (int i = 0; i < WeaponController.MyInstance.GunInventoryImages.Length; i++)
                    {
                        //You already have this weapon.
                        if (WeaponController.MyInstance.GunInventoryImages[i].sprite == gunData.GunImage)
                        {
                            WeaponController.MyInstance.HasWeapon(gunData);
                            exitInteraction();
                            return;

                        }
                        //You do not already have this weapon. 
                        else if (WeaponController.MyInstance.GunInventoryImages[i].sprite == null)
                        {
                            WeaponController.MyInstance.AddWeaponToInventory(gunData);
                            exitInteraction();
                            return;
                        }
                        
                        //You have no more room for weapons. 
                        else if (WeaponController.MyInstance.GunInventoryImages[i].sprite != null)
                        {
                            WeaponController.MyInstance.GunInventFull(gunData);
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
