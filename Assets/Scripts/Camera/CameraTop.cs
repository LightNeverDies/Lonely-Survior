using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform follow;
    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(follow);   
    }
}
