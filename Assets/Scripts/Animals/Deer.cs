using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : MonoBehaviour
{
    public Transform player;

    public float closeDistance = 8.0f;

    public float rotSpeed = 1.0f;

    public float speedDeer = 5.0f;

    Animator animator;

    private NavMeshAgent agent;



    
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private bool IsNavMeshMoving
    {
        get
        {
            return agent.velocity.magnitude > 0.1f;
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (player)
        {
            Vector3 distanceBetween = transform.position - player.position;

            float sqrLen = distanceBetween.magnitude;

            if (sqrLen < closeDistance * closeDistance)
            {

                Vector3 newDirection = transform.position + distanceBetween;
                agent.speed = speedDeer;


              /*  Quaternion rot = Quaternion.LookRotation(newDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);*/

                float singleStep = rotSpeed * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, newDirection, singleStep, 0.0f);
                agent.updateRotation = true ;

                agent.SetDestination(newDirection);
                Debug.DrawLine(transform.position, newDirection, Color.red);
                animator.SetBool("move", true);


               // print("The other transform is close to me!");

            }
            else if (sqrLen > closeDistance * closeDistance)
            {
                animator.SetBool("move", IsNavMeshMoving);
                StartCoroutine(WaitForSecond());
              //  print("Now it's safe!");

            }
        }
    }

    IEnumerator WaitForSecond()
    {
        animator.SetBool("eating", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("eating",false);

    }
}
