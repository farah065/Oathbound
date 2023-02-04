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
    public AudioSource keySource;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;

    public bool phase1;
    public bool transition;
    public bool first = true;

    public int i = 0;
    void Start()
    {
        audioSource.volume = 1;
        clips = Resources.LoadAll<AudioClip>("Audio");
        audioSource.clip = clips[i];
        audioSource.Play();
        i++;
        phase1 = true;
    }

    void Update()
    {
        playing = audioSource.isPlaying;
        if (!playing)
        {
            if (phase1)
            {
                if(i < 4)
                {
                    audioSource.clip = clips[i];
                    i++;
                }
                else
                    audioSource.clip = clips[4];
            }
            else
            {
                if (!sfxPlayed)
                {
                    sfxPlayed = true;
                    audioSource.clip = keySFX;
                    audioSource.Play();
                }
                else
                {
                    //audioSource.outputAudioMixerGroup = musicMixer;
                    playing = audioSource.isPlaying;
                    audioSource.clip = clips[5];
                    audioSource.volume = 0.5f;
                }
            }
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "skullkey")
        {
            audioSource.Stop();
            //audioSource.outputAudioMixerGroup = sfxMixer;
            phase1 = false;
        }
    }
}
