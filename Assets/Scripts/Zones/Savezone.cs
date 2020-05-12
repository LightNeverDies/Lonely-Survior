using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Savezone : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    public GameObject SaveZ;

    public GameObject SaveMess;

    public Text changeText;

    bool timerIsRunning = false;
    float time = 9;

    public void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (player)
        {
            if (player.Hand.transform.Find("Wooden Axe") || player.Hand.transform.Find("PickAxe"))
            {
                SaveMess.SetActive(true);
                Timer();
            }

            else
            {
                SaveMess.SetActive(false);
                ResetTimer();
            }
        }
    }

    public void Timer()
    {
        if (!timerIsRunning)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                changeText.text = string.Format("Disarm Immediately - Save Zone" + "00:0{0}", Mathf.FloorToInt(time % 360));
                if (time <= 0)
                {
                    SaveMess.SetActive(false);
                    player.TakeDamage(100);
                }
            }

        }
    }
    public void ResetTimer()
    {
        if (!timerIsRunning)
        {
            time = 9;
            if (time > 0)
            {
                time -= Time.deltaTime;
                changeText.text = string.Format("Disarm Immediately - Save Zone" + "00:0{0}", Mathf.FloorToInt(time % 360));
                if (time <= 0)
                {
                    SaveMess.SetActive(false);
                    player.TakeDamage(100);
                }
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {

        gameObject.GetComponent<SphereCollider>().isTrigger = false;
        SaveZ.SetActive(false);
        SaveMess.SetActive(false);
    }




}
