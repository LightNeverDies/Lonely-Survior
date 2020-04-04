using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBerry : InventoryItemCollection
{
    public int HealthPoints = 2;
    public int FoodPoints = 15;
    public int WaterPoints = 5;
    public PlayerController Player;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Player = player.GetComponent<PlayerController>();
    }
    public override void OnUse()
    {
        Player.Rehab(HealthPoints);

        Player.Eat(FoodPoints);

        Player.Drink(WaterPoints);

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
