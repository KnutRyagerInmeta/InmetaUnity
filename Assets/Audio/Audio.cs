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

    public void Play(AudioClip[] sounds = null)
    {
        if(sounds != null) this.sounds = sounds;
        if (this.sounds.Length > 0)
        {
            SelectSound();
            audioSource.Play();
        }
    }

    private void SelectSound()
    {
        var currentIndex = Util.GetRandomNumber(0, sounds.Length);
        audioSource.clip = sounds[currentIndex];
    }
}
