using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    PlayerScript player;
    public static int gameComplete = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && GameObject.FindGameObjectWithTag("Altar").transform.childCount == 0 && player.dors.engaged){
            int currLevelNum = int.Parse(SceneManager.GetActiveScene().name.Substring(6));
            int nextLevelNum = currLevelNum + 1;
            string nextLevel = "Level " + nextLevelNum;
            PlayerPrefs.SetInt("CurrentLevel", nextLevelNum);
            if(currLevelNum < 6)
                SceneManager.LoadScene(nextLevel);
            else
            {
                gameComplete = 1;
                PlayerPrefs.SetInt("CurrentLevel", 1);
                SceneManager.LoadScene("Main Menu");
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("End Scene");
    }
}
