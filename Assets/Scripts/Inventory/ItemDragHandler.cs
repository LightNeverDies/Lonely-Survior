using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public InventoryItemCollection Item { get; set; }

    public static GameObject item;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // ->code that was used before

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero; // -> code that was used before
    }

}
