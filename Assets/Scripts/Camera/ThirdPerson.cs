using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    //public GameObject player;
     private Vector3 targetPosition;

     [SerializeField]
     private float distanceAway=2;
     [SerializeField]
     private float distanceUp=2;
     [SerializeField]
     private float smooth=1;
     [SerializeField]
     private Transform follow;


    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }


    void LateUpdate()
    {

        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        transform.LookAt(follow);

    }





    /*    private void Camera_Start()
        {
            offset = transform.position - player.transform.position;
        }
        private void Camera_Update()
        {
            transform.position = player.transform.position + offset;
        }*/
}
