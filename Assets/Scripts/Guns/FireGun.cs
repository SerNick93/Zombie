using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGun : MonoBehaviour
{
    [SerializeField]
    private Transform[] gunPivotPoints;
    private SpriteRenderer topDownGunImage;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletForce; //Speed of bullet
    Rigidbody2D rb;
    GameObject bullet;
    float angle;
    private Vector3 bulletDirection;
    Quaternion facingRotation;
    [SerializeField]
    GameObject gun;
    Transform gunFirePoint;
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
        Physics2D.IgnoreCollision(bulletPrefab.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
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
            GameObject bullet = Instantiate(bulletPrefab, gunFirePoint.position, 
            gunPivotPoints[PlayerMovement.MyInstance.FacingDirection].rotation = Quaternion.Euler(0,0,angle + 90 + Random.Range(-5f, 5f)));


            //Addforce in the correct direction.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(gunPivotPoints[PlayerMovement.MyInstance.FacingDirection].up * -bulletForce, ForceMode2D.Impulse);
            

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


    void FixedUpdate()
    {
                #region
        Vector3 mouseOnScreen = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseOnScreen.y = mouseOnScreen.y - gunPivotPoints[PlayerMovement.MyInstance.FacingDirection].position.y;
        mouseOnScreen.x = mouseOnScreen.x - gunPivotPoints[PlayerMovement.MyInstance.FacingDirection].position.x;

        angle = Mathf.Atan2(mouseOnScreen.y, mouseOnScreen.x) * Mathf.Rad2Deg;

        //Clamp the player firing within range of where the player is looking.
        //E.g. Down: Fires within -135deg and -45deg.
        //If the mouse strays out of those bounds, then it will snap to the outisde bound it just left.

        //DOWN
        if (PlayerMovement.MyInstance.FacingDirection == 0)
        {
            if (angle < -135)
            {
                angle = -135;
            }
            else if (angle > -45)
            {
                angle = -45;
            }
        }


        //LEFT
        if (PlayerMovement.MyInstance.FacingDirection == 1)
        {

            if (angle < 135 && angle > -135)
            {
                angle = 135;
                return;

                if (angle > -135)
                {
                    angle = -135;
                }
            }
        }
    //UP
        if (PlayerMovement.MyInstance.FacingDirection == 2)
        {

            if (angle > 135)
            {
                angle = 135;
            }
            else if (angle < 45)
            {
                angle = 45;
            }
        }
        //RIGHT
        if (PlayerMovement.MyInstance.FacingDirection == 3)
        {
            if (angle < -45)
            {
                angle = -45;
            }
            else if (angle > 45)
            {
                angle = 45;
            }
        }
        #endregion     

        gunPivotPoints[PlayerMovement.MyInstance.FacingDirection].rotation = Quaternion.Euler(0, 0, angle + 90);
    }
    //Set the correct image of the gun, depending on which way the player is facing.
    public void SetDirectionGunImage(int firePoint)
    {
        //Debug.Log(firePoint);
        if (WeaponUIController.MyInstance.ThisIsTheActiveGun != null)
        {
            if (gun != null)
            {
                Destroy(gun.gameObject);
                //Debug.Log("Destroying Gun");
            }

            
            if (firePoint == 1 || firePoint == 3)
            {
                if (firePoint == 1)
                {
                    gun = Instantiate(WeaponUIController.MyInstance.ThisIsTheActiveGun.SideViewImage, gunPivotPoints[firePoint]);

                    gun.GetComponent<SpriteRenderer>().flipY = true;

                }
                else 
                    gun = Instantiate(WeaponUIController.MyInstance.ThisIsTheActiveGun.SideViewImage, gunPivotPoints[firePoint]);
            }
            else
            gun = Instantiate(WeaponUIController.MyInstance.ThisIsTheActiveGun.TopDownImage, gunPivotPoints[firePoint]);
            //Debug.Log("Reinstantiating gun in: " + " " + firePoint);

            //Search for the correct firepoint
            foreach (Transform childTransforms in transform.GetComponentsInChildren<Transform>())
            {
                foreach (Transform grandChildTransforms in childTransforms.GetComponentsInChildren<Transform>())
                {
                    if (grandChildTransforms.name == "FP_" + firePoint)
                    {
                        foreach (Transform greatGrandChildTransforms in grandChildTransforms.GetComponentsInChildren<Transform>())
                        {
                            //Thyis needs to get the firepoint from inside of the top down gun transform.
                            if (greatGrandChildTransforms.tag == "FirePoint")
                            {
                                gunFirePoint = greatGrandChildTransforms.GetComponent<Transform>();
                                gunFirePoint.transform.position = greatGrandChildTransforms.position;

                                if (firePoint == 2 || firePoint == 1)
                                {
                                    gun.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                }
                                else
                                {
                                    gun.GetComponent<SpriteRenderer>().sortingOrder = 3;
                                }
                                if (firePoint == 3 || firePoint == 1)
                                {
                                    gun.GetComponent<SpriteRenderer>().sprite = WeaponUIController.MyInstance.ThisIsTheActiveGun.SideViewImage.GetComponent<SpriteRenderer>().sprite;
                                   
                                }
                                else
                                {
                                    gun.GetComponent<SpriteRenderer>().sprite = WeaponUIController.MyInstance.ThisIsTheActiveGun.TopDownImage.GetComponent<SpriteRenderer>().sprite;

                                }
                            }
                        }

                    }
                }
            }


        }
    }
}

