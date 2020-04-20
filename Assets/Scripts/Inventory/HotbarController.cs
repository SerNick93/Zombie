using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    public static HotbarController myInstance;
    public static HotbarController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<HotbarController>();
            }
            return myInstance;
        }
    }


    [SerializeField]
    Slot[] hotbarSlot;
    public void AddItemToHB(Item item, int stackCount)
    {
        for (int i = 0; i < hotbarSlot.Length; i++)
        {
            if (hotbarSlot[i].Icon.sprite == null)
            {
                //Add item to slot.
                hotbarSlot[i].Icon.sprite = item.ItemImage;
                hotbarSlot[i].StackCount.text = stackCount.ToString();
                break;
            }
        }
    }

}
