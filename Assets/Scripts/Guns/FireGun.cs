using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGun : MonoBehaviour
{
    [SerializeField]
    private Transform gunPivotPoints;
    GameObject gun, gunInstance;
    [SerializeField]
    Camera mainCam;
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject impactFlash;

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
    /// Handles the raycasts of weapons and any particle effects needed. Also updates the Ammo UI.
    /// </summary>
    public void FireWeapon()
    {
            RaycastHit hit;
            muzzleFlash.Play();
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
            {
                EnemyController enemyController = hit.transform.GetComponent<EnemyController>();
                if (enemyController)
                {
                    enemyController.TakeDamage(WeaponController.MyInstance.ThisIsTheActiveGun.Damage);
                }
                
                GameObject impact = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);

            }

        WeaponController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip -= 1;

        WeaponController.MyInstance.UpdateAmmoUI
                (WeaponController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip,
                WeaponController.MyInstance.AmmoGlobal.CurrentAmmoAmount);
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
            WeaponController.MyInstance.turnOnWeaponUI();
            getFirePoint();


        }
    }
    public void UninstantiateGun()
    {
        Destroy(gunInstance);
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
                        if (greatGrandChildTransform.tag == "FirePoint")
                        {
                            muzzleFlash = greatGrandChildTransform.GetComponent<ParticleSystem>();
                            muzzleFlash.transform.position = greatGrandChildTransform.position;
                        }
                    }
                }
            }
        }

    }
}

