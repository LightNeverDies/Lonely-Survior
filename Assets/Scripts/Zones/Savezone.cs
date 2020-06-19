using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Savezone : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    public GameObject SaveZ;

    public GameObject SaveMess;

    public Text changeText;


    bool timerIsRunning = false;
    float time = 10f;


    public void OnTriggerStay(Collider other)
    {
        if (player)
        {
            if (player.IsArmed)
            {
                SaveMess.SetActive(true);
               // Timer();
            }
            if (player.Hand.transform.Find("Wooden Axe") || player.Hand.transform.Find("PickAxe"))
            {
                SaveMess.SetActive(true);
               // Timer();
            }
            else
            {
                SaveMess.SetActive(false);
                //ResetTimer();
            }
        }
    }

    /*    public void OnCollisionStay(Collision collision)
        {

                if (player.IsArmed)
                {
                    SaveMess.SetActive(true);
                    //Timer();
                }
                if (player.Hand.transform.Find("Wooden Axe") || player.Hand.transform.Find("PickAxe"))
                {
                    SaveMess.SetActive(true);
                  //  Timer();
                }
                else
                {
                    SaveMess.SetActive(false);
                   // ResetTimer();
                }

        }
    */
    /*    public void OnTriggerEnter(Collider other)
        {
            if (player.IsArmed)
            {
                SaveMess.SetActive(true);
                Timer();
            }
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
        }*/

    public void OnTriggerExit(Collider other)
    {
        SaveMess.SetActive(false);
       // ResetTimer();
    }


/*    public void Timer()
    {
        if (!timerIsRunning)
        {
            //StartCoroutine(WaitForSecond());
            changeText.text = string.Format("Disarm Immediately - Save Zone" + "00:" + time.ToString("00"));
            if (time <= 0)
            {
                SaveMess.SetActive(false);
                player.TakeDamage(100);
            }

        }
    }*/

/*    public void ResetTimer()
    {
        time = 10;
        if (!timerIsRunning)
        {
            //StartCoroutine(WaitForSecond());
            changeText.text = string.Format("Disarm Immediately - Save Zone" + "00:" + time.ToString("00"));

            if (time <= 0)
            {
                SaveMess.SetActive(false);
                player.TakeDamage(100);
            }
        }
    }
*//*
    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(1f);
        //time -= Time.deltaTime;
    }*/

/*    void FixedUpdate()
    {
        time -= Time.deltaTime;
    }*/

}
