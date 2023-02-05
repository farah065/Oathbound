using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer master;
    public Renderer bgRen;
    public AudioSource audioSource;
    public AudioClip credits;

    Resolution[] resolutions;
    public static int currentResolutionIndex = 0;
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
    }

    public void SettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("CurrentLevel", 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
