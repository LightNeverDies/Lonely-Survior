using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFliesMoon : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem[] FliesFire;
    public GameObject FF;
    public Light moon;
    void Start()
    {
        FliesFire = FF.GetComponentsInChildren<ParticleSystem>();
        FireFliesBegin();
        moon = GetComponent<Light>();
    }

    private void FireFliesBegin()
    {
        if (moon.intensity > 0.35f)
        {
            Debug.Log("Working");
            foreach (ParticleSystem flies in FliesFire)
            {
                flies.Stop();
            }
        }
    }
}
