using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    private GameObject pauseMenuUI;
    private GameObject controlsMenuUI;
    private PlayerScript player;

    private PlayerInput playerInput;
    private InputAction pauseAction;
    private InputAction restartAction;
    private EventSystem eventSystem;
    private GameObject firstSelectedPause;
    private GameObject firstSelectedControls;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
        restartAction = playerInput.actions["Restart"];


        paused = false;
        pauseMenuUI = gameObject.transform.Find("Pause Menu").gameObject;
        controlsMenuUI = gameObject.transform.Find("Controls Menu").gameObject;
        firstSelectedPause = pauseMenuUI.transform.Find("Resume").gameObject;
        firstSelectedControls = controlsMenuUI.transform.Find("KeyboardControls/Rebind Move Left/TriggerRebindButton").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
    }

    private void pause(InputAction.CallbackContext context)
    {
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

    public void controls(){
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelectedControls);
    }

    public void back(){
        pauseMenuUI.SetActive(true);
        controlsMenuUI.SetActive(false);
        eventSystem.SetSelectedGameObject(firstSelectedPause);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        restartAction.Enable();
        pauseAction.performed += pause;
        restartAction.performed += restart;
        eventSystem = EventSystem.current;
    }
    private void OnDisable() 
    {
        pauseAction.Disable();
        restartAction.Disable();
        pauseAction.performed -= pause;
        restartAction.performed -= restart;
    }
}
