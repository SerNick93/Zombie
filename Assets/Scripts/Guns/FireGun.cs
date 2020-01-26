using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGun : MonoBehaviour
{
    [SerializeField]
    private Transform gunPivotPoints;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletForce; //Speed of bullet
    Rigidbody rb;
    GameObject bullet;
    [SerializeField]
    GameObject gun, gunInstance;
    [SerializeField]
    Camera mainCam;
    Transform gunFirePoint;
    [SerializeField]
    private ParticleSystem muzzleFlash;
    public static FireGun myInstance { get; set; }
    public static FireGun MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<FireGun>();
            }
            return myInstance;
        }
    }

    private void Start()
    {
        gun = GetComponent<GameObject>();
    }

    /// <summary>
    /// Fire the weapon
    /// Instantiate bulley at the correct position, following the rotation of angle, which is the mouse cursor
    /// Check to see if the gun is out of ammo.
    /// </summary>
    public void FireWeapon()
    {
        if (WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip > 0)
        {
            RaycastHit hit;
            muzzleFlash.Play();
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
            {
                
            }

            WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip -=
               WeaponUIController.MyInstance.ThisIsTheActiveGun.FireRate;

            WeaponUIController.MyInstance.UpdateAmmoUI
                (WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip,
                WeaponUIController.MyInstance.AmmoGlobal.CurrentAmmoAmount);
        }

        else if (WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip <= 0)
        {
            WeaponUIController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip = 0;
            Debug.Log("You are out of Ammo!");

            //TODO: DISPLAY A "YOU ARE OUT OF AMMO" TOOLTIP. 
        }
    }
    public void InstantiateGun(Gun activeGun)
    {
        Debug.Log(activeGun);
        if (!gunInstance)
        {
            gunInstance = Instantiate(activeGun.GunPrefab, gunPivotPoints);
            gunInstance.GetComponent<SphereCollider>().enabled = false;
            getFirePoint();
        }
        else
        {
            Destroy(gunInstance);
            gunInstance = Instantiate(activeGun.GunPrefab, gunPivotPoints);
            gunInstance.GetComponent<SphereCollider>().enabled = false;
            WeaponUIController.MyInstance.turnOnWeaponUI();
            getFirePoint();


        }
    }
    public void getFirePoint()
    {
        foreach (Transform parentTransform in transform.GetComponentInChildren<Transform>())
        {
            foreach (Transform childTransform in parentTransform.GetComponentInChildren<Transform>())
            {
                foreach (Transform grandChildTransform in childTransform)
                {
                    foreach (Transform greatGrandChildTransform in grandChildTransform.GetComponentInChildren<Transform>())
                    {
                        Debug.Log(greatGrandChildTransform.name);
                        if (greatGrandChildTransform.tag == "FirePoint")
                        {
                            muzzleFlash = greatGrandChildTransform.GetComponent<ParticleSystem>();
                            muzzleFlash.transform.position = greatGrandChildTransform.position;
                            Debug.Log(gunFirePoint);

                        }
                    }
                }
            }
        }

    }

    //Set the correct image of the gun, depending on which way the player is facing.

}

