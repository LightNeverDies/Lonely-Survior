using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public ParticleSystem Fire;
    public ParticleSystem Smoke;

    private bool _isCausingDamage = false;
    public float DamageRepeatRate = 0.1f;
    public int DamageAmount = 1;

    public bool Repeating = true;
    public hud Hud;

    void Start()
    {
        Fire.Stop();
        Smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(WaitForSecond());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Hud.OpenHint();
        if(Fire.isPlaying && Smoke.isPlaying)
        {
            _isCausingDamage = true;

            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                if (Repeating)
                {
                    StartCoroutine(TakeDamage(player, DamageRepeatRate));
                }
                else
                {
                    player.TakeDamage(DamageAmount);
                }
            }
        }
        else if(!Fire.isPlaying && !Smoke.isPlaying)
        {
            _isCausingDamage = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Hud.CloseHint();
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                _isCausingDamage = false;
            }

    }


    IEnumerator WaitForSecond()
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
            yield return new WaitForSeconds(10);
            Fire.Stop();
            Smoke.Stop();
        }
    }


    IEnumerator TakeDamage(PlayerController player, float repeatRate)
    {
        while (_isCausingDamage)
        {
            player.TakeDamage(DamageAmount);
            TakeDamage(player, repeatRate);
            if (player.IsDead)
            {
                _isCausingDamage = false;
            }
            yield return new WaitForSeconds(repeatRate);
        }
    }


}
