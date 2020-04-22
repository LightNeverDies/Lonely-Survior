using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoof : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Roof;
    public Camera mainCamera;
    public Camera topCamera;
    

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "MPlayer")
        {
            Roof.SetActive(true);
            mainCamera.enabled = true;
            topCamera.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "MPlayer")
        {
            Roof.SetActive(false);
            mainCamera.enabled = false;
            topCamera.enabled = true;
        }
    }
}
