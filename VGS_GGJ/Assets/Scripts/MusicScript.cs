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
    void Start()
    {
        audioSource.volume = 1;
        clips = Resources.LoadAll<AudioClip>("Audio");
        audioSource.clip = clips[PlayerPrefs.GetInt("CurrentLevel") - 1];
        audioSource.Play();
        phase1 = true;
    }

    void Update()
    {
        playing = audioSource.isPlaying;
        if (!playing)
        {
            if (phase1)
            {
                audioSource.clip = clips[PlayerPrefs.GetInt("CurrentLevel") - 1];
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
                    //audioSource.outputAudioMixerGroup = musicMixer;
                    audioSource.clip = clips[6];
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
                audioSource.volume = 0.05f;
            else
                audioSource.volume = 0.5f;
        }
        else
        {
            audioSource.pitch = 1;
            audioSource2.pitch = 1;
            audioSource2.volume = 1f;
            if (!phase1)
                audioSource.volume = 0.15f;
            else
                audioSource.volume = 1;
        }

        if (rootgenscript.changeMusic && !changed)
        {
            audioSource.Stop();
            sfxPlayed = true;
            phase1 = false;
            changed = true;
        }
        if(!rootgenscript.changeMusic && changed){
            audioSource.Stop();
            phase1 = true;
            changed = false;
        }
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
            //audioSource.outputAudioMixerGroup = sfxMixer;
        }
    }
}
