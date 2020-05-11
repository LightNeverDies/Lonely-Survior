using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisablePlayer : MonoBehaviour
{

    public PlayerController player;

    public hud Hud;

    public GameObject Options;

    // Update is called once per frame
    void Update()
    {
        Resume();
        Option();
        if(player.IsDead)
        {
            Hud.OpenGameOver();
            WaitForSeconds();
        }
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
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

    public void Option()
    {
        if(Options.activeInHierarchy == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Options.SetActive(false);
            }
        }
    }
    public void ButtonResume()
    {
        Time.timeScale = 1;
    }


    public void RestartGame()
    {
        Hud.CloseGameOver();
        SceneManager.LoadScene("Tutorial");
    }
}
