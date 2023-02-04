using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenscript : MonoBehaviour
{
    public bool open = false;
    public bool close = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            open = false;
            GetComponent<BoxCollider2D>().enabled = true;
            anim.SetBool("open", true);
            StartCoroutine(closeopen());
        }
        if (close)
        {
            close = false;
            anim.SetBool("close", true);
            StartCoroutine(closeclose());
        }
    }
    IEnumerator closeopen()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("open", false);
    }
    IEnumerator closeclose()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("close", false);
    }
}
