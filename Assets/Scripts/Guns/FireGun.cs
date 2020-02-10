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
    Animator anim;
    float scale = .5f;
    float z = 10f;
    EnemyController enemyController;
    RaycastHit hit;

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
    private void FixedUpdate()
    {
        if (gunInstance != null)
        {
            if (!WeaponController.MyInstance.IsUIOn)
            {
                if (Input.GetMouseButton(1))
                {
                    //The scope is now active.
                    if (anim == null)
                    {
                        anim = gunInstance.GetComponent<Animator>();
                        anim.enabled = true;
                    }
                    WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope = true;
                    anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope);
                    
                }
                //The scope is now inactive
                else if (anim != null)
                {
                    
                    WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope = false;
                    anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope);
                    
                }

            }

        }


    }

    /// <summary>
    /// Handles the raycasts of weapons and any particle effects needed. Also updates the Ammo UI.
    /// </summary>
    public void FireWeapon()
    {
        muzzleFlash.Play();
        //While the scope is active.
        scale = WeaponController.MyInstance.ThisIsTheActiveGun.GunAccuracy;
        while (WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope)
        {
            scale /= 2;
            break;
        }
        //If the gun has spray, like a shotgun, then it will have a max bullet count in its dataobject.
        for (int i = 0; i < Random.Range(1, WeaponController.MyInstance.ThisIsTheActiveGun.MaxBulletCount); i++)
        {
            //Gun accuracy * the distance to the raycast hit, halved.
            Vector3 offset = Random.insideUnitCircle * scale * (hit.distance/50);
            
            offset.z = z;
            offset = mainCam.transform.TransformDirection(offset.normalized);
            
            Ray r = new Ray(mainCam.transform.position, offset);
            if (Physics.Raycast(r, out hit))
            {
                enemyController = hit.transform.GetComponent<EnemyController>();
                Debug.DrawLine(mainCam.transform.position, hit.point);
                if (enemyController)
                {
                    enemyController.TakeDamage(WeaponController.MyInstance.ThisIsTheActiveGun.Damage / hit.distance * 20);
                }
                GameObject impact = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);
            }

        }
           
        WeaponController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip -= 1;

        WeaponController.MyInstance.UpdateAmmoUI
                (WeaponController.MyInstance.ThisIsTheActiveGun.CurrentAmountInClip,
                WeaponController.MyInstance.AmmoGlobal.CurrentAmmoAmount);
    }

    public void InstantiateGun(Gun activeGun)
    {
        Debug.Log("Instantiating: "+ activeGun);
        if (!gunInstance)
        {
            gunInstance = Instantiate(activeGun.GunPrefab, gunPivotPoints);
            gunInstance.GetComponent<SphereCollider>().enabled = false;
            getFirePoint();
        }
        else //If you are manually changing the active weapon.
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
 
    //Gets the firepoint from uner the gun object.
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

