using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class InventoryController : MonoBehaviour
{
    public static InventoryController myInstance;
    public static InventoryController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<InventoryController>();
            }
            return myInstance;
        }
    }

    //Update Stack Delegate
    public delegate void OnItemChangeStack(Item item);
    public OnItemChangeStack OnItemChangeStackCallback;

    public delegate void OnItemDropToFloor(Item item);
    public OnItemDropToFloor OnItemDropToFloorCallback;

    public delegate void OnItemDestroyItem(Item item);
    public OnItemDestroyItem OnItemDestroyitemCallback;

    public delegate void OnAddItemToHotbar(Item item);
    public OnAddItemToHotbar OnAddItemToHotbarCallback;



    [SerializeField]
    CanvasGroup cg;
    [SerializeField]
    bool inventoryOn = false;
    [SerializeField]
    Transform itemListingParent;

    [SerializeField]
    public List<ItemListing> itemListings = new List<ItemListing>();

    [SerializeField]
    ItemListing itemListingPrefab;

    bool itemFound = false;

    ItemListing ItemListing;

    [SerializeField]
    private List<Item> items = new List<Item>();

    [SerializeField]
    float itemWeight;
    [SerializeField]
    TextMeshProUGUI itemWeightText;


    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryOn)
            {
                cg.alpha = 1;
                cg.blocksRaycasts = true;
                inventoryOn = true;
                WeightCheck();
                UIController.MyInstance.TurnCursorOn();
            }

            else if (inventoryOn)
            {
                cg.alpha = 0;
                cg.blocksRaycasts = false;
                inventoryOn = false;
                UIController.MyInstance.TurnCursorOff();

            }
        }
    }
    public void WeightCheck()
    {
        string weightString = string.Format("{0} {1}{2}{3}", "Weight:", itemWeight.ToString(), "/", PlayerStats.MyInstance.CarryWeight.ToString());
        itemWeightText.text = weightString;
    }


    public bool AddItem(Item item)
    {
        if (itemWeight + item.ItemWeight > PlayerStats.MyInstance.CarryWeight)
        {
            Debug.Log("You cannot carry anymore.");
            return false;
        }
        //Add to a pre existing stack
        else
        {
            foreach (ItemListing listing in itemListings)
            {
                if (listing.thisListedItem == item)
                {
                    itemWeight = itemWeight + item.ItemWeight;
                    WeightCheck();

                    listing.GetComponent<ItemListing>().InitStack(item);

                    itemFound = true;
                    break;
                }
            }
            if (itemFound == false)
            {
                itemHasNotBeenFound(item);
                ////Update the listing
            }
            else
                itemFound = false;
        }
        return true;

    }



    public void itemHasNotBeenFound(Item item)
    {
        ItemListing instancedItemListingPrefab = Instantiate(itemListingPrefab, itemListingParent);

        Item instancedItem = item;
        itemWeight = itemWeight + instancedItem.ItemWeight;
        WeightCheck();

        items.Add(instancedItem);
        //Instantiate the listing if item not found.
        itemListings.Add(instancedItemListingPrefab);

        instancedItemListingPrefab.GetComponent<ItemListing>().InitItemListing(instancedItem);

    }

    //Weight management
    public void RemoveWeight(Item item)
    {
        itemWeight = itemWeight - item.ItemWeight;
        WeightCheck();

    }
    //remove item and listing from their lists.
    public void RemoveItem(Item item, ItemListing itemListing)
    {
        Item instancedItem = item;
        ItemListing instancedItemListing = itemListing;


        items.Remove(instancedItem);
        itemListings.Remove(instancedItemListing);
    }
    

}
