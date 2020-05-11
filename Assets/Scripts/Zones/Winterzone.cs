using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winterzone : MonoBehaviour
{
    public GameObject Coldness;
    public void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            Coldness.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Coldness.SetActive(false);
    }
}
