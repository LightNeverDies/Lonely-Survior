using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public ParticleSystem Fire;
    public ParticleSystem Smoke;

    public hud Hud;
    public GameObject campfire;

    void Start()
    {
        Fire.Stop();
        Smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(campfire != null &&Input.GetKeyDown(KeyCode.E))
        {
            Camp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(campfire != null)
        {
            Hud.OpenHint("");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (campfire != null)
        {
            Hud.CloseHint();
        }
    }

    private void Camp()
    {
       if (Fire.isPlaying && Smoke.isPlaying)
       {
         Fire.Stop();
         Smoke.Stop();
         Debug.Log("in?");
       }
       else
       {
         Fire.Play();
         Smoke.Play();
       }
    }


}
