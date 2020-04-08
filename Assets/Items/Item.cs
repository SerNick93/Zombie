using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
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

    public string ItemName { get => itemName; set => itemName = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public Sprite ItemImage { get => itemImage; set => itemImage = value; }
    public float ItemWeight { get => itemWeight; set => itemWeight = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }

    public virtual void Use()
    {

    }
}
