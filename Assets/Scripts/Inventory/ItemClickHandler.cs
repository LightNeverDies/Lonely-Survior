using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;
    public KeyCode _Key;
    public PlayerController Player;
    public GameObject player;

    public float tapSpeed = 0.5f;
    private float lastTapTime = 0;

    private Button _button;
    private void Awake()
    {
        player = GameObject.Find("Player");
        _button = GetComponent<Button>();
        Player = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!Player.IsDead)
        {
            InventoryItemCollection item = AttachedItem;
            if (item != null)
            {
                if (Input.GetKeyDown(_Key))
                {
                    FadeToColor(_button.colors.pressedColor);
                    _button.onClick.Invoke();
                    if ((Time.time - lastTapTime) < tapSpeed)
                    {
                        if (item.ItemType != EItemType.Consumable)
                        {
                            if (item != null)
                            {
                                Debug.Log("Double tap");
                                item.gameObject.SetActive(false);
                                item.transform.parent = null;
                            }
                        }
                    }
                    lastTapTime = Time.time;
                }
                else if (Input.GetKeyUp(_Key))
                {
                    FadeToColor(_button.colors.normalColor);
                }
            }
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
    }

    private InventoryItemCollection AttachedItem
    {
        get
        {
            ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
            if (dragHandler.Item != null) // code added
            {
                return dragHandler.Item; 
            }
            return null; // code added
        }
    }
    public void OnItemClicked()
    {
        if (!Player.IsDead)
        {
            InventoryItemCollection item = AttachedItem;
            if (item != null)
            {
                _Inventory.UseItem(item);
                item.OnUse();
            }
        }  
    }

}
