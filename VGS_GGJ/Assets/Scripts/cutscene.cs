using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    float t = 0;
    public GameObject im1;
    public GameObject im2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 3)
        {
            im1.SetActive(false);
        }
        if (t > 6)
        {
            im2.SetActive(false);
        }
        if(t > 10)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
