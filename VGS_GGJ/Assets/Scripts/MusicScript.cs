using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip keySFX;

    public bool playing;
    public bool sfxPlayed;

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;

    public bool phase1;
    public bool changed = false;

    public int i;
    void Start()
    {
        clips = Resources.LoadAll<AudioClip>("Audio");
        phase1 = true;
        if (PlayerPrefs.GetInt("CurrentLevel", 1) == 1)
            i = 0;
        else
            i = PlayerPrefs.GetInt("CurrentLevel", 1) - 2;
    }

    void Update()
    {
        playing = audioSource.isPlaying;
        if (!playing)
        {
            if (phase1)
            {
                audioSource.clip = clips[i];
            }
            else
            {
                if (!sfxPlayed)
                {
                    audioSource.clip = keySFX;
                    sfxPlayed = true;
                    changed = true;
                }
                else
                {
                    audioSource.clip = clips[5];
                }
            }
            audioSource.Play();
        }

        if (PauseMenu.paused)
        {
            audioSource.pitch = 0.5f;
            audioSource2.pitch = 0.5f;
            audioSource2.volume = 0.5f;
            if (!phase1)
                audioSource.volume = 0.1f;
            else
                audioSource.volume = 0.5f;
        }
        else
        {
            audioSource.pitch = 1;
            audioSource2.pitch = 1;
            audioSource2.volume = 1f;
            if (!phase1)
                audioSource.volume = 0.3f;
            else
                audioSource.volume = 1;
        }

        if (rootgenscript.changeMusic && !changed && PlayerPrefs.GetInt("CurrentLevel", 1) != 6)
        {
            audioSource.Stop();
            sfxPlayed = true;
            phase1 = false;
            changed = true;
        }
        /*if(!rootgenscript.changeMusic && changed){
            audioSource.Stop();
            phase1 = true;
            changed = false;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "skullkey")
        {
            if (phase1)
            {
                sfxPlayed = false;
                audioSource.Stop();
                phase1 = false;
            }
        }
    }
}
