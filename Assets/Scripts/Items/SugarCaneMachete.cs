using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCaneMachete : MonoBehaviour
{
    [Range(1,100)]
    public int DamagePerHit = 10;

    public int GetDamagePerHit()
    {
        return DamagePerHit;
    }


}
