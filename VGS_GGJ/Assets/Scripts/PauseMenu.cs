using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    private GameObject pauseMenuUI;

    private PlayerScript player;
    void Awake()
    {
        paused = false;
        pauseMenuUI = gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                resume();
            else
                pause();
        }
        if (Input.GetKeyDown(KeyCode.R))
            restart();
    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
        player.enabled = false;
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        player.enabled = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void restart()
    {
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
