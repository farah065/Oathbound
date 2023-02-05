using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsScript : MonoBehaviour
{
    private GameObject keyboardUI;
    private GameObject gamepadUI;
    private GameObject firstSelectedKeyboard;
    private GameObject firstSelectedGamePad;
    private EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        keyboardUI = gameObject.transform.Find("KeyboardControls").gameObject;
        gamepadUI = gameObject.transform.Find("GamePadControls").gameObject;
        firstSelectedKeyboard = keyboardUI.transform.Find("Rebind Move Left/TriggerRebindButton").gameObject;
        firstSelectedGamePad = gamepadUI.transform.Find("Rebind Move Left/TriggerRebindButton").gameObject;
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showKeyboard()
    {
        keyboardUI.SetActive(true);
        gamepadUI.SetActive(false);
        eventSystem.SetSelectedGameObject(firstSelectedKeyboard);
    }
    public void showGamepad()
    {
        keyboardUI.SetActive(false);
        gamepadUI.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelectedGamePad);
    }
}
