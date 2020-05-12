using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : InventoryItemCollection
{
    public int FoodPoints = 15;
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

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
