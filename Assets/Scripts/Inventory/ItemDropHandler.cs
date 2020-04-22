using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public Inventory _Inventory;
    public GameObject player;
    public PlayerController Player;

    void Start()
    {
        player = GameObject.Find("Player");
        Player = player.GetComponent<PlayerController>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        if (!Player.IsDead)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
            {
                InventoryItemCollection item = eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>().Item;
                if (item != null)
                {
                    _Inventory.RemoveItem(item);
                    item.OnDrop();
                }
            }
        }
    }
}
