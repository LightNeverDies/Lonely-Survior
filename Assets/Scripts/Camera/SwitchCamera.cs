using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public Camera topCamera;
    void Start()
    {
        mainCamera.enabled = true;
        topCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            mainCamera.enabled = !mainCamera.enabled;
            topCamera.enabled = !topCamera.enabled;
        }
    }
}
