using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 99)]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animation))]
public class Weapon : ScriptableObject
{
    //[SerializeField]
    //private bool isGun;
    public enum weaponTypeEnum { Main, Side, Melee};

    [Header("Generic Variables")]
    [SerializeField]
    private weaponTypeEnum weaponType;
    [SerializeField]
    private string weaponName;
    [SerializeField]
    private Sprite weaponImage;
    [SerializeField]
    private GameObject weaponPrefab;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float rateOfAttack;
    //The rate that the player's stamina drains when they are using the aim function
    [SerializeField]
    private float staminaDrain;


    [Header("Gun Variables")]
    [SerializeField]
    [Tooltip("0 is complete 100% hitrate, anything above that is not.")]
    private float gunAccuracy;
    [SerializeField]
    private int clipSize;
    [SerializeField]
    private AmmoObject ammoObject;
    [SerializeField]
    private int currentAmountInClip;
    [SerializeField]
    [Tooltip("Is the weapon in scope mode?")]
    private bool normalToScope;
    [SerializeField]
    [Tooltip("The maximum amount of impacts from one bullet (This is for shotgun spray)")]
    private int maxBulletCount;
    [SerializeField]
    private ParticleSystem muzzleFlashParent;


    [Header("Melee Variables")]
    [SerializeField]
    [Tooltip("Is the weapon blocking?")]
    private bool block;
    [Tooltip("The amount of stamin drained per attack")]
    [SerializeField]
    private float meleeStaminDrain;

    //[Header("Granade Variables")]


    //GENERIC ACCESSORS
    public string WeaponName { get => weaponName; set => weaponName = value; }
    public Sprite WeaponImage { get => weaponImage; set => weaponImage = value; }
    public GameObject WeaponPrefab { get => weaponPrefab; set => weaponPrefab = value; }
    public float Damage { get => damage; set => damage = value; }
    public weaponTypeEnum WeaponType { get => weaponType; set => weaponType = value; }
    public float RateOfAttack { get => rateOfAttack; set => rateOfAttack = value; }
    public float StaminaDrain { get => staminaDrain; set => staminaDrain = value; }



    //GUN ACCESSORS
    public int ClipSize { get => clipSize; set => clipSize = value; }
    public float GunAccuracy { get => gunAccuracy; set => gunAccuracy = value; }
    public AmmoObject AmmoType { get => ammoObject; set => ammoObject = value; }
    public int CurrentAmountInClip { get => currentAmountInClip; set => currentAmountInClip = value; }
    public bool NormalToScope { get => normalToScope; set => normalToScope = value; }
    public int MaxBulletCount { get => maxBulletCount; set => maxBulletCount = value; }
    public ParticleSystem MuzzleFlashParent { get => muzzleFlashParent; set => muzzleFlashParent = value; }


    //MELEE ACCESSORS
    public bool Block { get => block; set => block = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float MeleeStaminDrain { get => meleeStaminDrain; set => meleeStaminDrain = value; }

    //GRANADE ACCESSORS




}

