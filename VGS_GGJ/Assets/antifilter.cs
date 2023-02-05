using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antifilter : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spr;
    float vis = 1.5f;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = Color.white;
        vis -= Time.deltaTime;
        tmp.a = Mathf.Lerp(0, 1, vis);
        spr.color = tmp;
    }
}
