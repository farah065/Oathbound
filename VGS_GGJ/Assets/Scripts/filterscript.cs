using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filterscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = Color.white;
        tmp.a = Mathf.Lerp(0, 1, player.position.x / 50);
        spr.color = tmp;
    }
}
