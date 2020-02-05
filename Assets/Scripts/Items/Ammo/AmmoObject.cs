using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AmmoObject : MonoBehaviour
{
    private List<AmmoType> ammoList = new List<AmmoType>();

    [SerializeField]
    private AmmoObject ammoPrefab;
    [SerializeField]
    private int bundleAmount;

    /// <summary>
    /// Collision detecton for ammo. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            UIController.MyInstance.AmmoPickup(name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpAmmo(name);

            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        UIController.MyInstance.turnOffInteractions();
    }

    /// <summary>
    /// Adds the ammo to the correct slot in the serialzied data object (AmmoController & AmmoType)
    /// </summary>
    /// <param name="ammoName"></param>
    public void PickUpAmmo(string ammoName)
    {
        foreach (AmmoType ammo in AmmoController.MyInstance.AmmoTypes)
        {
            if (ammoName.ToLower() == ammo.AmmoName.ToLower())
            {
                if (ammo.CurrentAmmoAmount >= ammo.MaxAmmoAmount)
                {
                    Destroy(gameObject);
                    ammo.CurrentAmmoAmount += bundleAmount;
                    if (WeaponController.MyInstance.ThisIsTheActiveGun == null)
                    {
                        Debug.Log("You have no active weapon, but the ammo has been picked up anyway");
                        UIController.MyInstance.turnOffInteractions();
                        return;

                    }
                    if (ammo.AmmoObjectPrefab == WeaponController.MyInstance.ThisIsTheActiveGun.AmmoType)
                    {
                        Debug.Log(ammo.AmmoObjectPrefab);
                        Debug.Log(WeaponController.MyInstance.ThisIsTheActiveGun.AmmoType);
                        WeaponController.MyInstance.UpdateAmmoUI(WeaponController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip, ammo.CurrentAmmoAmount);
                        UIController.MyInstance.turnOffInteractions();

                    }

                }
            }
        }
    }
}
