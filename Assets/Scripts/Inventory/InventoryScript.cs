﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript myInstance { get; set; }
    public static InventoryScript MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<InventoryScript>();
            }
            return myInstance;
        }
    }

    public Gun[] Guns { get => guns; set => guns = value; }
    public AmmoObject[] Ammo { get => ammo; set => ammo = value; }

    [SerializeField]
    Gun[] guns;
    [SerializeField]
    AmmoObject[] ammo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}