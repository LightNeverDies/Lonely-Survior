using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler //, IBeginDragHandler
{
    public InventoryItemCollection Item { get; set; }
 

/*    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        originalParent = transform.parent;
        transform.SetParent(parentDrag, true);
        Debug.Log("BeginDragging");

    }
*/
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Debug.Log("OnDragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        Debug.Log("OnEndDragging");
    }
}
