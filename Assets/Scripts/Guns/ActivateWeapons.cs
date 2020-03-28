using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateWeapons : MonoBehaviour
{
    [SerializeField]
    private Transform gunPivotPoints;
    GameObject weapon, weaponInstance;
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

    public static ActivateWeapons myInstance { get; set; }
    public static ActivateWeapons MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<ActivateWeapons>();
            }
            return myInstance;
        }
    }

    private void Start()
    {
        weapon = GetComponent<GameObject>();
    }
    private void LateUpdate()
    {
        //Weapon Scope
        if (weaponInstance != null)
        {
            if (!WeaponController.MyInstance.IsUIOn)
            {
                if (WeaponController.MyInstance.ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
                {
                    if (Input.GetMouseButton(1))
                    {
                        //The scope is now active.
                        if (anim == null)
                        {
                            anim = weaponInstance.GetComponent<Animator>();
                            anim.enabled = true;
                        }
                        WeaponController.MyInstance.ThisIsTheActiveWeapon.NormalToScope = true;
                        anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveWeapon.NormalToScope);

                    }
                    //The scope is now inactive
                    else if (anim != null)
                    {
                        WeaponController.MyInstance.ThisIsTheActiveWeapon.NormalToScope = false;
                        anim.SetBool("scopeIn", WeaponController.MyInstance.ThisIsTheActiveWeapon.NormalToScope);
                    }
                }
                else
                {
                    if (Input.GetMouseButton(1))
                    {

                        if (anim == null)
                        {
                            anim = weaponInstance.GetComponent<Animator>();
                            anim.enabled = true;
                        }
                        WeaponController.MyInstance.ThisIsTheActiveWeapon.Block = true;
                        anim.SetBool("Block", WeaponController.MyInstance.ThisIsTheActiveWeapon.Block);
                        
                    }
                    else if (anim != null)
                    {
                        WeaponController.MyInstance.ThisIsTheActiveWeapon.Block = false;
                        anim.SetBool("Block", WeaponController.MyInstance.ThisIsTheActiveWeapon.Block);
                    }
                }

            }

        }


    }

    /// <summary>
    /// Handles the raycasts of weapons and any particle effects needed. Also updates the Ammo UI.
    /// </summary>
    public void FireGun()
    {
        muzzleFlash.Play();
        //While the scope is active.
        scale = WeaponController.MyInstance.ThisIsTheActiveWeapon.GunAccuracy;
        while (WeaponController.MyInstance.ThisIsTheActiveWeapon.NormalToScope)
        {
            scale /= 2;
            break;
        }
        //If the gun has spray, like a shotgun, then it will have a max bullet count in its dataobject.
        for (int i = 0; i < Random.Range(1, WeaponController.MyInstance.ThisIsTheActiveWeapon.MaxBulletCount); i++)
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
                    enemyController.TakeDamage(WeaponController.MyInstance.ThisIsTheActiveWeapon.Damage / hit.distance * 20);
                }
                GameObject impact = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);
            }

        }
           
        WeaponController.MyInstance.ThisIsTheActiveWeapon.CurrentAmountInClip -= 1;

        WeaponController.MyInstance.UpdateAmmoUI
                (WeaponController.MyInstance.ThisIsTheActiveWeapon.CurrentAmountInClip,
                WeaponController.MyInstance.AmmoGlobal.CurrentAmmoAmount);
    }
    public void HitWithMelee()
    {
        anim = weaponInstance.GetComponent<Animator>();
        if (!WeaponController.MyInstance.ThisIsTheActiveWeapon.Block)
        {
            anim.Play("Swipe");
        }
    }

    public void InstantiateWeapon(Weapon activeWeapon)
    {
        Debug.Log("Instantiating: "+ activeWeapon);
        if (!weaponInstance)
        {
            weaponInstance = Instantiate(activeWeapon.WeaponPrefab, gunPivotPoints);
            weaponInstance.GetComponent<SphereCollider>().enabled = false;

            if (activeWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
            {
                getFirePoint(activeWeapon);
            }
            else if(activeWeapon.WeaponType == Weapon.weaponTypeEnum.Melee)
            {
                weaponInstance.GetComponent<MeshCollider>().enabled = true;
                WeaponController.MyInstance.AmmoText.text = "";
            }
            
        }
        else //If you are manually changing the active weapon.
        {
            Destroy(weaponInstance);
            weaponInstance = Instantiate(activeWeapon.WeaponPrefab, gunPivotPoints);
            weaponInstance.GetComponent<SphereCollider>().enabled = false;
            WeaponController.MyInstance.turnOnWeaponUI();

            if (activeWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
            {
                getFirePoint(activeWeapon);
            }
            else if (activeWeapon.WeaponType == Weapon.weaponTypeEnum.Melee)
            {
                WeaponController.MyInstance.AmmoText.text = "";
                weaponInstance.GetComponent<MeshCollider>().enabled = true;

            }
        }
    }
    public void UninstantiateWeapon()
    {
        Destroy(weaponInstance);
    }
 
    //Gets the firepoint from uner the gun object.
    public void getFirePoint(Weapon activeGun)
    {
        muzzleFlash = weaponInstance.GetComponentInChildren<ParticleSystem>();
    }
}

