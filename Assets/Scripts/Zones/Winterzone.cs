using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Winterzone : MonoBehaviour
{
    public GameObject Coldness;

    public Text coldness;

    // float radius = 0;
    int temperature = 0;
    float timeS = 0;
    float timeC = 0;

    public Light sun;
    public Light moon;

    public PlayerController player;

    void Start()
    {
        //radius = gameObject.GetComponent<SphereCollider>().radius;
        // radius could be used for the coldness damage to the player later.
        /*        sun = GetComponent<Light>();
                moon = GetComponent<Light>();*/
        //Debug.Log(radius);
    }
    public void OnTriggerStay(Collider other)
    {

        if (player)
        {
            Coldness.SetActive(true);
            if (Time.time >= timeS)
            {
                Cold();
                timeS = Time.time + 15f / 5f;
            }
            if(Time.time >= timeC)
            {
                ColdDamage();
                timeC = Time.time + 80f / 5f;
            }
        }
    }

    private void Cold()
    {

        if (sun.intensity > 1.13f && sun.intensity < 1.30f)
        {
            temperature++;
            coldness.text = string.Format("{0}°c", temperature);
        }
        if (sun.intensity > 1.1 & sun.intensity < 1.12)
        {
            temperature = 0;
        }
        if(moon.intensity > 0.35f && moon.intensity < 0.39f)
        {
            temperature--;
            coldness.text = string.Format("{0}°c", temperature);
        }
    }
    private void ColdDamage()
    {
        if (moon.intensity == 0.40f)
        {
            temperature--;
            if (temperature >= -10)
            {
                if (temperature <= -4)
                {
                    TakeDamage();
                }
                if (temperature == -10)
                {
                    InvokeRepeating("TakeDamage", 0, 2);
                }
                coldness.text = string.Format("{0}°c", temperature);
            }
        }
    }

    private void TakeDamage()
    {
        int coldTakeDamage = Random.Range(1, 2);
        player.TakeDamage(coldTakeDamage);
    }

    public void OnTriggerExit(Collider other)
    {
        Coldness.SetActive(false);
        temperature = 0;
        CancelInvoke("TakeDamage");
    }
}
