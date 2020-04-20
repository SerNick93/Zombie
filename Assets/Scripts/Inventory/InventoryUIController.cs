using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    Image itemImage; //This will eventually be the 3D model which will rotate.
    [SerializeField]
    TextMeshProUGUI itemDescription;

    private void Start()
    {
        itemName.text = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        itemDescription.text = null;

    }

    public static InventoryUIController myInstance;
    public static InventoryUIController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<InventoryUIController>();
            }
            return myInstance;
        }
    }

    public void UpdateUIElements(Item hoverItem)
    {
        itemName.text = hoverItem.ItemName;
        itemImage.enabled = true;
        itemImage.sprite = hoverItem.ItemImage;

        string itemDesFormatting = string.Format("{0} \n\n{1}", hoverItem.ItemDescription, "Item Weight: " + hoverItem.ItemWeight.ToString() );
        itemDescription.text = itemDesFormatting;
    }
    public void RemoveUIElements()
    {
        itemName.text = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        itemDescription.text = null;
    }
}
