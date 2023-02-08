using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    public Transform player;
    public AudioSource source;
    private Vector3 startPos;

    void Start()
    {
        startPos = player.position;
    }

    void Update()
    {
        float curDistance = Vector3.Distance(startPos, player.position);
        float factor = (1 - (curDistance / 55)) / 2;
        Debug.Log(factor);
        if (factor > 0)
            source.volume = factor;
        else
            source.volume = 0;
    }
}
