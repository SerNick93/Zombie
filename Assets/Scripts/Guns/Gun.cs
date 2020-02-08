using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun", order = 99)]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animation))]
public class Gun : ScriptableObject
{
    [SerializeField]
    private string gunName;
    [SerializeField]
    private Sprite gunImage;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int clipSize;
    [SerializeField]
    private GameObject gunPrefab;
    [SerializeField]
    private float gunAccuracy;
    [SerializeField]
    private AmmoObject ammoObject;
    [SerializeField]
    private int currentAmountInClip;
    [SerializeField]
    private float damage;
    [SerializeField]
    private bool normalToScope;
    [SerializeField]
    private int maxBulletCount;

    public string GunName { get => gunName; set => gunName = value; }
    public Sprite GunImage { get => gunImage; set => gunImage = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int ClipSize { get => clipSize; set => clipSize = value; }
    public GameObject GunPrefab { get => gunPrefab; set => gunPrefab = value; }
    public float GunAccuracy { get => gunAccuracy; set => gunAccuracy = value; }
    public AmmoObject AmmoType { get => ammoObject; set => ammoObject = value; }
    public int CurrentAmountInClip { get => currentAmountInClip; set => currentAmountInClip = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool NormalToScope { get => normalToScope; set => normalToScope = value; }
    public int MaxBulletCount { get => maxBulletCount; set => maxBulletCount = value; }

}
