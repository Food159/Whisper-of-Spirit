using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource bgSource;

    [Header("---------Audio Clip---------")]
    public AudioClip bgClip;

    private void Start()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
        bgSource.loop = true;
    }
}
