using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public static WeaponController myInstance { get; set; }
    public static WeaponController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<WeaponController>();
            }
            return myInstance;
        }
    }

    public Image ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }
    public Gun ThisIsTheActiveGun { get => ThisIsTheActiveGun1; set => ThisIsTheActiveGun1 = value; }
    public AmmoType AmmoGlobal { get => ammoGlobal; set => ammoGlobal = value; }
    public Gun ThisIsTheActiveGun1 { get => thisIsTheActiveGun; set => thisIsTheActiveGun = value; }
    public Image[] GunInventoryImages { get => gunInventoryImages; set => gunInventoryImages = value; }
    public Button[] GunInventoryButtons { get => gunInventoryButtons; set => gunInventoryButtons = value; }
    public bool IsUIOn { get => isUIOn; set => isUIOn = value; }

    [SerializeField]
    private Image activeWeapon;
    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private Image[] gunInventoryImages;
    [SerializeField]
    private CanvasGroup cg;
    [SerializeField]
    private Button[] gunInventoryButtons;
    [SerializeField]
    private Button activeWeaponButton;
    [SerializeField]
    private Transform canvasParent;
    private List<Gun> gunList = new List<Gun>();
    private bool isUIOn = false;
    [SerializeField]
    private Transform gameWorld;

    float nextFire = 0.0f;

    AmmoType ammoGlobal;
    Gun thisIsTheActiveGun;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            turnOnWeaponUI();
            //if (Time.timeScale == 1)
            //{
            //    Time.timeScale = 0;
            //}
            //else if (Time.timeScale == 0)
            //{
            //    Time.timeScale = 1; 
            //}
        }

        if (ThisIsTheActiveGun != null)
        {
            if (Input.GetButton("Fire1") && IsUIOn == false && Time.time > nextFire)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R) && IsUIOn == false)
            {
                ReloadWeapon();
            }

            if (Input.GetKeyDown(KeyCode.M) && IsUIOn == false)
            {
                UnequipWeapon();
            }
        }
    }

    /// <summary>
    /// For use when dropping and unequiping weapons.
    /// This will only run when the first gun os picked up. 
    /// </summary>
    /// <param name="FirstActiveGun"></param>
    public void SetGunImageZero(Gun FirstActiveGun)
    {
        GunInventoryImages[0].sprite = FirstActiveGun.GunImage;
        GunInventoryButtons[0].onClick.AddListener(() => SetWeaponActive(FirstActiveGun));
        gunList.Add(FirstActiveGun);
        SetWeaponActive(FirstActiveGun);
    }
    /// <summary>
    /// Set weapon active.
    /// </summary>
    /// <param name="ActiveGun"></param>

    public void SetWeaponActive(Gun ActiveGun)
    {
        Debug.Log("Setting Weapon Active");
        if (activeWeapon.sprite == null)
        {
            activeWeaponButton.onClick.AddListener(() => turnOnWeaponUI());
        }

        FireGun.MyInstance.InstantiateGun(ActiveGun);
        activeWeapon.sprite = ActiveGun.GunImage;
        activeWeapon.GetComponent<Image>().enabled = true;
        activeWeaponButton.GetComponent<Button>().enabled = true;
        

        foreach (AmmoType ammo in AmmoController.MyInstance.AmmoTypes)
        {
            if (ActiveGun.AmmoType == ammo.AmmoObjectPrefab)
            {
                AmmoGlobal = ammo;
                UpdateAmmoUI(ActiveGun.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount);
                ThisIsTheActiveGun = ActiveGun;
            }
        }

    }
    /// <summary>
    /// If there is already an active weapon, this will run. 
    /// </summary>
    /// <param name="addedGun"></param>
    public void AddWeaponToInventory(Gun addedGun)
    {
        Debug.Log("Adding Weapon to Inventory");
        for (int i = 0; i < GunInventoryImages.Length; i++)
        {
            if (GunInventoryImages[i].sprite == null)
            {
                if (GunInventoryImages[i].sprite != addedGun.GunImage)
                {
                    gunList.Add(addedGun);
                    GunInventoryImages[i].sprite = addedGun.GunImage;
                    GunInventoryButtons[i].onClick.AddListener(() => SetWeaponActive(addedGun));
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Turns on the gun inventory interface. 
    /// </summary>
    public void turnOnWeaponUI()
    {
        if (!IsUIOn)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            IsUIOn = true;
            UIController.MyInstance.TurnCursorOn();
        }
        else if (IsUIOn)
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            IsUIOn = false;
            UIController.MyInstance.TurnCursorOff();


        }
    }

    /// <summary>
    /// Ammo UI Updater
    /// </summary>
    /// <param name="currentAmountInClip"></param>
    /// <param name="currentAmmoAmount"></param>
    public void UpdateAmmoUI(int currentAmountInClip, int currentAmmoAmount)
    {
        ammoText.text = currentAmountInClip + "/" + currentAmmoAmount;
    }

    //Shoots the gun.
    public void Shoot()
    {
        if (ThisIsTheActiveGun.CurrentAmountInClip > 0)
        {
            nextFire = Time.time + ThisIsTheActiveGun.FireRate;
            FireGun.MyInstance.FireWeapon();
        }
        else if (ThisIsTheActiveGun.CurrentAmountInClip <= 0)
        {
            ThisIsTheActiveGun.CurrentAmountInClip = 0;
            Debug.Log("You are out of Ammo!");

            //TODO: DISPLAY A "YOU ARE OUT OF AMMO" TOOLTIP. 
        }

    }
    /// <summary>
    /// reloads the currently active weapon.
    /// </summary>
    public void ReloadWeapon()
    {
        Debug.Log("Reloading Weapon");
        int reloadAmount = 0;
        reloadAmount = ThisIsTheActiveGun.ClipSize - ThisIsTheActiveGun.CurrentAmountInClip;
        ThisIsTheActiveGun.CurrentAmountInClip += reloadAmount;
        AmmoGlobal.CurrentAmmoAmount -= reloadAmount;
        UpdateAmmoUI(ThisIsTheActiveGun.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount);
    }

    /// <summary>
    /// You have no space in your gun inventory.
    /// </summary>
    /// <param name="addedGun"></param>
    public void GunInventFull(Gun addedGun)
    {
        Debug.Log("You inventory is full.");
    }

    /// <summary>
    /// You already have this weapon. 
    /// </summary>
    /// <param name="addedGun"></param>
    public void HasWeapon(Gun addedGun)
    {
        foreach (AmmoType ammo in AmmoController.MyInstance.AmmoTypes)
        {
            if (addedGun.AmmoType == ammo.AmmoObjectPrefab)
            {
                int addonAmount = Random.Range(5, addedGun.ClipSize);

                ammo.CurrentAmmoAmount += addonAmount;
                if (addedGun.GunName == ThisIsTheActiveGun.GunName)
                {
                    UpdateAmmoUI(ThisIsTheActiveGun.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount);
                }
                Debug.Log("Added: "+ addonAmount +  " ammo to: " + addedGun.GunName + " " + addedGun.AmmoType);
            }
        }
    }
    /// <summary>
    /// Drops the weapon on the floor below you. 
    /// </summary>
    public void DropWeapon(string name)
    {
        //Take all of the invent spaces for weapons
        for (int i = 0; i < gunInventoryImages.Length; i++)
        {
            Debug.Log(i);
            //If there is a gun sprite in that space
            if (gunInventoryImages[i].sprite != null)
            {
                //if the spaces's sprite name matches a gun in the inventory
                if (gunInventoryImages[i].sprite.name == name)
                {
                    foreach (Gun gun in gunList)
                    {
                        if (gun.GunImage.name == gunInventoryImages[i].sprite.name)
                        {
                            if (ThisIsTheActiveGun != null && name == ThisIsTheActiveGun.GunImage.name)
                            {
                                UnequipWeapon();
                            }
                            gunInventoryImages[i].sprite = null;
                            gunInventoryButtons[i].onClick.RemoveAllListeners();
                            Instantiate(gun.GunPrefab, PlayerMovement.MyInstance.GroundCheck.transform.position, Quaternion.Euler(90, 0, 0), gameWorld);
                            gunList.Remove(gun);
                            Debug.Log("Dropping: " + gun.GunName);
                            return;
                        }
                    }
                }
            }   
        }       
    }
    /// <summary>
    /// Unequips the weapon, essentially holstering it. 
    /// </summary>
    public void UnequipWeapon()
    {
        FireGun.MyInstance.UninstantiateGun();
        activeWeapon.sprite = null;
        activeWeapon.GetComponent<Image>().enabled = false;
        activeWeaponButton.GetComponent<Button>().enabled = false;

        ammoText.text = null;
        ThisIsTheActiveGun = null;
    }

}






