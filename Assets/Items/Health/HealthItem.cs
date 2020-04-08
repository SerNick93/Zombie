using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HealthItem", fileName = "HealthItem")]
public class HealthItem : Item
{
    [SerializeField]
    private float healAmount;





    public override void Use()
    {
        base.Use();
        Debug.Log(ItemName + " is being Used");

        PlayerStats.MyInstance.CurrentHealth += healAmount;

    }
}
