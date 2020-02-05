using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunInventUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    /// <summary>
    /// Drops the weapon you are hovering over on a right click.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            WeaponController.MyInstance.DropWeapon();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
