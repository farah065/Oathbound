using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundenragescript : MonoBehaviour
{
    Animator anim;
    public rootgenscript rgn;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rgn.enraged)
        {
            anim.SetBool("Enraged", true);
        }
    }
}
