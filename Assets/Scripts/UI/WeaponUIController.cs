using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponUIController : MonoBehaviour
{
    public static WeaponUIController myInstance { get; set; }
    public static WeaponUIController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<WeaponUIController>();
            }
            return myInstance;
        }
    }

    public Image ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }
    public Gun ThisIsTheActiveGun { get => ThisIsTheActiveGun1; set => ThisIsTheActiveGun1 = value; }
    public AmmoType AmmoGlobal { get => ammoGlobal; set => ammoGlobal = value; }
    public Gun ThisIsTheActiveGun1 { get => thisIsTheActiveGun; set => thisIsTheActiveGun = value; }
    public Image[] GunInventoryImages { get => gunInventoryImages; set => gunInventoryImages = value; }

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
    private GameObject rightClickMenu;
    [SerializeField]
    private Transform canvasParent;

    private bool isUIOn = false;
    private bool isFiring = false;
    AmmoType ammoGlobal;
    Gun thisIsTheActiveGun;

    /// <summary>
    /// Set weapon active.
    /// </summary>
    /// <param name="ActiveGun"></param>
    public void SetWeaponActive(Gun ActiveGun)
    {
        Debug.Log("Setting Weapon Active");
        if (activeWeapon.sprite == null)
        {
            FireGun.MyInstance.SetDirectionGunImage(PlayerMovement.MyInstance.FacingDirection);
            GunInventoryImages[0].sprite = ActiveGun.GunImage;
            gunInventoryButtons[0].onClick.AddListener(() => SetWeaponActive(ActiveGun));
            activeWeaponButton.onClick.AddListener(() => turnOnWeaponUI());
        }
        activeWeapon.sprite = ActiveGun.GunImage;


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
                    GunInventoryImages[i].sprite = addedGun.GunImage;
                    gunInventoryButtons[i].onClick.AddListener(() => SetWeaponActive(addedGun));
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
        if (!isUIOn)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            isUIOn = true;
        }
        else if (isUIOn)
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            isUIOn = false;
        }
    }
    /// <summary>
    /// Ammo UI Updater
    /// </summary>
    /// <param name="currentAmountInClip"></param>
    /// <param name="currentAmmoAmount"></param>
    public void UpdateAmmoUI(int currentAmountInClip, int currentAmmoAmount)
    {
        Debug.Log("Updating Ammo Count");
        ammoText.text = currentAmountInClip + "/" + currentAmmoAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ThisIsTheActiveGun != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                turnOnWeaponUI();
            }
            if (Input.GetButtonDown("Fire1") && isUIOn == false)
            {
                FireGun.MyInstance.FireWeapon();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                isFiring = false;
            }
            if (Input.GetKeyDown(KeyCode.R) && isUIOn == false)
            {
                ReloadWeapon();
            }
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

    public void GunInventFull(Gun addedGun)
    {
        Debug.Log("You inventory is full.");
    }
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

    public void RightClickMenu()
    {
        Debug.Log("Right Click menu");
        Instantiate(rightClickMenu, parent, Quaternion.Euler(0, 0, 0), canvasParent);

    }
}






