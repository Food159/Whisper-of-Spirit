using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource SfxSource;

    [Header("---------Audio Clip---------")]
    public AudioClip bgClip;
    public AudioClip Death;
    public AudioClip Shoot;
    public AudioClip Jump;
    public AudioClip Run;
    public AudioClip Walk;
    public AudioClip Landing;

    private void Start()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
        bgSource.loop = true;
    }
    public void PlaySfx(AudioClip clip)
    {
        //SfxSource.clip = clip;
        //SfxSource.Play();
        SfxSource.PlayOneShot(clip);
    }
}
