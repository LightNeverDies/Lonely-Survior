using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyPack : MonoBehaviour
{
    private int mHitCount = 0;

    public PlayerController player;

    public hud Hud;

    public GameObject HitEffectPositon;

    public Text changeText;

    private string HintText = "Destroy the rock.";

    public float AttackRate = 1.0f;

    float nextAttack = 0f;


    public IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        Hud.CloseHint();
        changeText.text = "Press E to start or stop it.";
    }

    private void OnTriggerStay(Collider other)
    {
        int MaxHitCount = Random.Range(4, 7);
        changeText.text = HintText;
        Hud.OpenHint();

        if (player.Hand.transform.Find("PickAxe") && Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextAttack)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mHitCount++;
                    var pos = HitEffectPositon.transform.position;
                    Debug.Log(mHitCount);
                    if (mHitCount >= MaxHitCount)
                    {
                        StartCoroutine(WaitAnim());
                    }
                    nextAttack = Time.time + 1f / AttackRate;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Hud.CloseHint();
    }
}
