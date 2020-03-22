using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : InventoryItemCollection
{
    public override string Name
    {
        get
        {
            return "Wood";
        }
    }
    public override void OnUse()
    {
        base.OnUse();
    }
}
