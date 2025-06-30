using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variable
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource SfxSource;
    //[SerializeField] public AudioSource dialogueSource;

    [Header("---------Audio Clip---------")]
    public AudioClip bgClip;
    public AudioClip Death;
    public AudioClip Shoot;
    public AudioClip Jump;
    public AudioClip Run;
    public AudioClip Walk;
    public AudioClip Landing;
    public AudioClip dialogue;

    public static SoundManager instance;
    #endregion

    private void Start()
    {
        bgSource.clip = bgClip;
        bgSource.Play();
        bgSource.loop = true;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySfx(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
}
