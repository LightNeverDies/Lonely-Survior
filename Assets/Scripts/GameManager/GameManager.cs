using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance = null;

    public PlayerController Player;

    public GameObject GameOverHUD;

    public float ShowGameOverTime = 1.5f;

    public event EventHandler GameOver;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Dont destroy on reloading the scene
        // DontDestroyOnLoad(gameObject);

        if (Player == null)
        {
            Debug.LogError("You need to assign a Player to the GameManager");
        }

        if (Player != null)
        {
           // Player.IsDead += Player_PlayerDied;
        }
    }

    private void Player_PlayerDied(object sender, System.EventArgs e)
    {
        if (Player.Hud == null)
        {
            Debug.LogError("You need to assign a HUD to the PlayerController");
        }
        else
        {
            Player.Hud.gameObject.SetActive(false);

            if (GameOverHUD == null)
            {
                Debug.LogError("You need to assign a GameOverHUD to the GameManager");
            }
            else
            {
                GameOverNotify();

                Invoke("ShowGameOver", ShowGameOverTime);
            }
        }
    }

    private void GameOverNotify()
    {
        if (GameOver != null)
            GameOver(this, EventArgs.Empty);
    }

    private void ShowGameOver()
    {
        GameOverHUD.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
