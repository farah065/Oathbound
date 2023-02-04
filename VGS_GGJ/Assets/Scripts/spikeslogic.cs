using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeslogic : MonoBehaviour
{
    ArrayList spikes = new ArrayList();
    float p = 0;
    bool increasing = true;
    public Animator anim1;
    public Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            spikes.Add(child);
    }

    // Update is called once per frame
    void Update()
    {
        if (increasing)
            p += Time.deltaTime;
        else
            p -= Time.deltaTime;
        foreach(Transform spike in spikes)
        {
            Vector3 temp = spike.localScale;
            temp.y = Mathf.Lerp(12.7251f,0.1f,p);
            spike.localScale = temp;
        }
        if(p<=1 && p >= 0)
        {
            anim1.SetBool("changing", true);
            anim2.SetBool("changing", true);
        }
        else
        {
            anim1.SetBool("changing", false);
            anim2.SetBool("changing", false);
        }
        if (p < -1 || p > 2)
            increasing = !increasing;
    }

}
