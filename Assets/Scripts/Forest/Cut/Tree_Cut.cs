using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Cut : MonoBehaviour
{
    private Animator mAnimator;

    public GameObject HitEffect;

    public GameObject HitEffectPosition;

    public GameObject Firewood;

    public GameObject Weapon;

    private int mHitCount = 0;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void Fall()
    {
        mAnimator.SetTrigger("TrFall");

        Destroy(GetComponent<BoxCollider>());
        StartCoroutine(WaitAnim());

    }
    public IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        Firewood.SetActive(true);
    }

    private void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        InteractableItemBase item = Weapon.gameObject.GetComponent<InteractableItemBase>();
        InventoryItemCollection mCurrentItem = Weapon.gameObject.GetComponent<InventoryItemCollection>();
        int MaxHitCount = Random.Range(3, 5);

        if (Input.GetMouseButtonDown(0))
        {
            if (Weapon.activeSelf)
            {
                if (mCurrentItem.ItemType == EItemType.Weapon)
                {
                    Debug.Log(item);
                    mHitCount++;
                    var pos = HitEffectPosition.transform.position;

                    // Play hit sound
                    var hitEffect = (GameObject)Instantiate(HitEffect, pos, transform.rotation, transform.parent);
                    Destroy(hitEffect, 1.5f);

                    if (mHitCount >= MaxHitCount)
                    {
                        Fall();
                    }
                }
            }
        }
    }

    private void Attack()
    {
        
    }




}
