using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    public Transform player;
    public AudioSource source;
    private Vector3 startPos;
    private float pauseVol;
    private bool unpaused = true;

    void Start()
    {
        startPos = player.position;
    }

    void Update()
    {
        float curDistance = Vector3.Distance(startPos, player.position);
        float factor = (1 - (curDistance / 55)) / 2;
        if (factor > 0)
        {
            pauseVol = factor / 2;
        }
        else
        {
            pauseVol = 0;
        }

        if (PauseMenu.paused)
        {
            source.volume = pauseVol;
            source.pitch = 0.5f;
        }
        else
        {
            source.volume = pauseVol * 2;
            source.pitch = 1;
        }
    }
}
