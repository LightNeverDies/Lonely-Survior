using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc : MonoBehaviour
{
    public Transform player;


    public float distance = 12.0f;

    public float rotSpeed = 1.0f;

    public float speedOrc = 8.0f;


    //Attack CoolDown

    public int OrcHealth = 100;

    float OrcAttackTime = 0f;

    float OrcAttackRate = 2.0f;


    public PlayerController Nplayer;

    private NavMeshAgent agent;

    private OrcHealthBar orcHealthBar;

    SugarCaneMachete OrcWeapon;

    Axe Playeraxe;

    Animator orcAnimator;


    void Start()
    {
        orcAnimator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        OrcWeapon = GameObject.Find("SugarcaneMachete").GetComponent<SugarCaneMachete>();
        orcHealthBar = GameObject.Find("OrcHealth").GetComponent<OrcHealthBar>();
        Playeraxe = GameObject.Find("Wooden Axe").GetComponent<Axe>();

    }


    private bool IsNavMeshMoving
    {
        get
        {
            orcAnimator.Play("Armature|Idle_Watching");
            return agent.velocity.magnitude > 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsDeadOrc
    {
        get
        {
            return OrcHealth == 0;
        }
    }

    


    public void CauseDamage()
    {
        int hitdamage = OrcWeapon.GetDamagePerHit();
        if (Time.time >= OrcAttackTime)
        {
            orcAnimator.SetTrigger("attack_1");
            Nplayer.TakeDamage(hitdamage);
            OrcAttackTime = Time.time + 1f / OrcAttackRate;
        }
    }

    public void TakeDamage()
    {
        int amountDamagerPerHit = Playeraxe.GetDamagePerHit();
        OrcHealth -= amountDamagerPerHit;
        orcAnimator.SetTrigger("tr_hit");
        if (OrcHealth < 0)
            OrcHealth = 0;

        orcHealthBar.SetHealth(OrcHealth);

        if (IsDeadOrc)
        {
            OrcDead();
        }
    }

    public void OrcDead()
    {
        orcAnimator.SetBool("isdead", true);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine("BodyGone");
    }


    public IEnumerator BodyGone()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        if (player && !IsDeadOrc)
        {
            Vector3 distanceBetween = transform.position - player.position;

            float sqrLen = distanceBetween.magnitude;


            if (sqrLen < distance * distance)
            {

                Vector3 newDirection = transform.position - distanceBetween;
                agent.speed = speedOrc;
                agent.SetDestination(newDirection);


                orcAnimator.SetBool("run", true);
                Debug.DrawLine(transform.position, newDirection, Color.red);
            }

            if (Nplayer.IsDead || sqrLen > distance * distance)
            {
                orcAnimator.SetBool("run", IsNavMeshMoving);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Nplayer.isAttack)
        {
           TakeDamage();
        }
        if(player)
        {
            CauseDamage();
        }
    }


}
