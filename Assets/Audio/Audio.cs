using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{

    [SerializeField] AudioClip[] sounds;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (sounds.Length > 0)
        {
            SelectSound();
            audioSource.Play();
        }
    }

    public void Play(AudioClip[] sounds)
    {
        this.sounds = sounds;
        Play();
    }

    private void SelectSound()
    {
        var currentIndex = Util.GetRandomNumber(0, sounds.Length);
        audioSource.clip = sounds[currentIndex];
    }
}
