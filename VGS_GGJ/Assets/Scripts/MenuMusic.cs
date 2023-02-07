using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic musicPlayer;
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
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Settings" && 
            SceneManager.GetActiveScene().name != "cutscene")
        {
            Destroy(this.gameObject);
        }
    }
}
