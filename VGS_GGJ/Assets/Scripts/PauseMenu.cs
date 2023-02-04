using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    private GameObject pauseMenuUI;
    private PlayerScript player;

    private PlayerInput playerInput;
    private InputAction pauseAction;
    private InputAction restartAction;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
        restartAction = playerInput.actions["Restart"];


        paused = false;
        pauseMenuUI = gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
    }

    private void pause(InputAction.CallbackContext context)
    {
        Debug.Log("pause");
        if(paused){
            resume();
        }else{
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            paused = true;
            player.enabled = false;
        }
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

    private void restart(InputAction.CallbackContext context)
    {
        restart();
    }
    public void restart(){
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        restartAction.Enable();
        pauseAction.performed += pause;
        restartAction.performed += restart;
    }
    private void OnDisable() 
    {
        pauseAction.Disable();
        restartAction.Disable();
        pauseAction.performed -= pause;
        restartAction.performed -= restart;
    }
}
