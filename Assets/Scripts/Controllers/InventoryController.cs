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

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [SerializeField]
    CanvasGroup cg;
    [SerializeField]
    bool inventoryOn = false;
    [SerializeField]
    Transform itemListingParent;

    [SerializeField]
    public List<itemListing> itemListings = new List<itemListing>();

    [SerializeField]
    itemListing itemListingPrefab;
    [SerializeField]
    private Stack<Item> itemStack = new Stack<Item>();
    bool itemFound = false;

    itemListing ItemListing;

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


    public bool AddItem (Item item)
    {
        //Bool for when weight limit id applied
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        if (itemWeight + item.ItemWeight > PlayerStats.MyInstance.CarryWeight)
        {
            Debug.Log("You're inventory is full");
            return false;
        }
        else
        {
            foreach (itemListing listing in itemListings)
            {
                if (listing.thisItemListing == item)
                {
                    Debug.Log("True");

                    itemWeight = itemWeight + item.ItemWeight;
                    WeightCheck();

                    listing.GetComponent<itemListing>().AddItem(item);
                    itemFound = true;
                    break;
                }
            }

            if (itemFound == false)
            {

                itemListing instancedItemListingPrefab = Instantiate(itemListingPrefab, itemListingParent);

                itemWeight = itemWeight + item.ItemWeight;
                WeightCheck();


                items.Add(item);
                //Instantiate the listing if item not found.
                itemListings.Add(instancedItemListingPrefab);

                ////Update the listing
                instancedItemListingPrefab.GetComponent<itemListing>().AddItem(item);
            }
            else
                itemFound = false;
            
        }
        return true;

    }
    public void RemoveWeight(Item item)
    {
        itemWeight = itemWeight - item.ItemWeight;
        WeightCheck();

    }
    public void RemoveItem(Item item, itemListing itemListing)
    {
        items.Remove(item);
        itemListings.Remove(itemListing);

        itemWeight = itemWeight - item.ItemWeight;
        WeightCheck();

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }

}
