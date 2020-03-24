using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(InventoryItemCollection item)
    {
      Item = item;
    }
    public InventoryItemCollection Item;
}

