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

    public GameObject Weapon;

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
        
        if (player.Hand.transform.Find("Wooden Axe"))
        {
            Hud.OpenNPCDialog();
            changeText.text = "Guardian: Disarm Immediately!";
            agree.gameObject.SetActive(true);
            disagree.gameObject.SetActive(true);
        }
    }

    public void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == agree)
        {
            Debug.Log("Clicked: " + agree.name);
            InventoryItemCollection mCurrentItem = Weapon.gameObject.GetComponent<InventoryItemCollection>();
            player.SetItemActive(mCurrentItem,false);
            changeText.text = "Guardian: Thanks";
            agree.gameObject.SetActive(false);
            disagree.gameObject.SetActive(false);
            animator.Play("Armature|Watching");
        }

        if (buttonPressed == disagree)
        {
            //animator.SetTrigger("tr_attack_1");
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
