using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    Item thisItem;
    bool inCollider = false;

    public void AddItemToInventory()
    {
        //DEBUG::
        InventoryController.MyInstance.AddItem(thisItem);
    }
    private void OnTriggerStay(Collider other)
    {
        UIController.MyInstance.ItemPickup(thisItem);
        inCollider = true;
    }
    private void OnTriggerExit(Collider other)
    {
        UIController.MyInstance.turnOffInteractions();
        inCollider = false;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && inCollider == true)
        {
           bool pickupSuccessful = InventoryController.MyInstance.AddItem(thisItem);
            Destroy(gameObject);
        }
    }
}
