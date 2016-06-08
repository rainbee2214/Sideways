using UnityEngine;
using System.Collections;

public enum SoundType
{
    Coin, Star,
    Portal,
    Death,
    Boost,
    Background
}
public class AudioController : MonoBehaviour
{
    public static AudioController controller;

    void Awake()
    {
        controller = this;
        audioSource = GetComponent<AudioSource>();
        PlaySound(SoundType.Background);
    }
    AudioSource audioSource;

    public AudioClip coinClip, starClip, portalClip, boostClip, deathClip, backgroundSong;
    public void PlaySound(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Coin:
                audioSource.PlayOneShot(coinClip);
                break;
            case SoundType.Star:
                audioSource.PlayOneShot(starClip);
                break;
            case SoundType.Portal:
                audioSource.PlayOneShot(portalClip);
                break;
            case SoundType.Boost:
                audioSource.PlayOneShot(boostClip);
                break;
            case SoundType.Death:
                audioSource.PlayOneShot(deathClip);
                break;
            case SoundType.Background:
                audioSource.clip = backgroundSong;
                audioSource.Play();
                break;
            default:
                break;
        }
    }
}
