using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;
    public KeyCode _Key;
    public PlayerController Player;
    public GameObject player;

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
            if (Input.GetKeyDown(_Key))
            {
                FadeToColor(_button.colors.pressedColor);
                _button.onClick.Invoke();
            }
            else if (Input.GetKeyUp(_Key))
            {
                FadeToColor(_button.colors.normalColor);
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

            return dragHandler.Item;
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
