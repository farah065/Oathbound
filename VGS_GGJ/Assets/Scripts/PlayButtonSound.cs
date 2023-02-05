using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void playClick()
    {
        audioSource.Play();
    }
}
