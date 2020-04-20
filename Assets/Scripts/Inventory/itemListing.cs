using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class ItemListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    TextMeshProUGUI itemName = null;

    [SerializeField]
    public int quantity = 0;

    string wholeInstancedText;

    [SerializeField]
    public Item thisListedItem;

    InventoryController inv;
    bool onHover = false;

    public void Start()
    {
        inv = InventoryController.MyInstance;
        inv.OnItemChangeStackCallback += ReduceStack;
        inv.OnItemDropToFloorCallback += DropItem;
        inv.OnItemDestroyitemCallback += DestroyItem;
        inv.OnAddItemToHotbarCallback += AddToHotbar;

    }

    //When there is no stack of this item.
    public void InitItemListing(Item item)
    {
        thisListedItem = item;
        itemName = GetComponent<TextMeshProUGUI>();
        quantity += 1; //TODO:: Replace 1 with bundle amount.
        Debug.Log("Initializing new item.");

        string itemListingName = item.ItemName;
        wholeInstancedText = string.Format("{0}", itemListingName);

        itemName.text = wholeInstancedText;
    }
    //there is a stack of this item...
    public void InitStack(Item item)
    {
        string itemListingName = item.ItemName;
        quantity += 1;     
        UpdateStack(item, quantity);
    }

    //Called when reducing item quantities (invoked from the item class)
    public void ReduceStack(Item item)
    {

        if (item == thisListedItem)
        {
            quantity -= 1;
            inv.RemoveWeight(thisListedItem);
            RightClickMenu.MyInstance.DestroyMenu();
            UpdateStack(thisListedItem, quantity);
            return;
        }

    }

    //Updates the item stack
    public void UpdateStack(Item item, int q)
    {
        if (q == 0)
        {
            Debug.Log("Update Stack");

            //Stop hover
            onHover = false;
            InventoryUIController.MyInstance.RemoveUIElements();
            //remove item from list
            inv.RemoveItem(thisListedItem, this);
            //Destoy listing
            Destroy(gameObject);
            //unsubscribe from callback event
            inv.OnItemChangeStackCallback -= ReduceStack;
            return;
        }
        //update UI depending on stack count
        else if (q == 1)
        {
            wholeInstancedText = string.Format("{0}", item.ItemName);
            itemName.text = wholeInstancedText;
        }
        else if(q > 1)
        {
            q.ToString();
            wholeInstancedText = string.Format("{0} {1}{2}{3}", item.ItemName, "(", q, ")");
            itemName.text = wholeInstancedText;
        }
    }

    public void DropItem(Item item)
    {
        if (item == thisListedItem)
        {
            quantity = 0;
            UpdateStack(item, quantity);

            RightClickMenu.MyInstance.DestroyMenu();
            Instantiate(item.ItemPrefab, PlayerMovement.MyInstance.GroundCheck.transform.position, Quaternion.identity);
            inv.OnItemDropToFloorCallback -= DropItem;
        }
    }

    public void AddToHotbar(Item item)
    {
        item = thisListedItem;
        HotbarController.MyInstance.AddItemToHB(item, quantity);
    }

    public void DestroyItem(Item item)
    {
        if (item == thisListedItem)
        {
            quantity = 0;
            UpdateStack(item, quantity);

            RightClickMenu.MyInstance.DestroyMenu();
            inv.OnItemDestroyitemCallback -= DestroyItem;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem(thisListedItem);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClickMenu.MyInstance.InstantiateMenu(thisListedItem);
        }
    }
    public void UseItem(Item item)
    {
        item.Use();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryUIController.MyInstance.UpdateUIElements(thisListedItem);
        onHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUIController.MyInstance.RemoveUIElements();
        onHover = false;
    }
}
