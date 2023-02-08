using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic musicPlayer;
    public AudioSource source;
    public AudioClip mainMusic;
    public AudioClip completeMusic;
    private bool changed = false;

    void Awake()
    {
        if (musicPlayer == null)
        {
            musicPlayer = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.GetInt("Complete", 0) == 1)
        {
            source.clip = completeMusic;
            source.Play();
            changed = false;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Settings" && 
            SceneManager.GetActiveScene().name != "cutscene")
        {
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.GetInt("Complete", 0) == 0 && !changed)
        {
            source.Stop();
            source.clip = mainMusic;
            source.Play();
            changed = true;
        }
    }
}
