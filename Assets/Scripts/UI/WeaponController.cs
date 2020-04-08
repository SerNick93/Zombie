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
    public Weapon ThisIsTheActiveWeapon { get => thisIsTheActiveWeapon; set => thisIsTheActiveWeapon = value; }
    public AmmoType AmmoGlobal { get => ammoGlobal; set => ammoGlobal = value; }
    public Image[] WeaponInventoryImages { get => weaponInventoryImage; set => weaponInventoryImage = value; }
    public Button[] WeaponInventoryButtons { get => weaponInventoryButton; set => weaponInventoryButton = value; }
    public bool IsUIOn { get => isUIOn; set => isUIOn = value; }
    public TextMeshProUGUI AmmoText { get => ammoText; set => ammoText = value; }

    [SerializeField]
    private Image activeWeapon;
    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private Image[] weaponInventoryImage;
    [SerializeField]
    private CanvasGroup cg;
    [SerializeField]
    private Button[] weaponInventoryButton;
    [SerializeField]
    private Button activeWeaponButton;
    [SerializeField]
    private Transform canvasParent;
    private List<Weapon> weaponList = new List<Weapon>();
    
    private bool isUIOn = false;
    [SerializeField]
    private Transform gameWorld;

    float nextFire = 0.0f;

    AmmoType ammoGlobal;
    Weapon thisIsTheActiveWeapon;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            turnOnWeaponUI();
        }

        if (ThisIsTheActiveWeapon != null)
        {
            if (ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
            {
                if (Input.GetKeyDown(KeyCode.R) && IsUIOn == false)
                {
                    ReloadWeapon();
                }
            }
            if (Input.GetButton("Fire1") && IsUIOn == false && Time.time > nextFire)
            {
                AttackWithWeapon();
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
    /// <param name="FirstActiveWeapon"></param>
    public void SetGunImageZero(Weapon FirstActiveWeapon, int slotNumber)
    {
        switch (slotNumber)
        {
            case 0:
                WeaponInventoryImages[0].sprite = FirstActiveWeapon.WeaponImage;
                WeaponInventoryButtons[0].onClick.AddListener(() => SetWeaponActive(FirstActiveWeapon));
                break;
            case 1:
                WeaponInventoryImages[1].sprite = FirstActiveWeapon.WeaponImage;
                WeaponInventoryButtons[1].onClick.AddListener(() => SetWeaponActive(FirstActiveWeapon));
                break;
            case 2:
                WeaponInventoryImages[2].sprite = FirstActiveWeapon.WeaponImage;
                WeaponInventoryButtons[2].onClick.AddListener(() => SetWeaponActive(FirstActiveWeapon));

                break;


        }
        weaponList.Add(FirstActiveWeapon);
        SetWeaponActive(FirstActiveWeapon);
    }



    /// <summary>
    /// Set weapon active.
    /// </summary>
    /// <param name="ActiveWeapon"></param>

    public void SetWeaponActive(Weapon ActiveWeapon)
    {
        Debug.Log("Setting "+ ActiveWeapon.WeaponName + " Active");
        if (activeWeapon.sprite == null)
        {
            activeWeaponButton.onClick.AddListener(() => turnOnWeaponUI());
        }

        ActivateWeapons.MyInstance.InstantiateWeapon(ActiveWeapon);
        activeWeapon.sprite = ActiveWeapon.WeaponImage;
        activeWeapon.GetComponent<Image>().enabled = true;
        activeWeaponButton.GetComponent<Button>().enabled = true;
        ThisIsTheActiveWeapon = ActiveWeapon;
        //If the weapon is a gun.
        if (ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
        {
            foreach (AmmoType ammo in AmmoController.MyInstance.AmmoTypes)
            {
                if (ThisIsTheActiveWeapon.AmmoType == ammo.AmmoObjectPrefab)
                {
                    AmmoGlobal = ammo;
                    UpdateAmmoUI(ThisIsTheActiveWeapon.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount); 
                }
            }
        }
        
    }

    /// <summary>
    /// If there is already an active weapon, this will run. 
    /// </summary>
    /// <param name="addedWeapon"></param>
    public void AddWeaponToInventory(Weapon addedWeapon, int slotNumber)
    {
        switch (slotNumber)
        {
            case 0:
                weaponList.Add(addedWeapon);
                WeaponInventoryImages[0].sprite = addedWeapon.WeaponImage;
                WeaponInventoryButtons[0].onClick.AddListener(() => SetWeaponActive(addedWeapon));
                break;
            case 1:
                weaponList.Add(addedWeapon);
                WeaponInventoryImages[1].sprite = addedWeapon.WeaponImage;
                WeaponInventoryButtons[1].onClick.AddListener(() => SetWeaponActive(addedWeapon));
                break;
            case 2:
                weaponList.Add(addedWeapon);
                WeaponInventoryImages[2].sprite = addedWeapon.WeaponImage;
                WeaponInventoryButtons[2].onClick.AddListener(() => SetWeaponActive(addedWeapon));
                break;

        }

        Debug.Log("Adding " + addedWeapon.WeaponName +  " to the Inventory");
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
        if (ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
        {
            AmmoText.text = currentAmountInClip + "/" + currentAmmoAmount;
        }
    }

    //Shoots the gun.
    public void AttackWithWeapon()
    {
        if (ThisIsTheActiveWeapon.WeaponType == Weapon.weaponTypeEnum.Melee)
        {
            nextFire = Time.time + ThisIsTheActiveWeapon.RateOfAttack;
            ActivateWeapons.MyInstance.Attack();
            return;
        }
        else if (ThisIsTheActiveWeapon.CurrentAmountInClip > 0 && ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
        {

            nextFire = Time.time + ThisIsTheActiveWeapon.RateOfAttack;
            ActivateWeapons.MyInstance.Attack();

        }
        else if (ThisIsTheActiveWeapon.CurrentAmountInClip <= 0 && ThisIsTheActiveWeapon.WeaponType != Weapon.weaponTypeEnum.Melee)
        {
            ThisIsTheActiveWeapon.CurrentAmountInClip = 0;
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
        reloadAmount = ThisIsTheActiveWeapon.ClipSize - ThisIsTheActiveWeapon.CurrentAmountInClip;
        ThisIsTheActiveWeapon.CurrentAmountInClip += reloadAmount;
        AmmoGlobal.CurrentAmmoAmount -= reloadAmount;
        UpdateAmmoUI(ThisIsTheActiveWeapon.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount);
    }

    /// <summary>
    /// You have no space in your gun inventory.
    /// </summary>
    /// <param name="addedGun"></param>
    public void GunInventFull(Weapon addedGun)
    {
        Debug.Log("You inventory is full.");
    }

    /// <summary>
    /// You already have this weapon. 
    /// </summary>
    /// <param name="addedGun"></param>
    public void HasWeapon(Weapon addedGun)
    {
        foreach (AmmoType ammo in AmmoController.MyInstance.AmmoTypes)
        {
            if (addedGun.AmmoType == ammo.AmmoObjectPrefab)
            {
                int addonAmount = Random.Range(5, addedGun.ClipSize);

                ammo.CurrentAmmoAmount += addonAmount;
                if (addedGun.WeaponName == ThisIsTheActiveWeapon.WeaponName)
                {
                    UpdateAmmoUI(ThisIsTheActiveWeapon.CurrentAmountInClip, AmmoGlobal.CurrentAmmoAmount);
                }
                Debug.Log("Added: "+ addonAmount +  " ammo to: " + addedGun.WeaponName + " " + addedGun.AmmoType);
            }
        }
    }
    /// <summary>
    /// Drops the weapon on the floor below you. 
    /// </summary>
    public void DropWeapon(string name, bool weaponSwap)
    {
        //Take all of the invent spaces for weapons
        for (int i = 0; i < weaponInventoryImage.Length; i++)
        {
            //If there is a gun sprite in that space
            if (weaponInventoryImage[i].sprite != null)
            {
                //if the spaces's sprite name matches the button you clicked on
                if (weaponInventoryImage[i].sprite.name == name)
                {
                    //foreach gun in your current inventory
                    foreach (Weapon weapon in weaponList)
                    {
                        //does it match the sprite?
                        if (weapon.WeaponImage.name == weaponInventoryImage[i].sprite.name)
                        {
                            //is it the active weapon?

                            if (weaponSwap != true)
                            {
                                if (ThisIsTheActiveWeapon != null && name == ThisIsTheActiveWeapon.WeaponImage.name)
                                {
                                    UnequipWeapon();
                                }
                                weaponInventoryImage[i].sprite = null;
                                this.weaponInventoryButton[i].onClick.RemoveAllListeners();

                            }
                            //unassign it from the inventory
                            weapon.WeaponPrefab.GetComponent<Animator>().enabled = false;
                            Instantiate(weapon.WeaponPrefab, PlayerMovement.MyInstance.GroundCheck.transform.position + new Vector3 (0,0.01f,0), Quaternion.Euler(90,90,0), gameWorld);
                            weaponList.Remove(weapon);
                            Debug.Log("Dropping " + weapon.WeaponName);
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
        ActivateWeapons.MyInstance.UninstantiateWeapon();
        activeWeapon.sprite = null;
        activeWeapon.GetComponent<Image>().enabled = false;
        activeWeaponButton.GetComponent<Button>().enabled = false;

        AmmoText.text = null;
        ThisIsTheActiveWeapon = null;
    }
}






