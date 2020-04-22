using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFliesSun : MonoBehaviour
{
    public ParticleSystem[] FliesFire;
    public GameObject FF;
    public Light sun;
    void Start()
    {
        FliesFire = FF.GetComponentsInChildren<ParticleSystem>();
        sun = GetComponent<Light>();
    }
    private void Update()
    {
        FireFliesBegin();
    }

    private void FireFliesBegin()
    {
        if (sun.intensity > 1.2f)
        {
            foreach (ParticleSystem flies in FliesFire)
            {
                flies.Stop();
            }
        }
        else if (sun.intensity > 1f)
        {
            foreach (ParticleSystem flies in FliesFire)
            {
                flies.Play();
            }
        }
    }
}
