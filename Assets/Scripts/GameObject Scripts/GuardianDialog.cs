using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardianDialog : MonoBehaviour
{
    public hud Hud;

    public Text changeText;

    public Button agree, disagree;
    Button buttonPressed;

    public GameObject[] Weapons;

    public GameObject guardian;

    public Animator animator;

    public PlayerController player;

    private NavMeshAgent agent;




    void Start()
    {
        animator = guardian.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.Play("Armature|Watching");
    }

    private void OnTriggerEnter(Collider other)
    {  
        foreach(GameObject index in Weapons)
        {
            InventoryItemCollection mCurrentItem = index.gameObject.GetComponent<InventoryItemCollection>();
            if (player.Hand.transform.Find("Wooden Axe") || player.Hand.transform.Find("PickAxe"))
            {
                if (mCurrentItem.ItemType == EItemType.Weapon)
                {
                    Hud.OpenNPCDialog();
                    changeText.text = "Guardian: Disarm Immediately!";
                    agree.gameObject.SetActive(true);
                    disagree.gameObject.SetActive(true);
                }
            }
        }
    }

    public void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == agree)
        {
            Debug.Log("Clicked: " + agree.name);
            foreach (GameObject index in Weapons)
            {
                InventoryItemCollection mCurrentItem = index.gameObject.GetComponent<InventoryItemCollection>();
                 if (player.Hand.transform.Find("Wooden Axe") || player.Hand.transform.Find("PickAxe"))
                  {
                    if (mCurrentItem.ItemType == EItemType.Weapon)
                    {
                        player.SetItemActive(mCurrentItem, false);
                        changeText.text = "Guardian: Thanks";
                        agree.gameObject.SetActive(false);
                        disagree.gameObject.SetActive(false);
                        animator.Play("Armature|Watching");
                    }
                  }
            }
        }

        if (buttonPressed == disagree)
        {
            animator.Play("Armature|Attack");
            player.GuardianKill();
            Hud.CloseNPCDialog();
            Debug.Log("Clicked: " + disagree.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Hud.CloseNPCDialog();
    }


}
