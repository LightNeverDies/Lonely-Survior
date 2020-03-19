using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : InventoryItemCollection
{
    public override string Name
    {
        get
        {
            return "Axe";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }
}
