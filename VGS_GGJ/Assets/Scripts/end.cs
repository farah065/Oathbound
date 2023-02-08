using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    public int i = -1;

    string oathbound, GDD, GAD, GSD, narrative;
    public Text title;
    public Text content;

    public GameObject menuButton;
    public GameObject exit;
    
    void Start()
    {
        GDD = "Farah Ahmad\nYoussef ElSharkawy\nZeyad Alaa";
        GAD = "Mariam Tamer\nNada Tamer\nYara AlTantawy";
        GSD = "AlHassanein AlKendy\nOmar Yasser\nZiad ElGendy";
        narrative = "Yousef Korayem";
        string[] credits = { "", GDD, GAD, GSD, narrative };
        string[] titles = { "Oathbound", "Programming", "Art", "Music & Sound Design", "Narrative Design" };
        StartCoroutine(fade(credits, titles));
        menuButton.SetActive(false);
        exit.SetActive(false);
    }

    IEnumerator fade(string[] credits, string[] titles)
    {
        if (i == -1)
        {
            title.text = "";
            content.text = "";
            yield return new WaitForSeconds(10);
            i++;
            StartCoroutine(fade(credits, titles));
        }
        else {
            title.text = titles[i];
            content.text = credits[i];
            if (i == 0)
            {
                exit.SetActive(true);
                anim2.SetBool("skip", true);
                title.fontSize = 80;
            }
            else
                title.fontSize = 50;
            anim.SetBool("fade", true);
            yield return new WaitForSeconds(10);
            anim.SetBool("fade", false);
            if(i == credits.Length - 1)
                anim2.SetBool("skip", false);
            yield return new WaitForSeconds(1);
            i++;
            if (i != credits.Length)
                StartCoroutine(fade(credits, titles));
            else
            {
                exit.SetActive(false);
                title.text = "";
                content.text = "";
                anim.SetBool("fade", true);
                menuButton.SetActive(true);
            }
        }
    }
}
