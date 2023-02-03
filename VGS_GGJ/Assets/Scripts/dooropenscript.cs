using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenscript : MonoBehaviour
{
    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
