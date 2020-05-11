using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : InventoryItemCollection
{
    public int DamagePerHit = 20;

    public int GetDamagePerHit()
    {
        return DamagePerHit;
    }
    public override void OnUse()
    {
        base.OnUse();
    }
}
