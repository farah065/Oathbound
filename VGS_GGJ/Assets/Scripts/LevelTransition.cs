using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    PlayerScript player;
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
            SceneManager.LoadScene(nextLevel);
        }

    }
}
