using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySlot
{
    private Stack<InventoryItemCollection> mItemStack = new Stack<InventoryItemCollection>();
    private int mId = 0;
    public InventorySlot(int id)
    {
        mId = id;
    }
    public int Id
    {
        get { return mId; }
    }
    public void AddItem(InventoryItemCollection item)
    {
        item.Slot = this;
        mItemStack.Push(item);
    }
    public InventoryItemCollection FirstItem
    {
        get
        {
            if (IsEmpty)
            {
                return null;
            }
            return mItemStack.Peek();
        }
    }
    public bool IsStackable(InventoryItemCollection item)
    {
        if (IsEmpty)
            return false;

        InventoryItemCollection first = mItemStack.Peek();
        if (first.ItemType == EItemType.Weapon)
        {
            return false;
        }
        else if (first.Name == item.Name)
        {
            return true;
        }

        return false;
    }

    public bool IsEmpty
    {
        get { return Count == 0; }
    }

    public int Count
    {
        get { return mItemStack.Count; }
    }
    public bool Remove(InventoryItemCollection item)
    {
        if (IsEmpty)
            return false;

        InventoryItemCollection first = mItemStack.Peek();
        if(first.Name == item.Name)
        {
            mItemStack.Pop();
            return true;
        }

        return false;
    }
}
