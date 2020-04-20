using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
delegate void itemActionMethods();
public class Item : ScriptableObject
{
    [SerializeField]
    string itemName;
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Sprite itemImage;
    [SerializeField][Multiline]
    string itemDescription;
    [SerializeField]
    float itemWeight;
    [SerializeField]
    bool isStackable;

    [SerializeField]
    private List<itemActionMethods> itemActionMethods = new List<itemActionMethods>();

    public string ItemName { get => itemName; set => itemName = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public Sprite ItemImage { get => itemImage; set => itemImage = value; }
    public float ItemWeight { get => itemWeight; set => itemWeight = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    internal List<itemActionMethods> ItemActionMethods { get => itemActionMethods; set => itemActionMethods = value; }
    public bool IsStackable { get => isStackable; set => isStackable = value; }



    public virtual void AddToActions()
    {
        ItemActionMethods.Add(Drop);
        ItemActionMethods.Add(Destroy);
        ItemActionMethods.Add(AddToHotbar);
    }


    public virtual void Use()
    {
        //Invoke the stack update event
        if (InventoryController.myInstance.OnItemChangeStackCallback != null)
        {
            Debug.Log("Invoke");
            InventoryController.myInstance.OnItemChangeStackCallback.Invoke(this);
            return;
        }

        Debug.Log("Use");

    }
    public virtual void AddToHotbar()
    {
        if (InventoryController.myInstance.OnAddItemToHotbarCallback != null)
        {
            Debug.Log("Invoke");
            InventoryController.myInstance.OnAddItemToHotbarCallback.Invoke(this);
            return;
        }
    }
    public virtual void Drop()
    {
        if (InventoryController.myInstance.OnItemDropToFloorCallback != null)
        {
            Debug.Log("Invoke");
            InventoryController.myInstance.OnItemDropToFloorCallback.Invoke(this);
            return;
        }
        Debug.Log("Item Dropped");
        
    }
    public virtual void Destroy()
    {
        if (InventoryController.myInstance.OnItemDestroyitemCallback != null)
        {
            Debug.Log("Invoke");
            InventoryController.myInstance.OnItemDestroyitemCallback.Invoke(this);
            return;
        }
        Debug.Log("Destroy Item");
    }
}
