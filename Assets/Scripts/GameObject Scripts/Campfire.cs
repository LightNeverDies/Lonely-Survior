using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public ParticleSystem Fire;
    public ParticleSystem Smoke;

    public hud Hud;
    private bool _isTrigger= false;

    void Start()
    {
        Fire.Stop();
        Smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if(Input.GetKeyDown(KeyCode.E))
        {
            Camp();
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Camp();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
            Hud.OpenHint("");
    }
    private void OnTriggerExit(Collider other)
    {
            Hud.CloseHint();
    }



    private void Camp()
    {
        if (Fire.isPlaying && Smoke.isPlaying)
        {
            Fire.Stop();
            Smoke.Stop();
        }
        else
        {
            Fire.Play();
            Smoke.Play();
        }
    }


}
