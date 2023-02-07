using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class antifilter : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spr;
    float vis = 1.5f;
    public AudioSource audio;
    bool ok = false;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        StartCoroutine(makeok());
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = Color.white;
        vis -= Time.deltaTime;
        tmp.a = Mathf.Lerp(0, 1, vis);
        spr.color = tmp;
        if (!audio.isPlaying || (ok && Input.anyKey))
            SceneManager.LoadScene("Main Menu");
    }
    IEnumerator makeok()
    {
        yield return new WaitForSeconds(5);
        ok = true;
    }
}
