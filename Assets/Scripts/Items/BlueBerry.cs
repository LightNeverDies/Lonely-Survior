using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBerry : InventoryItemCollection
{

    public int HealthPoints = 5;
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

        Player.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
