using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoType
{
    [SerializeField]
    private string ammoName;

    [SerializeField]
    private AmmoObject ammoObjectPrefab;

    [SerializeField]
    private int currentAmmoAmount;

    [SerializeField]
    private int maxAmmoAmount;

    [SerializeField]
    private int damageBuff;

    public string AmmoName { get => ammoName; set => ammoName = value; }
    public AmmoObject AmmoObjectPrefab { get => ammoObjectPrefab; set => ammoObjectPrefab = value; }
    public int CurrentAmmoAmount { get => currentAmmoAmount; set => currentAmmoAmount = value; }
    public int MaxAmmoAmount { get => maxAmmoAmount; set => maxAmmoAmount = value; }
    public int DamageBuff { get => damageBuff; set => damageBuff = value; }
}
