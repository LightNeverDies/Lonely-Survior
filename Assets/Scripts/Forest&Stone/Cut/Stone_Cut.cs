using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Cut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HitEffect;

    public GameObject HitEffectPosition;

    public GameObject[] Stone;

    public GameObject Weapon;

    public PlayerController player;

    private int mHitCount = 0;

    public float AttackRate = 1.0f;

    float nextAttack = 0f;

    public IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        foreach (GameObject item in Stone)
        {
            item.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        InteractableItemBase item = Weapon.gameObject.GetComponent<InteractableItemBase>();
        InventoryItemCollection mCurrentItem = Weapon.gameObject.GetComponent<InventoryItemCollection>();

        int MaxHitCount = Random.Range(3, 5);

        if (player.Hand.transform.Find("PickAxe"))
        {
            if (Time.time >= nextAttack)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log(item);
                    mHitCount++;
                    var pos = HitEffectPosition.transform.position;
                    //Debug.Log(mHitCount);
                    // Play hit sound
                    var hitEffect = (GameObject)Instantiate(HitEffect, pos, transform.rotation, transform.parent);
                    Destroy(hitEffect, 1.5f);

                    if (mHitCount >= MaxHitCount)
                    {
                        StartCoroutine(WaitAnim());
                    }
                    nextAttack = Time.time + 1f / AttackRate;
                }
            }
        }

    }

}
