using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : InventoryItemCollection
{
    public int FoodPoints = 5;
    public int WaterPoints = 30;
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
