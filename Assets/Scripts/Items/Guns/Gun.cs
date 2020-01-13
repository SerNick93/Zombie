using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun", order = 99)]
public class Gun : ScriptableObject
{
    [SerializeField]
    private string gunName;
    [SerializeField]
    private Sprite gunImage;
    [SerializeField]
    private int fireRate;
    [SerializeField]
    private int clipSize;
    [SerializeField]
    private GameObject gunPrefab;
    [SerializeField]
    private int gunAccuracy;
    [SerializeField]
    private AmmoObject ammoObject;
    [SerializeField]
    private int currentAmountInClip;
    [SerializeField]
    private GameObject topDownImage;
    [SerializeField]
    private GameObject sideViewImage;

    public string GunName { get => gunName; set => gunName = value; }
    public Sprite GunImage { get => gunImage; set => gunImage = value; }
    public int FireRate { get => fireRate; set => fireRate = value; }
    public int ClipSize { get => clipSize; set => clipSize = value; }
    public GameObject GunPrefab { get => gunPrefab; set => gunPrefab = value; }
    public int GunAccuracy { get => gunAccuracy; set => gunAccuracy = value; }
    public AmmoObject AmmoType { get => ammoObject; set => ammoObject = value; }
    public int CurrentAmountInClip { get => currentAmountInClip; set => currentAmountInClip = value; }
    public GameObject TopDownImage { get => topDownImage; set => topDownImage = value; }
    public GameObject SideViewImage { get => sideViewImage; set => sideViewImage = value; }
}
