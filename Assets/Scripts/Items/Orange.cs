using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : InventoryItemCollection
{
    public int FoodPoints = 20;
    public int WaterPoints = 3;
    public PlayerController Player;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Player = player.GetComponent<PlayerController>();
    }
    public override void OnUse()
    {

        Player.Eat(FoodPoints);

        Player.Drink(WaterPoints);

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
