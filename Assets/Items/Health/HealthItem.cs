using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HealthItem", fileName = "HealthItem")]
public class HealthItem : Item
{
    [SerializeField]
    private float healAmount;

    public override void AddToActions()
    {
        ItemActionMethods.Clear();
        ItemActionMethods.Add(Use);
        base.AddToActions();


    }

    public override void Use()
    {
        base.Use();

        Debug.Log(ItemName + " is being Used");

        PlayerStats.MyInstance.CurrentHealth += healAmount;
        
    }
    public override void Drop()
    {
        base.Drop();
    }
    public override void Destroy()
    {
        base.Destroy();
    }


}
