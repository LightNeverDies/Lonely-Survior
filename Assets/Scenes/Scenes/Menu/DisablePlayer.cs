using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisablePlayer : MonoBehaviour
{

    public PlayerController player;

    public hud Hud;

    public Button resume;

    Button press;

    // Update is called once per frame
    void Update()
    {
        Resume();
    }

    public void Resume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hud.Shadow.SetActive(!Hud.Shadow.activeSelf);
            if (Hud.Shadow.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

    }
    public void ButtonResume()
    {
        Time.timeScale = 1;
    }
}
