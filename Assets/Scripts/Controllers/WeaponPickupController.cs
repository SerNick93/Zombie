using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickupController : MonoBehaviour
{
    [SerializeField]
    private Weapon weaponData;
    bool inCollider;
    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            inCollider = true;
            UIController.MyInstance.WeaponPickup(weaponData);
        }
    }
    void LateUpdate()
    {
        if (inCollider && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(weaponData.WeaponType);
            PickupCode();
        }
    }

    public void PickupCode()
    {
        //Will only run if you do not have any guns on you
        //If Places the first weapon in the correct slot in the inventory.
        if (WeaponController.MyInstance.ActiveWeapon.sprite == null &&
            WeaponController.MyInstance.WeaponInventoryImages[0].sprite == null &&
            WeaponController.MyInstance.WeaponInventoryImages[1].sprite == null &&
            WeaponController.MyInstance.WeaponInventoryImages[2].sprite == null) 
        {
            switch (weaponData.WeaponType)
            {
                case Weapon.weaponTypeEnum.Main:
                    WeaponController.MyInstance.SetGunImageZero(weaponData, 1);
                    exitInteraction();
                    break;
                case Weapon.weaponTypeEnum.Side:
                    WeaponController.MyInstance.SetGunImageZero(weaponData, 0);
                    exitInteraction();
                    break;
                case Weapon.weaponTypeEnum.Melee:
                    WeaponController.MyInstance.SetGunImageZero(weaponData, 2);
                    exitInteraction();
                    break;
                    default:
                    break;
            }
        }
        else
        {
            //Will run if you have any gun on you.
            switch (weaponData.WeaponType)
            {//If IS Main
                case Weapon.weaponTypeEnum.Main:
                    //You do not already have a mainhand weapon.
                    if (WeaponController.MyInstance.WeaponInventoryImages[1].sprite == null)
                    {
                        WeaponController.MyInstance.AddWeaponToInventory(weaponData, 1);
                        exitInteraction();
                    }
                    //You do already have a mainhand weapon.
                    else if (WeaponController.MyInstance.WeaponInventoryImages[1].sprite != null)
                    {
                        //And its the SAME one you are wanting to pick up
                        if (WeaponController.MyInstance.WeaponInventoryImages[1].sprite == weaponData.WeaponImage)
                        {
                            WeaponController.MyInstance.HasWeapon(weaponData);
                            exitInteraction();
                            return;
                        }

                        //If you have a mainhand weapon and the one you are picking up is different...
                        else
                        {
                            //if you are replacing the active weapon.
                            if (WeaponController.MyInstance.ActiveWeapon.sprite == WeaponController.MyInstance.WeaponInventoryImages[1].sprite)
                            {
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[1].sprite.name, true);
                                WeaponController.MyInstance.SetGunImageZero(weaponData, 1);
                                WeaponController.MyInstance.turnOnWeaponUI();
                                exitInteraction();
                                return;
                            }
                            else
                            {
                                //If we are not replacing the active weapon.
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[1].sprite.name, true);
                                WeaponController.MyInstance.AddWeaponToInventory(weaponData, 1);
                                exitInteraction();
                                return;
                            }

                        }
                    }
                    break;
                //if IS Side
                case Weapon.weaponTypeEnum.Side:
                    //If the weapon is a sidearm, and you do not already have one:
                    if (WeaponController.MyInstance.WeaponInventoryImages[0].sprite == null)
                    {
                        WeaponController.MyInstance.AddWeaponToInventory(weaponData, 0);
                        exitInteraction();
                    }

                    //If you do already have one
                    else if (WeaponController.MyInstance.WeaponInventoryImages[0].sprite != null)
                    {
                        //And its the SAME one you are wanting to pick up
                        if (WeaponController.MyInstance.WeaponInventoryImages[0].sprite == weaponData.WeaponImage)
                        {
                            WeaponController.MyInstance.HasWeapon(weaponData);
                            exitInteraction();
                            return;
                        }

                        //If it is not the same one that you want to pick up
                        else
                        {
                            //if you are replacing the active weapon.
                            if (WeaponController.MyInstance.ActiveWeapon.sprite == WeaponController.MyInstance.WeaponInventoryImages[0].sprite)
                            {
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[0].sprite.name, true);
                                WeaponController.MyInstance.SetGunImageZero(weaponData, 0);
                                WeaponController.MyInstance.turnOnWeaponUI();
                                exitInteraction();
                                return;
                            }
                            else
                            {
                                //If we are not replacing the active weapon.
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[0].sprite.name, true);
                                WeaponController.MyInstance.AddWeaponToInventory(weaponData, 0);
                                exitInteraction();
                                return;
                            }

                        }
                    }
                    break;
                //if it is meleee
                case Weapon.weaponTypeEnum.Melee:
                    if (WeaponController.MyInstance.WeaponInventoryImages[2].sprite == null)
                    {
                        WeaponController.MyInstance.AddWeaponToInventory(weaponData, 2);
                        exitInteraction();
                    }

                    //If you do already have one
                    else if (WeaponController.MyInstance.WeaponInventoryImages[2].sprite != null)
                    {
                        //And its the SAME one you are wanting to pick up
                        if (WeaponController.MyInstance.WeaponInventoryImages[2].sprite == weaponData.WeaponImage)
                        {
                            Debug.Log("You already have this weapon!");
                            exitInteraction();
                            return;
                        }

                        //If it is not the same one that you want to pick up
                        else
                        {
                            //if you are replacing the active weapon.
                            if (WeaponController.MyInstance.ActiveWeapon.sprite == WeaponController.MyInstance.WeaponInventoryImages[2].sprite)
                            {
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[0].sprite.name, true);
                                WeaponController.MyInstance.SetGunImageZero(weaponData, 2);
                                WeaponController.MyInstance.turnOnWeaponUI();
                                exitInteraction();
                                return;
                            }
                            else
                            {
                                //If we are not replacing the active weapon.
                                WeaponController.MyInstance.DropWeapon(WeaponController.MyInstance.WeaponInventoryImages[2].sprite.name, true);
                                WeaponController.MyInstance.AddWeaponToInventory(weaponData, 2);
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
