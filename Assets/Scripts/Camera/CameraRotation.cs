using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed=50f;

    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        if (Input.GetKey(KeyCode.Keypad4))
            transform.Rotate(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.Keypad6))
            transform.Rotate(0, -speed * Time.deltaTime, 0);
    }


}
