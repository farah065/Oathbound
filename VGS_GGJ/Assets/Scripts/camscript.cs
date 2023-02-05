using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > 0)
            cam.position = new Vector3(player.position.x, 0, -10);
        else
            cam.position = new Vector3(0, 0, -10);
    }
}
