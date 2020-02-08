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
                    if (anim == null)
                    {
                        anim = gunInstance.GetComponent<Animator>();
                        anim.enabled = true;
                    }
                    WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope = true;
                    anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope);
                    
                }
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

        for (int i = 0; i < Random.Range(1, WeaponController.MyInstance.ThisIsTheActiveGun.MaxBulletCount); i++)
        {
            Vector3 offset = Random.insideUnitCircle * WeaponController.MyInstance.ThisIsTheActiveGun.GunAccuracy;
            offset.z = z;
            offset = mainCam.transform.TransformDirection(offset.normalized);

            Ray r = new Ray(mainCam.transform.position, offset);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit))
            {
                enemyController = hit.transform.GetComponent<EnemyController>();

                if (enemyController)
                {
                    enemyController.TakeDamage(WeaponController.MyInstance.ThisIsTheActiveGun.Damage);
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
            scale = activeGun.GunAccuracy;
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
    public void ScopeActive()
    {
 

    }
    public void ScopeInactive()
    {
        Animator anim = gunInstance.GetComponent<Animator>();

        WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope = false;
        anim.enabled = false;
        anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveGun.NormalToScope);

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

