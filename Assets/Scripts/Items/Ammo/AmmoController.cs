using UnityEngine;
using System;
using System.Collections.Generic;

public class AmmoController : MonoBehaviour
{
    public static AmmoController myInstance { get; set; }
    public static AmmoController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<AmmoController>();
            }
            return myInstance;
        }
    }

   public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public List<AmmoType> AmmoTypes { get => ammoTypes; set => ammoTypes = value; }

    [SerializeField]
    private List<AmmoType> ammoTypes;



}