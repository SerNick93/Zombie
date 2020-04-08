using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class itemListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    TextMeshProUGUI itemName = null;
    [SerializeField]
    public int quantity = 0;
    string wholeInstancedText;

    [SerializeField]
    public Item thisItemListing;


    public void AddItem(Item itemListing)
    {
        thisItemListing = itemListing;
        itemName = GetComponent<TextMeshProUGUI>();
        quantity++;
        Debug.Log(thisItemListing + " " + quantity);

        //Debug.Log(quantity);

        if (quantity > 1)
        {
            UpdateQuantity(thisItemListing);
        }
        else
        {
            string itemListingName = itemListing.ItemName;
            wholeInstancedText = string.Format("{0}", itemListingName);
            itemName.text = wholeInstancedText;
        }



    }
    public void UpdateQuantity(Item itemListing)
    {     
        Debug.Log(itemListing + " " + quantity);
        string itemListingName = itemListing.ItemName;
        string q = quantity.ToString();
        if (quantity == 1)
        {
            wholeInstancedText = string.Format("{0}", itemListingName);
        }
        else if (quantity > 1)
        {
            wholeInstancedText = string.Format("{0} {1}{2}{3}", itemListing.ItemName, "(", q, ")");
        }
        itemName.text = wholeInstancedText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        InventoryUIController.MyInstance.UpdateUIElements(thisItemListing);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUIController.MyInstance.RemoveUIElements(thisItemListing);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        quantity--;
        if (quantity < 1)
        {
            InventoryUIController.MyInstance.RemoveUIElements(thisItemListing);
            Destroy(gameObject);
            InventoryController.MyInstance.RemoveItem(thisItemListing, this);

        }
        else if (quantity >= 1)
        {
            UpdateQuantity(thisItemListing);
            InventoryController.MyInstance.RemoveWeight(thisItemListing);

        }
        UseItem(thisItemListing);
    }
    public void UseItem(Item item)
    {
        thisItemListing.Use();
    }
}
