using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvetoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void onPickup();

    void OnDrop();
    void OnUse();

    void DropItem();
}

public class InventoryEventArgs : EventArgs
{
        public InventoryEventArgs(IInvetoryItem item)
        {
            Item = item;
        }
        public IInvetoryItem Item;
}

