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
    private void OnTriggerStay2D(Collider2D collision)
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
    private void OnTriggerExit2D(Collider2D collision)
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

                    if (ammo.AmmoObjectPrefab == WeaponUIController.MyInstance.ThisIsTheActiveGun.AmmoType)
                    {
                        Debug.Log(ammo.AmmoObjectPrefab);
                        Debug.Log(WeaponUIController.MyInstance.ThisIsTheActiveGun.AmmoType);
                        WeaponUIController.MyInstance.UpdateAmmoUI(WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip, ammo.CurrentAmmoAmount);
                        UIController.MyInstance.turnOffInteractions();

                    }
                    else
                     return;
                }
            }
        }
    }
}
