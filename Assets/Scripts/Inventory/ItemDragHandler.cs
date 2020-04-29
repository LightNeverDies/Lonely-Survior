using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler , IBeginDragHandler
{
    public InventoryItemCollection Item { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDragging");

    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log("OnDragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        Debug.Log("OnEndDragging");
    }

}
