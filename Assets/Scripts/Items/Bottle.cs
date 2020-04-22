using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : InventoryItemCollection
{
    public int WaterPoints = 30;
    public PlayerController Player;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Player = player.GetComponent<PlayerController>();
    }

    public override void OnUse()
    {
        Player.Drink(WaterPoints);

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
