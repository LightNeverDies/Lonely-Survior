using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed=50;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0);
            //transform.Rotate(0, 0, 0);
        }
    }
}
