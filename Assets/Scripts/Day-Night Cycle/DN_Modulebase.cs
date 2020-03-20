using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DN_Modulebase : MonoBehaviour
{
    protected DayNight dayNight;

    private void OnEnable()
    {
        dayNight = this.GetComponent<DayNight>();
        if (dayNight != null)
        {
            dayNight.AddModule(this);
        }
    }

    private void OnDisable()
    {
        if(dayNight !=null)
        {
            dayNight.RemoveModule(this);
        }
    }

    public abstract void UpdateModule(float intensity);
}
