using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : InventoryItemCollection
{
    public int HealthPoints = 5;
    public PlayerController Player;
    public override void OnUse()
    {
        Player.Rehab(HealthPoints);

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
