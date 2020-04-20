using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RightClickMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static RightClickMenu myInstance;
    public static RightClickMenu MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<RightClickMenu>();
            }
            return myInstance;
        }
        
    }

    [SerializeField]
    private GameObject rightClickMenu;

    [SerializeField]
    Transform parentCanvas;

    [SerializeField]
    private GameObject rightClickOptionPrefab;

    Vector3 mousePos;
    bool isRightClickActive = false;
    GameObject instantiatedRightClickMenu;
    GameObject instancedRightCLickOptionPrefab;
    TextMeshProUGUI optionText;
    bool overMenu = false;
    Button optionTextButton;
    public void LateUpdate()
    {
        mousePos = Input.mousePosition;
        //This needs to destroy them menu, ONLY if the player is not hovering over the 
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log(EventSystem.current.tagw);
        //    Debug.Log("Destroy");
        //    DestroyMenu();
        //}
    }

    //Instantiat RC Menu
    public void InstantiateMenu(Item item)
    {
        if (!isRightClickActive)
        {
            instantiatedRightClickMenu = Instantiate(rightClickMenu, mousePos, Quaternion.identity, parentCanvas);
            isRightClickActive = true;
            InstantiateMenuItems(item);
        }
        else
        {
            Destroy(instantiatedRightClickMenu);
            isRightClickActive = false;
        }

    }

    //Instantiate the items on the menu
    public void InstantiateMenuItems(Item item)
    {
        item.AddToActions();

        //Take the possible use options from the item and put them into the menu items
        //using the itemActionMethod delegate in the Item class.
        foreach (itemActionMethods itemActions in item.ItemActionMethods)
        {
            instancedRightCLickOptionPrefab =
                Instantiate(rightClickOptionPrefab, instantiatedRightClickMenu.transform);

            optionText = instancedRightCLickOptionPrefab.GetComponent<TextMeshProUGUI>();

            optionTextButton = instancedRightCLickOptionPrefab.GetComponent<Button>();

            optionTextButton.onClick.AddListener(delegate { itemActions(); });

            optionText.text = itemActions.Method.Name.ToString();
        }
    }
    public void DestroyMenu()
    {
        Destroy(instantiatedRightClickMenu);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        overMenu = true;
        Debug.Log(overMenu);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overMenu = false;
    }
}
