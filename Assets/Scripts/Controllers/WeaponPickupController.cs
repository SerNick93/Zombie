using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickupController : MonoBehaviour
{
    [SerializeField]
    private Gun gunData;
    bool inCollider;
    void LateUpdate()
    {
        if (inCollider && Input.GetKeyDown(KeyCode.E))
        {
            PickupCode();
        }
    }
    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            inCollider = true;
            UIController.MyInstance.GunPickup(gunData);
        }
    }
    public void PickupCode()
    {
        //UI Element
        if (Input.GetKey(KeyCode.E))
        {
            //Will only run if you do not have any guns on you
            //If Places the first weapon in the correct slot in the inventory.
            if (WeaponController.MyInstance.ActiveWeapon.sprite == null &&
                WeaponController.MyInstance.GunInventoryImages[0].sprite == null &&
                WeaponController.MyInstance.GunInventoryImages[1].sprite == null)
            {
                if (gunData.SideArm == true)
                {
                    WeaponController.MyInstance.SetGunImageZero(gunData, 0);
                    exitInteraction();
                    return;
                }
                else if (gunData.SideArm == false)
                {
                    WeaponController.MyInstance.SetGunImageZero(gunData, 1);
                    exitInteraction();
                    return;
                }
            }
            else
            {
                //Will run if you have any gun on you.
                switch (gunData.SideArm)
                {
                    case true:
                        //If the weapon is a sidearm, and you do not already have one:
                        if (WeaponController.MyInstance.GunInventoryImages[0].sprite == null)
                        {
                            WeaponController.MyInstance.AddWeaponToInventory(gunData, 0);
                            exitInteraction();
                        }

                        //If you do already have one
                        else if (WeaponController.MyInstance.GunInventoryImages[0].sprite != null)
                        {
                            //And its the SAME one you are wanting to pick up
                            if (WeaponController.MyInstance.GunInventoryImages[0].sprite == gunData.GunImage)
                            {
                                WeaponController.MyInstance.HasWeapon(gunData);
                                exitInteraction();
                                return;
                            }

                            //If it is not the same one that you want to pick up
                            else
                            {
                                //if you are replacing the active weapon.
                                if (WeaponController.MyInstance.ActiveWeapon.sprite == WeaponController.MyInstance.GunInventoryImages[0].sprite)
                                {
                                    WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.GunInventoryImages[0].sprite.name, true);
                                    WeaponController.MyInstance.SetGunImageZero(gunData, 0);
                                    WeaponController.MyInstance.turnOnWeaponUI();
                                    exitInteraction();
                                    return;
                                }
                                else
                                {
                                    //If we are not replacing the active weapon.
                                    WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.GunInventoryImages[0].sprite.name, true);
                                    WeaponController.MyInstance.AddWeaponToInventory(gunData, 0);
                                    exitInteraction();
                                    return;
                                }

                            }
                        }
                        break;

                    case false:
                        //If the weapon is NOT a sidearm, and you do not already have a main weapon:
                        if (WeaponController.MyInstance.GunInventoryImages[1].sprite == null)
                        {
                            WeaponController.MyInstance.AddWeaponToInventory(gunData, 1);
                            exitInteraction();
                        }

                        //If you do already have one
                        else if (WeaponController.MyInstance.GunInventoryImages[1].sprite != null)
                        {
                            //And its the SAME one you are wanting to pick up
                            if (WeaponController.MyInstance.GunInventoryImages[1].sprite == gunData.GunImage)
                            {
                                WeaponController.MyInstance.HasWeapon(gunData);
                                exitInteraction();
                                return;
                            }

                            //If it is not the same one that you want to pick up
                            else
                            {
                                //if you are replacing the active weapon.
                                if (WeaponController.MyInstance.ActiveWeapon.sprite == WeaponController.MyInstance.GunInventoryImages[1].sprite)
                                {
                                    WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.GunInventoryImages[1].sprite.name, true);
                                    WeaponController.MyInstance.SetGunImageZero(gunData, 1);
                                    WeaponController.MyInstance.turnOnWeaponUI();
                                    exitInteraction();
                                    return;
                                }
                                else
                                {
                                    //If we are not replacing the active weapon.
                                    WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.GunInventoryImages[1].sprite.name, true);
                                    WeaponController.MyInstance.AddWeaponToInventory(gunData, 1);
                                    exitInteraction();
                                    return;
                                }

                            }
                        }
                        break;
                }
            }



            return;



        }
    }

    public void OnTriggerExit(Collider collision)
    {
        inCollider = false;
        UIController.MyInstance.turnOffInteractions();
    }
    public void exitInteraction()
    {
        UIController.MyInstance.turnOffInteractions();
        Destroy(gameObject);
    }
}
