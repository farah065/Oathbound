using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public AudioMixer master;
    public Renderer bgRen;
    public AudioSource audioSource;
    public AudioClip credits;

    Resolution[] resolutions;
    public static int currentResolutionIndex = 0;
    EventSystem eventSystem;
    private GameObject mainMenuUI;
    private GameObject controlsMenuUI;
    private GameObject firstSelectedMainMenu;
    private GameObject firstSelectedControls;
    void Start()
    {
        // creating array of all possible resolutions and getting the index for the current one
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // playerprefs to make sure settings are as the user left them
        
        Resolution resolution = resolutions[PlayerPrefs.GetInt("Resolution", resolutions.Length - 1)];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        Screen.fullScreen = (PlayerPrefs.GetInt("FullScreen", 1) != 0);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);

        master.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1)) * 20);
        master.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 1)) * 20);
        master.SetFloat("sfxVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 1)) * 20);

        PlayerPrefs.SetInt("Complete", LevelTransition.gameComplete);

        if(PlayerPrefs.GetInt("Complete") == 1)
        {
            bgRen.enabled = false;
            audioSource.clip = credits;
            audioSource.Play();
        }
        mainMenuUI = gameObject.transform.Find("MainMenu").gameObject;
        controlsMenuUI = gameObject.transform.Find("Controls Menu").gameObject;
        firstSelectedMainMenu = mainMenuUI.transform.Find("Play Game").gameObject;
        firstSelectedControls = controlsMenuUI.transform.Find("Rebind Move Left/TriggerRebindButton").gameObject;
        eventSystem = EventSystem.current;
    }

    public void SettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("CurrentLevel", 1) == 1 && PlayerPrefs.GetInt("Cutscene", 0) == 0)
            SceneManager.LoadScene("cutscene");
        else
            SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("CurrentLevel", 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void controls()
    {
        mainMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelectedControls);
    }

    public void back()
    {
        mainMenuUI.SetActive(true);
        controlsMenuUI.SetActive(false);
        eventSystem.SetSelectedGameObject(firstSelectedMainMenu);
    }
}
