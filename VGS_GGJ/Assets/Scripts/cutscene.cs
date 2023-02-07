using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public Animator anim;
    public GameObject im1;
    public GameObject im2;
    public bool fading = false;
    public int i = 0;

    private void Start()
    {
        GameObject[] im = { im1, im2 };
        StartCoroutine(fade(im));
    }

    void Update()
    {
        
    }

    IEnumerator fade(GameObject[] im)
    {
        while (fading)
            yield return null;
        fading = true;
        if (i == 0)
            yield return new WaitForSeconds(5);
        else
            yield return new WaitForSeconds(4);
        anim.SetBool("fade", true);
        yield return new WaitForSeconds(1);
        im[i].SetActive(false);
        anim.SetBool("fade", false);
        yield return new WaitForSeconds(1);
        fading = false;
        i++;
        if (i == im.Length)
            StartCoroutine(end());
        else
            StartCoroutine(fade(im));
    }

    IEnumerator end()
    {
        while (fading)
            yield return null;
        fading = true;
        yield return new WaitForSeconds(4);
        anim.SetBool("fade", true);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("Cutscene", 1);
        SceneManager.LoadScene("Level 1");
    }
}
