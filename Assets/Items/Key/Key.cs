using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Key", fileName = "Key")]
public class Key : Item
{
    public override void Use()
    {
        base.Use();
        Debug.Log(ItemName + " is being Used");

    }
}
