using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunInventUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Open Menu when right clicking on the buttons for the weapon UI.
            //Only when there is a weapon active there.
            //Menu needs to spawn at the menu pivot, where the mouse pointer is. 

            WeaponUIController.MyInstance.RightClickMenu();
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(EventSystem.current.name);
        }
        WeaponUIController.MyInstance.CloseRightClickMenu();

    }
}
